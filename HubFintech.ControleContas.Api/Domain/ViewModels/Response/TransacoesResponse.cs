using System;
using HubFintech.ControleContas.Api.Domain.ViewModels.Shared;
using Newtonsoft.Json;

namespace HubFintech.ControleContas.Api.Domain.ViewModels.Response
{
    public class TransacoesResponse : Resource
    {
        public TransacoesResponse()
        {
        }
        
        public TransacoesResponse(Transacao transacao)
        {
            Id = transacao.Id;
            TipoTransacao = transacao.TipoTransacao == Domain.TipoTransacao.Aporte ? "Aporte" : "Transferencia";
            
            if(transacao.TipoTransacao == Domain.TipoTransacao.Transferencia)
                ContaOrigem = new TransacaoConta(transacao.ContaOrigem);
            
            ContaDestino = new TransacaoConta(transacao.ContaDestino);

            if (transacao.TipoTransacao == Domain.TipoTransacao.Aporte)
                CodigoAporte = transacao.CodigoAporte;

            Valor = transacao.Valor;
            Extornado = transacao.Extornado;
            DataCriacao = transacao.DataCriacao;
            DataExtorno = transacao.DataExtorno;

        }
        
        public int Id { get; set; }
        public string TipoTransacao { get; set; }
        
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public TransacaoConta ContaOrigem { get; set; }
        
        public TransacaoConta ContaDestino { get; set; }
        
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string CodigoAporte { get; set; }
        
        public decimal Valor { get; set; }
        public bool Extornado { get; set; }
        public DateTime DataCriacao { get; set; }
        
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public DateTime? DataExtorno { get; set; }
    }

    public class TransacaoConta
    {
        public TransacaoConta()
        {
            
        }

        public TransacaoConta(Conta conta)
        {
            Id = conta.Id;
            Nome = conta.Nome;
        }
        public int Id { get; set; }
        public string Nome { get; set; }
    }
}