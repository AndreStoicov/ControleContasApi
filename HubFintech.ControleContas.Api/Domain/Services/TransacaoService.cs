using System;
using System.Collections.Generic;
using System.Linq;
using Fabrik.Common;
using HubFintech.ControleContas.Api.Configuration.Factories;
using HubFintech.ControleContas.Api.Domain.Repositories.Interfaces;
using HubFintech.ControleContas.Api.Domain.Services.Interfaces;
using HubFintech.ControleContas.Api.Domain.ViewModels.Request;
using HubFintech.ControleContas.Api.Domain.ViewModels.Response;

namespace HubFintech.ControleContas.Api.Domain.Services
{
    public class TransacaoService : ITransacaoService
    {
        private readonly ILogFactory _logFactory;
        private readonly IBaseRepository<Transacao> _transacaoRepository;

        public TransacaoService(ILogFactory logFactory, IBaseRepository<Transacao> transacaoRepository)
        {
            _logFactory = logFactory;
            _transacaoRepository = transacaoRepository;
        }

        public List<TransacoesResponse> ObtemTodasTransacoes(int contaId)
        {
            List<TransacoesResponse> retorno;
            try
            {
                var transacoes =
                    _transacaoRepository.Find(x => x.ContaOrigemId == contaId || x.ContaDestinoId == contaId).ToList();

                retorno = transacoes.Select(x => new TransacoesResponse(x)).ToList();
            }
            catch (Exception ex)
            {
                _logFactory.Log().Error($"Erro ao ObtemTodasTransacoes. {ex.Message} - {ex.StackTrace}");
                throw;
            }

            return retorno;
        }

        public TransacoesResponse ObtemTransacao(int transacaoId)
        {
            TransacoesResponse retorno;
            try
            {
                var transacao = _transacaoRepository.GetById(transacaoId);

                Ensure.NotNull(transacao, $"Não Existe nenhuma transação de Id {transacaoId}");

                retorno = new TransacoesResponse(transacao);
            }
            catch (Exception ex)
            {
                _logFactory.Log().Error($"Erro ao ObtemTransacao. {ex.Message} - {ex.StackTrace}");
                throw;
            }

            return retorno;
        }

        public TransacoesResponse ExtornaTransacao(TransacaoExtornoRequest input)
        {
            TransacoesResponse retorno;
            try
            {
                var transacao = _transacaoRepository.GetById(input.TransacaoId);
                transacao.Extornado = true;

                Ensure.NotNull(transacao, $"Não Existe nenhuma transação de Id {transacaoId}");

                retorno = new TransacoesResponse(transacao);
            }
            catch (Exception ex)
            {
                _logFactory.Log().Error($"Erro ao ExtornaTransacao. {ex.Message} - {ex.StackTrace}");
                throw;
            }

            return retorno;
        }
    }
}