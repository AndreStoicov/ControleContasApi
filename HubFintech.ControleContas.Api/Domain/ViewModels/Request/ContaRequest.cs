using System.Runtime.Serialization;

namespace HubFintech.ControleContas.Api.Domain.ViewModels.Request
{
    [DataContract]
    public class ContaRequest
    {
        [DataMember] public int PessoaId { get; set; }

        [DataMember] public string Nome { get; set; }

        public static Conta Cria(ContaRequest request, Pessoa pessoa)
        {
            return Conta.Criar(request.Nome, pessoa);
        }
    }
}