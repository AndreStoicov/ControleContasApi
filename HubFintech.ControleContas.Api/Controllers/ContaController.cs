using System.Web.Http;
using System.Web.Http.Description;
using HubFintech.ControleContas.Api.Domain.Services.Interfaces;
using HubFintech.ControleContas.Api.Domain.ViewModels.Request;

namespace HubFintech.ControleContas.Api.Controllers
{
    [RoutePrefix("v1/pessoas/{pessoaId}/contas")]
    [ApiExplorerSettings(IgnoreApi = false)]
    public class ContaController : ApiController
    {
        private readonly IContaService _contaService;
        private readonly ITransacaoService _transacaoService;

        public ContaController(IContaService contaService, ITransacaoService transacaoService)
        {
            _contaService = contaService;
            _transacaoService = transacaoService;
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

        [Route(Name = "CriaConta"), HttpPost]
        public IHttpActionResult PostCriaConta([FromUri] int pessoaId, [FromBody] ContaRequest input)
        {
            var conta = _contaService.CriaConta(input);
            return Ok(conta);
        }

        [Route("{contaId/filhas}", Name = "CriaContaFilha"), HttpPost]
        public IHttpActionResult PostCriaContaFilha([FromUri] int pessoaId, [FromBody] ContaFilhaRequest input)
        {
            var conta = _contaService.CriaContaFilha(input);
            return Ok(conta);
        }

        [Route("{contaId}/transacoes", Name = "ObtemTodasTransacoes"), HttpGet]
        public IHttpActionResult GetAllTransacoes([FromUri] int pessoaId, [FromUri] int contaId)
        {
            var pessoas = _transacaoService.ObtemTodasTransacoes(contaId);
            return Ok(pessoas);
        }
    }
}