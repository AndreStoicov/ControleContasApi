using System;
using System.Collections.Generic;
using Fabrik.Common;

namespace HubFintech.ControleContas.Api.Domain
{
    public class Conta
    {
        protected Conta()
        {
            ContasFilha = new List<Conta>();
            TransacoesCredito = new List<Transacao>();
            TransacoesDebito = new List<Transacao>();
            GestaoSaldos = new List<GestaoSaldo>();
        }

        private Conta(string nome, TipoConta tipoConta, Pessoa pessoa)
        {
            Nome = nome;
            TipoConta = tipoConta;
            DataCriacao = DateTime.Now;
            Ativo = true;
            Cancelada = false;
            Pessoa = pessoa;
            PessoaId = pessoa.Id;
        }

        private Conta(string nome, TipoConta tipoConta, Pessoa pessoa, Conta contaPai) : this(nome, tipoConta, pessoa)
        {
            ContaPai = contaPai;
            ContaPaiId = contaPai.Id;
        }

        public static Conta Criar(string nome, Pessoa pessoa)
        {
            Ensure.Argument.NotNullOrEmpty(nome, nameof(nome));
            Ensure.Argument.NotNull(pessoa, nameof(pessoa));

            return new Conta(nome, TipoConta.Matriz, pessoa);
        }

        public static Conta CriarContaFilha(string nome, Pessoa pessoa, Conta contaPai)
        {
            Ensure.Argument.NotNullOrEmpty(nome, nameof(nome));
            Ensure.Argument.NotNull(pessoa, nameof(pessoa));
            Ensure.Argument.NotNull(contaPai, nameof(contaPai));

            return new Conta(nome, TipoConta.Filial, pessoa, contaPai);
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataCriacao { get; set; }

        public TipoConta TipoConta { get; set; }
        public int? ContaPaiId { get; set; }
        public bool Ativo { get; set; }
        public bool Cancelada { get; set; }

        public int PessoaId { get; set; }
        public virtual Pessoa Pessoa { get; set; }

        public virtual Conta ContaPai { get; set; }
        public virtual ICollection<Conta> ContasFilha { get; set; }

        public virtual ICollection<Transacao> TransacoesCredito { get; set; }
        public virtual ICollection<Transacao> TransacoesDebito { get; set; }
        public virtual ICollection<GestaoSaldo> GestaoSaldos { get; set; }
    }
}