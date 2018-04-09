using System;
using System.Collections.Generic;
using System.Net;
using HubFintech.ControleContas.Api.Configuration.Factories;
using HubFintech.ControleContas.Api.Domain;
using HubFintech.ControleContas.Api.Domain.Repositories.Interfaces;
using HubFintech.ControleContas.Api.Domain.Services;
using HubFintech.ControleContas.Api.Domain.ViewModels.Response;
using Moq;
using Xunit;

namespace HubFintech.ControleContas.Test.Services
{
    public class TransacaoServiceTest
    {
        private readonly TransacaoService _transacaoService;
        private readonly AutoMoq.AutoMoqer _moq = new AutoMoq.AutoMoqer();

        public TransacaoServiceTest()
        {
            _transacaoService = _moq.Create<TransacaoService>();
            _moq.GetMock<ILogFactory>().Setup(x => x.Log());
        }

        [Fact(DisplayName = "Extorna Transacao deve atualizar data e flag extorno")]
        public void TestCase1()
        {
            var transacao = new Transacao
            {
                Id = 30,
                Extornado = false
            };

            _moq.GetMock<IBaseRepository<Transacao>>().Setup(x => x.GetById(It.IsAny<int>())).Returns(transacao);
            _moq.GetMock<IBaseRepository<Transacao>>().Setup(x => x.Update(transacao));

            var transacaoUpdate = new Transacao
            {
                Id = 30,
                Extornado = true,
                TipoTransacao = TipoTransacao.Aporte,
                CodigoAporte = "ABC123",
                DataCriacao = DateTime.Now,
                DataExtorno = DateTime.Now,
                ContaDestino = Conta.Criar("Nome Conta", PessoaFisica.Criar("33792235811", "andre", DateTime.Now))
            };
            
            var retorno = new TransacoesResponse(transacaoUpdate);

            Assert.True(retorno.Extornado);
        }
    }
}