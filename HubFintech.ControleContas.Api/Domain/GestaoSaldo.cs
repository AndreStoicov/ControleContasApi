using System;

namespace HubFintech.ControleContas.Api.Domain
{
    public class GestaoSaldo
    {
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