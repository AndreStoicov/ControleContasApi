using System;
using System.Collections.Generic;

namespace HubFintech.ControleContas.Api.Domain
{
    public abstract class Pessoa
    {
        public Pessoa()
        {
            Contas = new List<Conta>();
        }

        public int Id { get; set; }
        public virtual ICollection<Conta> Contas { get; set; }
    }

    public class PessoaFisica : Pessoa
    {
        public string Cpf { get; set; }
        public string NomeCompleto { get; set; }
        public DateTime DataNascimento { get; set; }
    }

    public class PessoaJuridica : Pessoa
    {
        public string Cnpj { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
    }
}