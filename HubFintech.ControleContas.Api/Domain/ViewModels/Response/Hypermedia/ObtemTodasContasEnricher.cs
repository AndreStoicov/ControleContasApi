using System.Collections.Generic;
using System.Net.Http;
using Fabrik.Common.WebAPI;
using Fabrik.Common.WebAPI.Links;

namespace HubFintech.ControleContas.Api.Domain.ViewModels.Response.Hypermedia
{
    public class ObtemTodasContasEnricher : ObjectContentResponseEnricher<List<ContaResponse>>
    {
        public override void Enrich(List<ContaResponse> content)
        {
            content.ForEach(x =>
            {
                x.AddLink(new SelfLink(Request.GetUrlHelper()
                    .Link("ObtemConta", new {pessoaId = x.PessoaId, contaId = x.Id}), "Obtem Conta Por Id"));

                x.AddLink(new RelatedLink(Request.GetUrlHelper()
                    .Link("ObtemPessoa", new {pessoaId = x.PessoaId}), "Obtem Pessoa Por Id"));

                if (x.ContemContasFilha)
                {
                    x.AddLink(new CollectionLink(Request.GetUrlHelper()
                            .Link("ObtemTodasContasFilhas", new {pessoaId = x.PessoaId, contaId = x.Id}),
                        "Obtem Todas as Contas Filhas dessa Conta"));
                }

                x.AddLink(new Link("add-transferencia", Request.GetUrlHelper()
                        .Link("CriaTrasferencia", new {contaId = x.Id}),
                    "Cria uma Nova Transferencia com essa Conta como Origem"));

                if (x.TipoConta == "Matriz")
                {
                    x.AddLink(new Link("add-Aporte", Request.GetUrlHelper()
                            .Link("CriaAporte", new {contaId = x.Id}),
                        "Cria uma Novo Aporte com essa Conta como Destino"));
                }
            });
        }
    }
}