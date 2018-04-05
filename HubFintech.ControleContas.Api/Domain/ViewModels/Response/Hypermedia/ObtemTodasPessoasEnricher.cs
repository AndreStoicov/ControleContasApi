using System.Collections.Generic;
using Fabrik.Common.WebAPI;
using Fabrik.Common.WebAPI.Links;
using System.Net.Http;

namespace HubFintech.ControleContas.Api.Domain.ViewModels.Response.Hypermedia
{
    public class ObtemTodasPessoasEnricher : ObjectContentResponseEnricher<List<PessoaResponse>>
    {
        public override void Enrich(List<PessoaResponse> content)
        {
            content.ForEach(x =>
            {
                x.AddLink(new SelfLink(Request.GetUrlHelper()
                    .Link("ObtemPessoa", new {pessoaId = x.Id}), "Obtem Pessoa Por Id"));
                
                x.AddLink(new CollectionLink(Request.GetUrlHelper()
                    .Link("ObtemTodasContas", new {pessoaId = x.Id}), "Obtem Todas as Contas dessa Pessoa"));

                x.AddLink(new Link("add-conta", Request.GetUrlHelper()
                    .Link("CriaConta", new {pessoaId = x.Id}), "Cria uma Nova conta vinculada com essa Pessoa"));
            });
        }
    }
}