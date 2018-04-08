using FluentValidation;
using HubFintech.ControleContas.Api.Configuration;
using HubFintech.ControleContas.Api.Domain.Repositories.Interfaces;
using HubFintech.ControleContas.Api.Domain.ViewModels.Request;

namespace HubFintech.ControleContas.Api.Domain.Validators
{
    public class TransacaoAporteRequestValidator : AbstractValidator<TransacaoAporteRequest>
    {
        private readonly IGlobalContainerAccessor _globalContainerAccessor;

        public TransacaoAporteRequestValidator(IGlobalContainerAccessor globalContainerAccessor)
        {
            _globalContainerAccessor = globalContainerAccessor;

            RuleFor(x => x.ContaDestinoId).Must(ContaDeveExistir)
                .WithMessage("Conta não existe ou está Inativa/Cancelada");

            RuleFor(x => x.Valor).Must(x => x > 0).WithMessage("Valor deve ser maior que zero.");
        }

        private bool ContaDeveExistir(int contaId)
        {
            var _contaRepository =
                _globalContainerAccessor.GetInstance(typeof(IBaseRepository<Conta>)) as IBaseRepository<Conta>;

            var conta = _contaRepository.GetById(contaId);

            return conta != null && conta.Ativo && !conta.Cancelada;
        }
    }
}