using HubFintech.ControleContas.Api.Domain.ViewModels.Response.Hypermedia;
using Newtonsoft.Json;

namespace HubFintech.ControleContas.Api.Domain.ViewModels.Response
{
    public class PessoaResponse : Resource
    {
        public int Id { get; set; }
        public string TipoPessoa { get; set; }
        
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public PessoaFisica PessoaFisica { get; set; }
        
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public PessoaJuridica PessoaJuridica { get; set; }
    }
}