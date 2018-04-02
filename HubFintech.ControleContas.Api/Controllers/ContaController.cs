using System.Web.Http;
using System.Web.Http.Description;
using HubFintech.ControleContas.Api.Domain;
using HubFintech.ControleContas.Api.Domain.Repositories.Interfaces;

namespace HubFintech.ControleContas.Api.Controllers
{
    [RoutePrefix("v1/contas")]
    [ApiExplorerSettings(IgnoreApi = false)]
    public class ContaController : BaseController
    {
        private readonly IBaseRepository<Conta> _contaRepository;
        
        public ContaController(IBaseRepository<Conta> contaRepository)
        {
            _contaRepository = contaRepository;
        }
        
        [Route, HttpGet]
        public IHttpActionResult Get()
        {
            var contas = _contaRepository.All();
            return Ok(contas);
        }
    }
}