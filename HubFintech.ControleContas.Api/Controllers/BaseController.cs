using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.ModelBinding;
using HubFintech.ControleContas.Api.Domain.ViewModels;

namespace HubFintech.ControleContas.Api.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class BaseController : ApiController
    {
        protected IHttpActionResult Response(Model model)
        {
            HttpResponseMessage response = null;

            if (model.IsSucess())
            {
                switch (model.StatusCode)
                {
                    case HttpStatusCode.NoContent:
                        response = Request.CreateResponse(HttpStatusCode.NoContent);
                        break;
                    default:
                        response = Request.CreateResponse(model.StatusCode, model);
                        break;
                }
            }
            else
            {
                response = CreateErrorResponse(model);
            }

            return new HttpActionResult(model.StatusCode, response);
        }

        private HttpResponseMessage CreateErrorResponse(Model model)
        {
            var dicionario = new ModelStateDictionary();

            foreach (var erro in model.Erros)
            {
                var key = erro.Codigo;
                var value = erro.Descricao;

                dicionario.AddModelError(key, value);
            }

            return Request.CreateErrorResponse(model.StatusCode, dicionario);
        }
    }


    public class HttpActionResult : IHttpActionResult
    {
        public HttpResponseMessage Message { get; }
        public HttpStatusCode StatusCode { get; private set; }

        public HttpActionResult(HttpStatusCode statusCode, string message)
        {
            StatusCode = statusCode;
            Message = new HttpResponseMessage(statusCode) {Content = new StringContent(message)};
        }

        public HttpActionResult(HttpStatusCode statusCode, HttpResponseMessage message)
        {
            message.StatusCode = statusCode;
            StatusCode = statusCode;
            Message = message;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(Message);
        }
    }
}