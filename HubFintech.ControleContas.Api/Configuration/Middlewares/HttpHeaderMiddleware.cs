using System.Threading.Tasks;
using System.Web.Http;
using HubFintech.ControleContas.Api.Configuration.Factories;
using Microsoft.Owin;

namespace HubFintech.ControleContas.Api.Configuration.Middlewares
{
    public class HttpHeaderMiddleware : OwinMiddleware
    {
        public HttpHeaderMiddleware(OwinMiddleware next) : base(next)
        {
        }

        public override async Task Invoke(IOwinContext context)
        {
            if (context.Request.Method != "OPTIONS")
            {
                var logFactory =
                    GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(ILogFactory)) as ILogFactory;

                context.Response.Headers.Append("Access-Control-Expose-Headers", "X-Request-ID");
                context.Response.Headers.Append("X-Request-ID", logFactory.CorrelationId());
            }

            await Next.Invoke(context);
        }
    }
}