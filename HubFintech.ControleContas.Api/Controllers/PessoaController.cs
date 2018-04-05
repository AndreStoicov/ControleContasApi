using System.Web.Http;
using System.Web.Http.Description;
using HubFintech.ControleContas.Api.Domain.Services.Interfaces;
using HubFintech.ControleContas.Api.Domain.ViewModels.Request;

namespace HubFintech.ControleContas.Api.Controllers
{
    [RoutePrefix("v1/pessoas")]
    [ApiExplorerSettings(IgnoreApi = false)]
    public class PessoaController : ApiController
    {
        private readonly IPessoaService _pessoaService;

        public PessoaController(IPessoaService pessoaService)
        {
            _pessoaService = pessoaService;
        }

        [Route(Name = "ObtemTodasPessoas"), HttpGet]
        public IHttpActionResult GetAll()
        {
            var pessoas = _pessoaService.ObtemTodas();
            return Ok(pessoas);
        }

        [Route("{pessoaId}", Name = "ObtemPessoa"), HttpGet]
        public IHttpActionResult GetById([FromUri] int pessoaId)
        {
            var pessoa = _pessoaService.ObtemPessoa(pessoaId);
            return Ok(pessoa);
        }

        [Route(Name = "CriaPessoa"), HttpPost]
        public IHttpActionResult Post([FromBody] PessoaRequest input)
        {
            var pessoa = _pessoaService.CriaPessoa(input);
            return Ok(pessoa);
        }
    }
}