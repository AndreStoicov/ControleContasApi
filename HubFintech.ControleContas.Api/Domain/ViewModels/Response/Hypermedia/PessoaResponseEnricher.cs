using Fabrik.Common.WebAPI;
using Fabrik.Common.WebAPI.Links;
using System.Net.Http;

namespace HubFintech.ControleContas.Api.Domain.ViewModels.Response.Hypermedia
{
    public class PessoaResponseEnricher : ObjectContentResponseEnricher<PessoaResponse>
    {
        public override void Enrich(PessoaResponse content)
        {
            content.AddLink(new SelfLink(Request.GetUrlHelper()
                .Link("", new {controller = "pessoa", id = content.Id})));
        }
    }
}