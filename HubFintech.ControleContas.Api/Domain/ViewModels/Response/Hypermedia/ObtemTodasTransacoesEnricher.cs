using System.Collections.Generic;
using System.Net.Http;
using Fabrik.Common.WebAPI;
using Fabrik.Common.WebAPI.Links;

namespace HubFintech.ControleContas.Api.Domain.ViewModels.Response.Hypermedia
{
    public class ObtemTodasTransacoesEnricher : ObjectContentResponseEnricher<List<TransacoesResponse>>
    {
        public override void Enrich(List<TransacoesResponse> content)
        {
            content.ForEach(x =>
            {
                x.AddLink(new SelfLink(Request.GetUrlHelper()
                    .Link("ObtemTransacao", new {transacaoId = x.Id}), "Obtem Transacao Por Id"));

                if (!x.Extornado)
                {
                    x.AddLink(new Link("add-extorno", Request.GetUrlHelper()
                        .Link("ExtornaTransacao", new {transacaoId = x.Id}), "Extorna essa Transacao"));
                }
            });
        }
    }
}