using FluentValidation;
using HubFintech.ControleContas.Api.Configuration;
using HubFintech.ControleContas.Api.Domain.Repositories.Interfaces;
using HubFintech.ControleContas.Api.Domain.ViewModels.Request;

namespace HubFintech.ControleContas.Api.Domain.Validators
{
    public class TransacaoTransferenciaRequestValidator : AbstractValidator<TransacaoTransferenciaRequest>
    {
        private readonly IGlobalContainerAccessor _globalContainerAccessor;

        public TransacaoTransferenciaRequestValidator(IGlobalContainerAccessor globalContainerAccessor)
        {
            _globalContainerAccessor = globalContainerAccessor;

            RuleFor(x => x.ContaDestinoId).Must((model, destino) => destino != model.ContaOrigemId)
                .WithMessage("Conta Destino não pode ser a mesma que Origem");

            RuleFor(x => x.ContaDestinoId).Must(ContaDeveExistir)
                .WithMessage("Conta Destino não existe ou está Inativa/Cancelada");

            RuleFor(x => x.ContaDestinoId).Must(ContaDestinoNaoPodeSerMatriz)
                .WithMessage("Conta Destino não pode ser do Tipo Matriz");

            RuleFor(x => x.ContaOrigemId).Must(ContaDeveExistir)
                .WithMessage("Conta Origem não existe ou está Inativa/Cancelada");

            RuleFor(x => x.Valor).Must(x => x > 0).WithMessage("Valor deve ser maior que zero.");
        }

        private bool ContaDeveExistir(int contaId)
        {
            var _contaRepository =
                _globalContainerAccessor.GetInstance(typeof(IBaseRepository<Conta>)) as IBaseRepository<Conta>;

            var conta = _contaRepository.GetById(contaId);

            return conta != null && conta.Ativo && !conta.Cancelada;
        }

        private bool ContaDestinoNaoPodeSerMatriz(int contaId)
        {
            var _contaRepository =
                _globalContainerAccessor.GetInstance(typeof(IBaseRepository<Conta>)) as IBaseRepository<Conta>;

            var conta = _contaRepository.GetById(contaId);

            return conta != null && conta.TipoConta == TipoConta.Filial;
        }
    }
}