using System;
using System.Collections.Generic;

namespace HubFintech.ControleContas.Api.Domain
{
    public class Conta
    {
        public Conta()
        {
            ContasFilha = new List<Conta>();
            TransacoesCredito = new List<Transacao>();
            TransacoesDebito = new List<Transacao>();
            GestaoSaldos = new List<GestaoSaldo>();

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