using System;

namespace HubFintech.ControleContas.Api.Domain.ViewModels.Shared
{
    public class PessoaFisicaViewModel
    {
        public PessoaFisicaViewModel()
        {
            
        }
        public PessoaFisicaViewModel(PessoaFisica pessoa)
        {
            Cpf = pessoa.Cpf;
            NomeCompleto = pessoa.NomeCompleto;
            DataNascimento = pessoa.DataNascimento;
        }
        
        public string Cpf { get; set; }
        public string NomeCompleto { get; set; }
        public DateTime DataNascimento { get; set; }
    }
}