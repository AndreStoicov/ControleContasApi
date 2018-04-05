using System.Collections.Generic;
using System.Net;
using System.Runtime.Serialization;

namespace HubFintech.ControleContas.Api.Domain.ViewModels.Shared
{
    [DataContract]
    public class ApiResponse
    {
        [DataMember] public string Version => "1.0.0";

        [DataMember] public int StatusCode { get; set; }


        [DataMember(EmitDefaultValue = false)] 
        public IEnumerable<Mensagem> Erros { get; set; }

        [DataMember(EmitDefaultValue = false)] 
        public object Result { get; set; }

        public ApiResponse(HttpStatusCode statusCode, object result = null, IEnumerable<Mensagem> mensagem = null)
        {
            StatusCode = (int) statusCode;
            Result = result;
            Erros = mensagem;
        }
    }

    public class Mensagem
    {
        public Mensagem(string descricao)
        {
            Descricao = descricao;
        }

        public string Descricao { get; set; }
    }
}