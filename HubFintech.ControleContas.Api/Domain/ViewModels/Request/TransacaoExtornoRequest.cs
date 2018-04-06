using System.Runtime.Serialization;

namespace HubFintech.ControleContas.Api.Domain.ViewModels.Request
{
    [DataContract]
    public class TransacaoExtornoRequest
    {
        [DataMember]
        public int TransacaoId { get; set; }
        
        [DataMember]
        public string CodigoAporte { get; set; }
    }
}