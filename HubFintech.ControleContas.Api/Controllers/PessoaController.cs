using System.Web.Http;
using System.Web.Http.Description;

namespace HubFintech.ControleContas.Api.Controllers
{
    [RoutePrefix("v1/pessoas")]
    [ApiExplorerSettings(IgnoreApi = false)]
    public class PessoaController: BaseController
    {
        [Route, HttpGet]
        public IHttpActionResult GetAll()
        {
            var contas = _contaRepository.All();
            return Ok(contas);
        }
        
        [Route("{pessoaId}"), HttpGet]
        public IHttpActionResult GetById()
        {
            var contas = _contaRepository.All();
            return Ok(contas);
        }
        
        [Route, HttpPost]
        public IHttpActionResult Post()
        {
            var contas = _contaRepository.All();
            return Ok(contas);
        }
    }
}