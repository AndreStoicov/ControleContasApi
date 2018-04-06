using FluentValidation;
using HubFintech.ControleContas.Api.Configuration;
using HubFintech.ControleContas.Api.Domain.Repositories.Interfaces;
using HubFintech.ControleContas.Api.Domain.ViewModels.Request;

namespace HubFintech.ControleContas.Api.Domain.Validators
{
    public class ContaFilhaRequestValidator : AbstractValidator<ContaFilhaRequest>
    {
        private readonly IGlobalContainerAccessor _globalContainerAccessor;

        public ContaFilhaRequestValidator(IGlobalContainerAccessor globalContainerAccessor)
        {
            _globalContainerAccessor = globalContainerAccessor;

            RuleFor(x => x.ContaPaiId).Must(ContaDeveExistir).WithMessage("Conta informada não existe");
            RuleFor(x => x.PessoaId).Must(PessoaDeveExistir).WithMessage("Pessoa informada não existe");
            RuleFor(x => x.Nome).NotEmpty().WithMessage("Nome da Conta é obrigatório");
        }

        private bool ContaDeveExistir(int contaId)
        {
            var _contaRepository =
                _globalContainerAccessor.GetInstance(typeof(IBaseRepository<Conta>)) as IBaseRepository<Conta>;
            var conta = _contaRepository.GetById(contaId);

            return conta == null;
        }

        private bool PessoaDeveExistir(int pessoaId)
        {
            var _pessoaRepository =
                _globalContainerAccessor.GetInstance(typeof(IBaseRepository<Pessoa>)) as IBaseRepository<Pessoa>;
            var pessoa = _pessoaRepository.GetById(pessoaId);

            return pessoa == null;
        }
    }
}