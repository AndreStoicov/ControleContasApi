using System.Web.Http;
using System.Web.Http.Description;
using HubFintech.ControleContas.Api.Domain.Services.Interfaces;
using HubFintech.ControleContas.Api.Domain.ViewModels.Request;

namespace HubFintech.ControleContas.Api.Controllers
{
    [RoutePrefix("v1/transacoes")]
    [ApiExplorerSettings(IgnoreApi = false)]
    public class TransacaoController : ApiController
    {
        private readonly ITransacaoService _transacaoService;

        public TransacaoController(ITransacaoService transacaoService)
        {
            _transacaoService = transacaoService;
        }

        [Route("{transacaoId}", Name = "ObtemTransacao"), HttpGet]
        public IHttpActionResult GetAll([FromUri] int transacaoId)
        {
            var pessoas = _transacaoService.ObtemTransacao(transacaoId);
            return Ok(pessoas);
        }
        
        [Route("{transacaoId/extornar}", Name = "ObtemTransacao"), HttpGet]
        public IHttpActionResult GetAll([FromUri] int transacaoId, [FromBody] TransacaoExtornoRequest input)
        {
            var pessoas = _transacaoService.ObtemTransacao(transacaoId);
            return Ok(pessoas);
        }
    }
}