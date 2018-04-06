using FluentValidation;
using HubFintech.ControleContas.Api.Configuration;
using HubFintech.ControleContas.Api.Domain.Repositories.Interfaces;
using HubFintech.ControleContas.Api.Domain.ViewModels.Request;

namespace HubFintech.ControleContas.Api.Domain.Validators
{
    public class ContaRequestValidator : AbstractValidator<ContaRequest>
    {
        private readonly IGlobalContainerAccessor _globalContainerAccessor;

        public ContaRequestValidator(IGlobalContainerAccessor globalContainerAccessor)
        {
            _globalContainerAccessor = globalContainerAccessor;

            RuleFor(x => x.PessoaId).Must(PessoaDeveExistir).WithMessage("Pessoa informada não existe");
            RuleFor(x => x.Nome).NotEmpty().WithMessage("Nome da Conta é obrigatório");
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