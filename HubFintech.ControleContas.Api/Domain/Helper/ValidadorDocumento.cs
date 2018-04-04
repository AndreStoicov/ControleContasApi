using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace HubFintech.ControleContas.Api.Domain.Helper
{
    public static class ValidadorDocumento
    {
        public static string RemoveMascara(this string documento)
        {
            return string.IsNullOrEmpty(documento) ? string.Empty : Regex.Replace(documento, RegexHelper.MatchAllButNumbers, string.Empty);
        }

        public static string TruncateCPF(this string value)
        {
            return value.Length <= 11 ? value : value.Substring(0, 11);
        }

        public static bool HasValidCnpj(string cnpj)
        {
            if (string.IsNullOrWhiteSpace(cnpj))
                return false;

            var multiplicador1 = new[] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            var multiplicador2 = new[] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            var documento = cnpj.Trim();
            documento = Regex.Replace(documento, RegexHelper.MatchAllButNumbers, string.Empty);

            if (documento.Length != 14 || BlackListCnpj.Contains(documento))
                return false;

            var tempCnpj = documento.Substring(0, 12);
            var soma = 0;

            for (var i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

            var resto = (soma % 11);

            resto = resto < 2 ? 0 : 11 - resto;

            var digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;

            for (var i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

            resto = (soma % 11);

            resto = resto < 2 ? 0 : 11 - resto;

            digito = digito + resto;

            return documento.EndsWith(digito);
        }

        public static bool HasValidCpf(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
                return false;

            var multiplicador1 = new[] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            var multiplicador2 = new[] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            var documento = cpf.Trim();
            documento = Regex.Replace(documento, RegexHelper.MatchAllButNumbers, string.Empty);

            if (string.IsNullOrEmpty(documento))
                return false;

            if (documento.Length != 11 || BlackListCpf.Contains(documento))
                return false;

            var tempCpf = documento.Substring(0, 9);
            var soma = 0;

            for (var i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            var resto = soma % 11;

            resto = resto < 2 ? 0 : 11 - resto;

            var digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;

            for (var i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;

            resto = resto < 2 ? 0 : 11 - resto;

            digito = digito + resto;
            return documento.EndsWith(digito);
        }

        private static List<string> BlackListCpf => new List<string>
        {
            "00000000000",
            "11111111111",
            "22222222222",
            "33333333333",
            "44444444444",
            "55555555555",
            "66666666666",
            "77777777777",
            "88888888888",
            "99999999999"
        };

        private static List<string> BlackListCnpj => new List<string>
        {
            "00000000000000",
            "11111111111111",
            "22222222222222",
            "33333333333333",
            "44444444444444",
            "55555555555555",
            "66666666666666",
            "77777777777777",
            "88888888888888",
            "99999999999999"
        };
    }
}