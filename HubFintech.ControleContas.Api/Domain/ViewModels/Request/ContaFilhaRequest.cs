using System.Runtime.Serialization;

namespace HubFintech.ControleContas.Api.Domain.ViewModels.Request
{
    [DataContract]
    public class ContaFilhaRequest : ContaRequest
    {
        [DataMember] public int ContaPaiId { get; set; }

        public static Conta Cria(ContaRequest request, Pessoa pessoa, Conta contaPai)
        {
            return Conta.CriarContaFilha(request.Nome, pessoa, contaPai);
        }
    }
}