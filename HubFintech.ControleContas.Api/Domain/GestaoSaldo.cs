using System;
using Fabrik.Common;

namespace HubFintech.ControleContas.Api.Domain
{
    public class GestaoSaldo
    {
        public GestaoSaldo(Conta conta, Transacao transacao)
        {
            Conta = conta;
            ContaId = conta.Id;
            Transacao = transacao;
            TransacaoId = transacao.Id;
            DataCriacao = DateTime.Now;
            SaldoCorrente = true;
        }

        public static GestaoSaldo Criar(Conta conta, Transacao transacao)
        {
            Ensure.Argument.NotNull(conta, nameof(conta));
            Ensure.Argument.NotNull(transacao, nameof(transacao));

            return new GestaoSaldo(conta, transacao);
        }

        public int Id { get; set; }

        public int ContaId { get; set; }
        public virtual Conta Conta { get; set; }

        public int TransacaoId { get; set; }
        public virtual Transacao Transacao { get; set; }

        public decimal Saldo { get; set; }
        public bool SaldoCorrente { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}