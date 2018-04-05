using System.Web.Http;
using System.Web.Http.Description;
using HubFintech.ControleContas.Api.Domain.Services.Interfaces;

namespace HubFintech.ControleContas.Api.Controllers
{
    [RoutePrefix("v1/pessoas/{pessoaId}/contas")]
    [ApiExplorerSettings(IgnoreApi = false)]
    public class ContaController : ApiController
    {
        private readonly IContaService _contaService;

        public ContaController(IContaService contaService)
        {
            _contaService = contaService;
        }

        [Route(Name = "ObtemTodasContas"), HttpGet]
        public IHttpActionResult Get([FromUri] int pessoaId)
        {
            var contas = _contaService.ObtemContas(pessoaId);
            return Ok(contas);
        }

        [Route("{contaId}", Name = "ObtemConta"), HttpGet]
        public IHttpActionResult GetObtemConta([FromUri] int pessoaId, [FromUri] int contaId)
        {
            var conta = _contaService.ObtemConta(pessoaId, contaId);
            return Ok(conta);
        }

        [Route("{contaId}/filhas", Name = "ObtemTodasContasFilhas"), HttpGet]
        public IHttpActionResult GetObtemTodasContasFilhas([FromUri] int pessoaId, [FromUri] int contaId)
        {
            var conta = _contaService.ObtemContasFilha(pessoaId, contaId);
            return Ok(conta);
        }
    }
}