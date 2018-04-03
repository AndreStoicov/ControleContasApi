using System;
using System.Collections.Generic;

namespace HubFintech.ControleContas.Api.Domain
{
    public class Conta
    {
        public Conta()
        {
            ContasFilha = new List<Conta>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }

        private string _dataCriacao;

        public DateTime DataCriacao { get; set; }

        public TipoConta TipoConta { get; set; }
        public decimal Saldo { get; set; }
        public int? ContaPaiId { get; set; }
        public bool Ativo { get; set; }
        public bool Cancelada { get; set; }

        public int PessoaId { get; set; }
        public virtual Pessoa Pessoa { get; set; }

        public virtual Conta ContaPai { get; set; }
        public virtual ICollection<Conta> ContasFilha { get; set; }
    }
}