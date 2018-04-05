using System.Collections.Generic;
using Fabrik.Common.WebAPI;
using Fabrik.Common.WebAPI.Links;
using System.Net.Http;

namespace HubFintech.ControleContas.Api.Domain.ViewModels.Response.Hypermedia
{
    public class ObtemPessoaEnricher : ObjectContentResponseEnricher<PessoaResponse>
    {
        public override void Enrich(PessoaResponse content)
        {
            content.AddLink(new SelfLink(Request.GetUrlHelper()
                .Link("ObtemPessoa", new {pessoaId = content.Id}), "Obtem Pessoa Por Id"));

            content.AddLink(new CollectionLink(Request.GetUrlHelper()
                .Link("ObtemTodasContas", new {pessoaId = content.Id}), "Obtem Todas as Contas dessa Pessoa"));

            content.AddLink(new Link("add-conta", Request.GetUrlHelper()
                .Link("CriaConta", new {pessoaId = content.Id}), "Cria uma Nova conta vinculada com essa Pessoa"));
        }
    }
}