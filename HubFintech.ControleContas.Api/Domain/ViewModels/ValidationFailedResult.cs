using System;
using Newtonsoft.Json;

namespace HubFintech.ControleContas.Api.Domain.ViewModels
{
    public class ValidationFailedResult
    {
        public static string ToErrorMessage(string erro, Object valorInformado)
        {
            return ToErrorMessage(string.Empty, erro, valorInformado);
        }

        public static string ToErrorMessage(string codigo, string erro, Object valorInformado)
        {
            var validacao = new Mensagem(codigo, erro, valorInformado);
            return JsonConvert.SerializeObject(validacao);
        }

        public static Mensagem ToErrorObject(string errorMessage)
        {
            var validacao = JsonConvert.DeserializeObject<Mensagem>(errorMessage);
            return validacao;
        }
    }
}