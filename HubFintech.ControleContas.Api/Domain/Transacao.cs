using System;
using System.Collections.Generic;

namespace HubFintech.ControleContas.Api.Domain
{
    public class Transacao
    {
        public Transacao(TipoTransacao tipoTransacao, Conta contaDestino, decimal valor)
        {
            TipoTransacao = tipoTransacao;
            ContaDestino = contaDestino;
            ContaDestinoId = contaDestino.Id;
            Valor = valor;
            Extornado = false;
            DataCriacao = DateTime.Now;
        }

        public static Transacao ExtornaTransacao(Transacao transacao)
        {
            if(transacao.TipoTransacao == TipoTransacao.Aporte)
        }
        
        public int Id { get; set; }
        public TipoTransacao TipoTransacao { get; set; }

        public int? ContaOrigemId { get; set; }
        public virtual Conta ContaOrigem { get; set; }

        public int ContaDestinoId { get; set; }
        public virtual Conta ContaDestino { get; set; }

        public string CodigoAporte { get; set; }
        public decimal Valor { get; set; }
        public bool Extornado { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataExtorno { get; set; }
        
        public virtual List<GestaoSaldo> GestaoSaldo { get; set; }
    }
}