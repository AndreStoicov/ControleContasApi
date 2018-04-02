using System;

namespace HubFintech.ControleContas.Api.Domain
{
    public class Conta
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}