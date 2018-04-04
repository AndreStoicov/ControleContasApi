using System;

namespace HubFintech.ControleContas.Api.Domain.ViewModels.Response
{
    public class PessoaFisicaResponse
    {
        public string Cpf { get; set; }
        public string NomeCompleto { get; set; }
        public DateTime DataNascimento { get; set; }
    }
}