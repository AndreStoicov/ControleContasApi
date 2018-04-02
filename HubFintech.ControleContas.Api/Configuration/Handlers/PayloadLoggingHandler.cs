using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Routing;
using HubFintech.ControleContas.Api.Configuration.Factories;

namespace HubFintech.ControleContas.Api.Configuration.Handlers
{
    public class PayloadLoggingHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var stopwatch = new Stopwatch();

            stopwatch.Start();

            var endpoint = $"{request.Method} > {request.RequestUri}";

            if (!IsValidEndpoint(request))
                return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);

            var requestPayload = await request.Content.ReadAsStringAsync().ConfigureAwait(false);

            await IncommingMessageAsync(endpoint, requestPayload).ConfigureAwait(false);

            var response = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);

            stopwatch.Stop();

            await ResquestDuration(endpoint, stopwatch);

            var responsePayload = await response.Content.ReadAsStringAsync();

            await OutgoingMessageAsync(endpoint, responsePayload).ConfigureAwait(false);

            return response;
        }

        private async Task IncommingMessageAsync(string endpoint, string payload)
        {
            var logFactory =
                GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(ILogFactory)) as ILogFactory;

            if (!string.IsNullOrWhiteSpace(payload))
                payload = " - Payload: " + payload;

            await Task.Run(() => logFactory.Log().Info($"REQUEST | Endpoint: {endpoint}{payload}"));
        }

        private async Task OutgoingMessageAsync(string endpoint, string payload)
        {
            var logFactory =
                GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(ILogFactory)) as ILogFactory;

            if (!string.IsNullOrWhiteSpace(payload))
                payload = " - Payload: " + payload;

            await Task.Run(() => logFactory.Log().Info($"RESPONSE | Endpoint: {endpoint}{payload}"));
        }

        private async Task ResquestDuration(string endpoint, Stopwatch stopwatch)
        {
            var logFactory =
                GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(ILogFactory)) as ILogFactory;
            var ts = stopwatch.Elapsed;

            var elapsed = $"{ts.Hours:00}:{ts.Minutes:00}:{ts.Seconds:00}.{ts.Milliseconds / 10:00}";

            await Task.Run(() => logFactory.Log().Debug($"DURATION | Endpoint: {endpoint} - Elapsed: {elapsed}"));
        }

        private bool IsValidEndpoint(HttpRequestMessage request)
        {
            var httpConfiguration = request.GetConfiguration();
            var httpRouteData = httpConfiguration.Routes.GetRouteData(request);

            if (httpRouteData == null)
                return false;

            try
            {
                var subroutes = (IEnumerable<IHttpRouteData>) httpRouteData.Values["MS_SubRoutes"];

                return ((IHttpRouteData[]) subroutes)[0].Route.RouteTemplate.Length > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}