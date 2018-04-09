using System;
using System.Collections.Generic;
using System.Linq;
using Fabrik.Common;
using HubFintech.ControleContas.Api.Domain.Helper;

namespace HubFintech.ControleContas.Api.Domain
{
    public abstract class Pessoa
    {
        public Pessoa()
        {
            Contas = new List<Conta>();
        }

        public int Id { get; set; }
        public virtual ICollection<Conta> Contas { get; }
    }

    public class PessoaFisica : Pessoa
    {
        protected PessoaFisica()
        {
        }

        private PessoaFisica(string cpf, string nomeCompleto, DateTime dataNascimento)
        {
            Cpf = cpf;
            NomeCompleto = nomeCompleto;
            DataNascimento = dataNascimento;
        }

        public static PessoaFisica Criar(string cpf, string nomeCompleto, DateTime dataNascimento)
        {
            if (!ValidadorDocumento.HasValidCpf(cpf))
                throw new ArgumentException(nameof(cpf));

            Ensure.Argument.NotNullOrEmpty(nomeCompleto, nameof(nomeCompleto));
            Ensure.Argument.IsNot(dataNascimento <= DateTime.MinValue || dataNascimento >= DateTime.MaxValue,
                "Data Nascimento precisa ter um valor válido");

            return new PessoaFisica(cpf, nomeCompleto, dataNascimento);
        }

        public string Cpf { get; private set; }
        public string NomeCompleto { get; private set; }
        public DateTime DataNascimento { get; private set; }
    }

    public class PessoaJuridica : Pessoa
    {
        protected PessoaJuridica()
        {
        }

        private PessoaJuridica(string cnpj, string razaoSocial, string nomeFantasia)
        {
            Cnpj = cnpj;
            RazaoSocial = razaoSocial;
            NomeFantasia = nomeFantasia;
        }

        public static PessoaJuridica Criar(string cnpj, string razaoSocial, string nomeFantasia)
        {
            if (!ValidadorDocumento.HasValidCnpj(cnpj))
                throw new ArgumentException(nameof(cnpj));

            Ensure.Argument.NotNullOrEmpty(razaoSocial, nameof(razaoSocial));
            Ensure.Argument.NotNullOrEmpty(nomeFantasia, nameof(nomeFantasia));

            return new PessoaJuridica(cnpj, razaoSocial, nomeFantasia);
        }

        public string Cnpj { get; private set; }
        public string RazaoSocial { get; private set; }
        public string NomeFantasia { get; private set; }
    }
}