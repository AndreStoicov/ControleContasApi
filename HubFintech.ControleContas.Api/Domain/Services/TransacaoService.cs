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
        private readonly IBaseRepository<Conta> _contaRepository;

        public TransacaoService(ILogFactory logFactory, IBaseRepository<Transacao> transacaoRepository,
            IBaseRepository<Conta> contaRepository)
        {
            _logFactory = logFactory;
            _transacaoRepository = transacaoRepository;
            _contaRepository = contaRepository;
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

                Ensure.NotNull(transacao, $"Não Existe nenhuma transação de Id {input.TransacaoId}");

                transacao.Extornado = true;
                transacao.DataExtorno = DateTime.Now;

                _transacaoRepository.Update(transacao);

                retorno = new TransacoesResponse(transacao);
            }
            catch (Exception ex)
            {
                _logFactory.Log().Error($"Erro ao ExtornaTransacao. {ex.Message} - {ex.StackTrace}");
                throw;
            }

            return retorno;
        }

        public TransacoesResponse CriaAporte(TransacaoAporteRequest input)
        {
            TransacoesResponse retorno;
            try
            {
                var conta = _contaRepository.GetById(input.ContaDestinoId);
                var transacaoAporte = Transacao.CriaAporte(conta, input.Valor);
                transacaoAporte = _transacaoRepository.Add(transacaoAporte);

                retorno = new TransacoesResponse(transacaoAporte);
            }
            catch (Exception ex)
            {
                _logFactory.Log().Error($"Erro ao CriaAporte. {ex.Message} - {ex.StackTrace}");
                throw;
            }

            return retorno;
        }

        public TransacoesResponse CriaTransferencia(TransacaoTransferenciaRequest input)
        {
            TransacoesResponse retorno;
            try
            {
                var contaOrigem = _contaRepository.GetById(input.ContaOrigemId);
                var contaDestino = _contaRepository.GetById(input.ContaDestinoId);

                var transacaoTransferencia = Transacao.CriaTransferencia(contaOrigem, contaDestino, input.Valor);
                transacaoTransferencia = _transacaoRepository.Add(transacaoTransferencia);
                retorno = new TransacoesResponse(transacaoTransferencia);
            }
            catch (Exception ex)
            {
                _logFactory.Log().Error($"Erro ao CriaTransferencia. {ex.Message} - {ex.StackTrace}");
                throw;
            }

            return retorno;
        }
    }
}