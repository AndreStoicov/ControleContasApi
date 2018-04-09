using System;
using System.Linq;
using Fabrik.Common;

namespace HubFintech.ControleContas.Api.Domain
{
    public class Transacao
    {
        public Transacao()
        {
        }

        private Transacao(TipoTransacao tipoTransacao, Conta contaDestino, decimal valor)
        {
            TipoTransacao = tipoTransacao;
            ContaDestino = contaDestino;
            ContaDestinoId = contaDestino.Id;
            Valor = valor;
            Extornado = false;
            DataCriacao = DateTime.Now;
        }

        public static Transacao CriaAporte(Conta conta, decimal valor)
        {
            Ensure.Argument.NotNull(conta, nameof(conta));
            Ensure.Argument.IsNot(valor <= 0, nameof(valor));

            var transacao = new Transacao(TipoTransacao.Aporte, conta, valor)
            {
                CodigoAporte = RandomString()
            };

            return transacao;
        }

        public static Transacao CriaTransferencia(Conta contaOrigem, Conta contaDestino, decimal valor)
        {
            Ensure.Argument.NotNull(contaOrigem, nameof(contaOrigem));
            Ensure.Argument.NotNull(contaDestino, nameof(contaDestino));
            Ensure.Argument.IsNot(valor <= 0, nameof(valor));

            var transacao = new Transacao(TipoTransacao.Transferencia, contaDestino, valor)
            {
                ContaOrigem = contaOrigem,
                ContaOrigemId = contaOrigem.Id
            };

            return transacao;
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

        private static readonly Random Random = new Random();

        private static string RandomString()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 8)
                .Select(s => s[Random.Next(s.Length)]).ToArray());
        }
    }
}