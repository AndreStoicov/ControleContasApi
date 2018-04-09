using System;
using System.Net;
using FluentValidation.TestHelper;
using HubFintech.ControleContas.Api.Configuration;
using HubFintech.ControleContas.Api.Domain;
using HubFintech.ControleContas.Api.Domain.Repositories.Interfaces;
using HubFintech.ControleContas.Api.Domain.Validators;
using Moq;
using Xunit;

namespace HubFintech.ControleContas.Test.Validators
{
    public class TransacaoAporteRequestValidatorTest
    {
        private readonly TransacaoAporteRequestValidator _validator;
        private readonly AutoMoq.AutoMoqer _moq = new AutoMoq.AutoMoqer();

        public TransacaoAporteRequestValidatorTest()
        {
            _validator = _moq.Create<TransacaoAporteRequestValidator>();

            _moq.GetMock<IGlobalContainerAccessor>()
                .Setup(x => x.GetInstance(It.Is<Type>(y => y == typeof(IBaseRepository<Conta>))))
                .Returns(_moq.GetMock<IBaseRepository<Conta>>().Object);
        }

        [Fact(DisplayName = "Deve validar Valor")]
        public void Test_Case1()
        {
            _validator.ShouldNotHaveValidationErrorFor(x => x.Valor, 1);

            _validator.ShouldHaveValidationErrorFor(x => x.Valor, 0)
                .WithErrorMessage("Valor deve ser maior que zero.");
            _validator.ShouldHaveValidationErrorFor(x => x.Valor, -10)
                .WithErrorMessage("Valor deve ser maior que zero.");
        }

        [Fact(DisplayName = "Deve validar Conta Destino nula")]
        public void Test_Case2()
        {
            Conta conta = null;
            _moq.GetMock<IBaseRepository<Conta>>().Setup(x => x.GetById(It.IsAny<int>())).Returns(conta);

            _validator.ShouldHaveValidationErrorFor(x => x.ContaDestinoId, 30)
                .WithErrorMessage("Conta não existe ou está Inativa/Cancelada");
        }

        [Fact(DisplayName = "Deve validar Conta Destino Inativa")]
        public void Test_Case3()
        {
            var conta = Conta.Criar("Nome Conta", PessoaFisica.Criar("33792235811", "andre", DateTime.Now));
            conta.Ativo = false;
            conta.Cancelada = false;

            _moq.GetMock<IBaseRepository<Conta>>().Setup(x => x.GetById(It.IsAny<int>())).Returns(conta);

            _validator.ShouldHaveValidationErrorFor(x => x.ContaDestinoId, 30)
                .WithErrorMessage("Conta não existe ou está Inativa/Cancelada");
        }

        [Fact(DisplayName = "Deve validar Conta Destino Cancelada")]
        public void Test_Case4()
        {
            var conta = Conta.Criar("Nome Conta", PessoaFisica.Criar("33792235811", "andre", DateTime.Now));
            conta.Ativo = true;
            conta.Cancelada = true;

            _moq.GetMock<IBaseRepository<Conta>>().Setup(x => x.GetById(It.IsAny<int>())).Returns(conta);

            _validator.ShouldHaveValidationErrorFor(x => x.ContaDestinoId, 30)
                .WithErrorMessage("Conta não existe ou está Inativa/Cancelada");
        }

        [Fact(DisplayName = "Não Deve validar Conta Destino Cancelada")]
        public void Test_Case5()
        {
            var conta = Conta.Criar("Nome Conta", PessoaFisica.Criar("33792235811", "andre", DateTime.Now));
            conta.Ativo = true;
            conta.Cancelada = false;

            _moq.GetMock<IBaseRepository<Conta>>().Setup(x => x.GetById(It.IsAny<int>())).Returns(conta);

            _validator.ShouldNotHaveValidationErrorFor(x => x.ContaDestinoId, 30);
        }
    }
}