using System;
using System.Linq;
using HubFintech.ControleContas.Api.Domain.ViewModels.Shared;
using Newtonsoft.Json;

namespace HubFintech.ControleContas.Api.Domain.ViewModels.Response
{
    public class ContaResponse : Resource
    {
        public ContaResponse()
        {
            
        }

        public ContaResponse(Conta conta)
        {
            Id = conta.Id;
            PessoaId = conta.PessoaId;
            Nome = conta.Nome;
            TipoConta = conta.TipoConta == Domain.TipoConta.Filial ? "Filial" : "Matriz";
            DataCriacao = conta.DataCriacao;
            Ativo = conta.Ativo;
            Cancelada = conta.Cancelada;
            ContaPaiId = conta.ContaPaiId.GetValueOrDefault();
            ContemContasFilha = conta.ContasFilha.Any();

            var saldo = conta.GestaoSaldos.FirstOrDefault(x => x.SaldoCorrente);
            Saldo = saldo?.Saldo ?? 0;
        }
        
        public int Id { get; set; }
        
        [JsonIgnore]
        public int PessoaId { get; set; }
        
        public string Nome { get; set; }
        public string TipoConta { get; set; }
        public DateTime DataCriacao { get; set; }
        public bool Ativo { get; set; }
        public bool Cancelada { get; set; }
        public decimal Saldo { get; set; }
        
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int ContaPaiId { get; set; }
        
        public bool ContemContasFilha { get; set; }
    }
}