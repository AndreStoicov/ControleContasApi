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
    public class ContaService : IContaService
    {
        private readonly ILogFactory _logFactory;
        private readonly IBaseRepository<Conta> _contaRepository;
        private readonly IBaseRepository<Pessoa> _pessoaRepository;

        public ContaService(ILogFactory logFactory, IBaseRepository<Conta> contaRepository,
            IBaseRepository<Pessoa> pessoaRepository)
        {
            _logFactory = logFactory;
            _contaRepository = contaRepository;
            _pessoaRepository = pessoaRepository;
        }

        public List<ContaResponse> ObtemContas(int pessoaId)
        {
            List<ContaResponse> retorno;
            try
            {
                Ensure.Argument.Is(pessoaId > 0, "Pessoa Id precisa ser maior que zero");
                var contas = _contaRepository.Find(x => x.PessoaId == pessoaId);
                retorno = contas.Select(x => new ContaResponse(x)).ToList();
            }
            catch (Exception ex)
            {
                _logFactory.Log().Error($"Erro ao ObtemContas. {ex.Message} - {ex.StackTrace}");
                throw;
            }

            return retorno;
        }

        public List<ContaResponse> ObtemContasFilha(int pessoaId, int contaId)
        {
            List<ContaResponse> retorno;
            try
            {
                Ensure.Argument.Is(pessoaId > 0, "Pessoa Id precisa ser maior que zero");
                Ensure.Argument.Is(contaId > 0, "Conta Id precisa ser maior que zero");

                var contas = _contaRepository.Find(x => x.PessoaId == pessoaId && x.Id == contaId).ToList();
                var contasFilha = contas.SelectMany(x => x.ContasFilha).ToList();

                Ensure.Not(!contasFilha.Any(), $"Conta Id {contaId} Não possui nenhuma Conta Filha");

                retorno = contasFilha.Select(x => new ContaResponse(x)).ToList();
            }
            catch (Exception ex)
            {
                _logFactory.Log().Error($"Erro ao ObtemContasFilha. {ex.Message} - {ex.StackTrace}");
                throw;
            }

            return retorno;
        }

        public ContaResponse ObtemConta(int pessoaId, int contaId)
        {
            ContaResponse retorno;
            try
            {
                Ensure.Argument.Is(pessoaId > 0, "Pessoa Id precisa ser maior que zero");
                Ensure.Argument.Is(contaId > 0, "Conta Id precisa ser maior que zero");

                var conta = _contaRepository.Find(x => x.PessoaId == pessoaId && x.Id == contaId).FirstOrDefault();

                Ensure.NotNull(conta, "Não existe nenhuma Conta com os parâmetros informados");

                retorno = new ContaResponse(conta);
            }
            catch (Exception ex)
            {
                _logFactory.Log().Error($"Erro ao ObtemConta. {ex.Message} - {ex.StackTrace}");
                throw;
            }

            return retorno;
        }

        public ContaResponse CriaConta(ContaRequest contaRequest)
        {
            ContaResponse retorno;
            try
            {
                var pessoa = _pessoaRepository.GetById(contaRequest.PessoaId);

                Ensure.NotNull(pessoa, nameof(pessoa));

                var conta = ContaRequest.Cria(contaRequest, pessoa);
                conta = _contaRepository.Add(conta);

                retorno = new ContaResponse(conta);
            }
            catch (Exception ex)
            {
                _logFactory.Log().Error($"Erro ao CriaConta. {ex.Message} - {ex.StackTrace}");
                throw;
            }

            return retorno;
        }

        public ContaResponse CriaContaFilha(ContaFilhaRequest contaRequest)
        {
            ContaResponse retorno;
            try
            {
                var pessoa = _pessoaRepository.GetById(contaRequest.PessoaId);
                var contaPai = _contaRepository.GetById(contaRequest.ContaPaiId);

                Ensure.NotNull(pessoa, nameof(pessoa));
                Ensure.NotNull(contaPai, nameof(contaPai));

                var conta = ContaFilhaRequest.Cria(contaRequest, pessoa, contaPai);
                conta = _contaRepository.Add(conta);

                retorno = new ContaResponse(conta);
            }
            catch (Exception ex)
            {
                _logFactory.Log().Error($"Erro ao CriaConta. {ex.Message} - {ex.StackTrace}");
                throw;
            }

            return retorno;
        }
    }
}