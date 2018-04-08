using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Dynamitey.DynamicObjects;
using Fabrik.Common;
using HubFintech.ControleContas.Api.Configuration.Factories;
using HubFintech.ControleContas.Api.Domain.ViewModels;
using HubFintech.ControleContas.Api.Domain.ViewModels.Response;
using HubFintech.ControleContas.Api.Domain.ViewModels.Shared;
using Microsoft.Owin.Diagnostics.Views;

namespace HubFintech.ControleContas.Api.Configuration.Handlers
{
    public class WrappingHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);

            return BuildApiResponse(request, response);
        }

        private static HttpResponseMessage BuildApiResponse(HttpRequestMessage request, HttpResponseMessage response)
        {
            List<Mensagem> mensagens = null;
            var logFactory =
                GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(ILogFactory)) as ILogFactory;

            if (response.TryGetContentValue(out object content) && !response.IsSuccessStatusCode)
            {
                if (content is HttpError error)
                {
                    mensagens = new List<Mensagem>();
                    content = null;

                    if (error.ModelState != null && error.ModelState.Count > 0)
                    {
                        response.StatusCode = (HttpStatusCode) 422;

                        foreach (var errorKey in error.ModelState.Keys)
                        {
                            var erro = error.ModelState[errorKey] as string[];
                            erro.ForEach(x => mensagens.Add(new Mensagem(x)));
                        }
                    }

                    if (response.StatusCode == HttpStatusCode.InternalServerError)
                    {
                        var errorMessage = "Ocorreu um erro inesperado em sua requisição." +
                                           "Favor entrar em contato com o Administrador da" +
                                           $"API com o código de erro: {logFactory.CorrelationId()}";

                        mensagens.Add(new Mensagem(errorMessage));
                    }
                }
            }

            var newResponse = request.CreateResponse(response.StatusCode,
                new ApiResponse(response.StatusCode, content, mensagens));

            foreach (var header in response.Headers)
            {
                newResponse.Headers.Add(header.Key, header.Value);
            }

            return newResponse;
        }
    }
}