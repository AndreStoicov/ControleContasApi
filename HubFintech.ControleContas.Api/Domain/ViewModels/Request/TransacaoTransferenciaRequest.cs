using System.Runtime.Serialization;

namespace HubFintech.ControleContas.Api.Domain.ViewModels.Request
{
    [DataContract]
    public class TransacaoTransferenciaRequest
    {
        [DataMember] 
        public int ContaOrigemId { get; set; }
        [DataMember] 
        public int ContaDestinoId { get; set; }
        [DataMember] 
        public decimal Valor { get; set; }
    }
}