using HubFintech.ControleContas.Api.Domain.ViewModels.Response.Hypermedia;
using HubFintech.ControleContas.Api.Domain.ViewModels.Shared;
using Newtonsoft.Json;

namespace HubFintech.ControleContas.Api.Domain.ViewModels.Response
{
    public class PessoaResponse : Resource
    {
        public PessoaResponse()
        {
        }

        public PessoaResponse(PessoaFisica pessoa)
        {
            Id = pessoa.Id;
            TipoPessoa = "PF";
            PessoaFisica = new PessoaFisicaViewModel(pessoa);
        }

        public PessoaResponse(PessoaJuridica pessoa)
        {
            Id = pessoa.Id;
            TipoPessoa = "PJ";
            PessoaJuridica = new PessoaJuridicaViewModel(pessoa);
        }

        public int Id { get; set; }
        public string TipoPessoa { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public PessoaFisicaViewModel PessoaFisica { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public PessoaJuridicaViewModel PessoaJuridica { get; set; }
    }
}