using FluentValidation;
using HubFintech.ControleContas.Api.Configuration;
using HubFintech.ControleContas.Api.Domain.Helper;
using HubFintech.ControleContas.Api.Domain.Services.Interfaces;
using HubFintech.ControleContas.Api.Domain.ViewModels.Request;

namespace HubFintech.ControleContas.Api.Domain.Validators
{
    public class PessoaRequestValidator : AbstractValidator<PessoaRequest>
    {
        private readonly IGlobalContainerAccessor _globalContainerAccessor;

        public PessoaRequestValidator(IGlobalContainerAccessor globalContainerAccessor)
        {
            _globalContainerAccessor = globalContainerAccessor;

            When(x => x.PessoaFisica != null, () =>
            {
                RuleFor(x => x.PessoaJuridica).Null()
                    .WithMessage("Não é possivel Adicionar uma Pessoa Fisica e Juridica ao mesmo tempo.");

                RuleFor(x => x.PessoaFisica.Cpf).Must(ValidadorDocumento.HasValidCpf)
                    .WithMessage("CPF precisa ser válido");

                RuleFor(x => x.PessoaFisica.Cpf).Must(PessoaFisicaCpfUnico)
                    .WithMessage("Já existe uma Pessoa com o CPF informado");

                RuleFor(x => x.PessoaFisica.NomeCompleto).NotEmpty().WithMessage("Nome Completo é obrigatório");
                RuleFor(x => x.PessoaFisica.DataNascimento).NotEmpty().WithMessage("Data Nascimento é obrigatório");
            });

            When(x => x.PessoaJuridica != null, () =>
            {
                RuleFor(x => x.PessoaFisica).Null()
                    .WithMessage("Não é possivel Adicionar uma Pessoa Fisica e Juridica ao mesmo tempo.");

                RuleFor(x => x.PessoaJuridica.Cnpj).Must(ValidadorDocumento.HasValidCnpj)
                    .WithMessage("CNPJ precisa ser válido");

                RuleFor(x => x.PessoaJuridica.Cnpj).Must(PessoaFisicaCnpjUnico)
                    .WithMessage("Já existe uma Pessoa com o CNPJ informado");

                RuleFor(x => x.PessoaJuridica.RazaoSocial).NotEmpty().WithMessage("RazaoSocial é obrigatório");
                RuleFor(x => x.PessoaJuridica.NomeFantasia).NotEmpty().WithMessage("NomeFantasia é obrigatório");
            });
        }

        private bool PessoaFisicaCpfUnico(string cpf)
        {
            var _pessoaService = _globalContainerAccessor.GetInstance(typeof(IPessoaService)) as IPessoaService;
            var pessoa = _pessoaService.ObtemPessoaPorCpf(cpf);

            return pessoa == null;
        }

        private bool PessoaFisicaCnpjUnico(string cnpj)
        {
            var _pessoaService = _globalContainerAccessor.GetInstance(typeof(IPessoaService)) as IPessoaService;
            var pessoa = _pessoaService.ObtemPessoaPorCnpj(cnpj);

            return pessoa == null;
        }
    }
}