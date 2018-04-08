using System;
using FluentValidation;
using HubFintech.ControleContas.Api.Configuration;
using HubFintech.ControleContas.Api.Domain.Repositories.Interfaces;
using HubFintech.ControleContas.Api.Domain.ViewModels.Request;

namespace HubFintech.ControleContas.Api.Domain.Validators
{
    public class TransacaoExtornoRequestValidator : AbstractValidator<TransacaoExtornoRequest>
    {
        private readonly IGlobalContainerAccessor _globalContainerAccessor;

        public TransacaoExtornoRequestValidator(IGlobalContainerAccessor globalContainerAccessor)
        {
            _globalContainerAccessor = globalContainerAccessor;

            RuleFor(x => x.TransacaoId).Must(DeveExistirTransacao)
                .WithMessage("Transação Não encontrada ou já foi extornada");
            RuleFor(x => x.TransacaoId)
                .Must((model, transacaoid) => ExtornoAporteDeveConterCodigo(transacaoid, model.CodigoAporte))
                .WithMessage("Para extornar Aportes precisa conter um código válido");
        }

        private bool DeveExistirTransacao(int transacaoId)
        {
            var _transacaoRepository =
                _globalContainerAccessor.GetInstance(typeof(IBaseRepository<Transacao>)) as IBaseRepository<Transacao>;

            var transacao = _transacaoRepository.GetById(transacaoId);

            return transacao != null && !transacao.Extornado;
        }

        private bool ExtornoAporteDeveConterCodigo(int transacaoId, string codigoAporte)
        {
            var _transacaoRepository =
                _globalContainerAccessor.GetInstance(typeof(IBaseRepository<Transacao>)) as IBaseRepository<Transacao>;

            var transacao = _transacaoRepository.GetById(transacaoId);

            if (transacao != null && transacao.TipoTransacao == TipoTransacao.Aporte)
                return string.Equals(codigoAporte, transacao.CodigoAporte, StringComparison.InvariantCultureIgnoreCase);

            return true;
        }
    }
}