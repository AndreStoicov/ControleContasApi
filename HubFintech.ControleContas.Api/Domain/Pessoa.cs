using System;
using System.Collections.Generic;
using System.Linq;
using Fabrik.Common;
using HubFintech.ControleContas.Api.Domain.Helper;

namespace HubFintech.ControleContas.Api.Domain
{
    public abstract class Pessoa
    {
        private ICollection<Conta> _contas;

        protected Pessoa()
        {
            _contas = new List<Conta>();
        }

        public int Id { get; protected set; }
        public virtual ICollection<Conta> Contas => _contas;

        public void AdicionarConta(Conta conta)
        {
            Ensure.Argument.NotNull(conta, nameof(conta));

            if (_contas == null)
                _contas = new List<Conta>();

            if (_contas.Any(x => x.Id == conta.Id))
                RemoverConta(conta);

            _contas.Add(conta);
        }

        public void RemoverConta(Conta conta)
        {
            Ensure.Argument.NotNull(conta, nameof(conta));

            if (_contas == null)
                _contas = new List<Conta>();

            var contaAntigo = _contas.FirstOrDefault(x => x.Id == conta.Id);

            Ensure.Argument.NotNull(contaAntigo, nameof(contaAntigo));

            _contas.Remove(contaAntigo);
        }
    }

    public class PessoaFisica : Pessoa
    {
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

        public string Cpf { get; protected set; }
        public string NomeCompleto { get; protected set; }
        public DateTime DataNascimento { get; protected set; }
    }

    public class PessoaJuridica : Pessoa
    {
        public PessoaJuridica(string cnpj, string razaoSocial, string nomeFantasia)
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

        public string Cnpj { get; protected set; }
        public string RazaoSocial { get; protected set; }
        public string NomeFantasia { get; protected set; }
    }
}