using System.Collections.Generic;
using System.Net.Http;
using Fabrik.Common.WebAPI;
using Fabrik.Common.WebAPI.Links;

namespace HubFintech.ControleContas.Api.Domain.ViewModels.Response.Hypermedia
{
    public class ObtemTransacaoEnricher : ObjectContentResponseEnricher<TransacoesResponse>
    {
        public override void Enrich(TransacoesResponse content)
        {
            content.AddLink(new SelfLink(Request.GetUrlHelper()
                .Link("ObtemTransacao", new {transacaoId = content.Id}), "Obtem Transacao Por Id"));

            if (!content.Extornado)
            {
                content.AddLink(new Link("add-extorno", Request.GetUrlHelper()
                    .Link("ExtornaTransacao", new {transacaoId = content.Id}), "Extorna essa Transacao"));
            }
        }
    }
}