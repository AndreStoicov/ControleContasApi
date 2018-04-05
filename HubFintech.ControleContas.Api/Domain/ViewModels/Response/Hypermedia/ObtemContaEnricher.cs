using System.Net.Http;
using Fabrik.Common.WebAPI;
using Fabrik.Common.WebAPI.Links;

namespace HubFintech.ControleContas.Api.Domain.ViewModels.Response.Hypermedia
{
    public class ObtemContaEnricher : ObjectContentResponseEnricher<ContaResponse>
    {
        public override void Enrich(ContaResponse content)
        {
            content.AddLink(new SelfLink(Request.GetUrlHelper()
                .Link("ObtemConta", new {pessoaId = content.PessoaId, contaId = content.Id}), "Obtem Conta Por Id"));

            content.AddLink(new RelatedLink(Request.GetUrlHelper()
                .Link("ObtemPessoa", new {pessoaId = content.PessoaId}), "Obtem Pessoa Por Id"));

            if (content.ContemContasFilha)
            {
                content.AddLink(new CollectionLink(Request.GetUrlHelper()
                        .Link("ObtemTodasContasFilhas", new {pessoaId = content.PessoaId, contaId = content.Id}),
                    "Obtem Todas as Contas Filhas dessa Conta"));
            }

            content.AddLink(new Link("add-transferencia", Request.GetUrlHelper()
                    .Link("CriaTrasferencia", new {contaId = content.Id}),
                "Cria uma Nova Transferencia com essa Conta como Origem"));

            if (content.TipoConta == "Matriz")
            {
                content.AddLink(new Link("add-Aporte", Request.GetUrlHelper()
                        .Link("CriaAporte", new {contaId = content.Id}),
                    "Cria uma Novo Aporte com essa Conta como Destino"));
            }
        }
    }
}