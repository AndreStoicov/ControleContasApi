using System;
using System.Collections.Generic;
using System.Linq;
using Fabrik.Common;
using HubFintech.ControleContas.Api.Configuration.Factories;
using HubFintech.ControleContas.Api.Domain.Repositories.Interfaces;
using HubFintech.ControleContas.Api.Domain.Services.Interfaces;
using HubFintech.ControleContas.Api.Domain.ViewModels;
using HubFintech.ControleContas.Api.Domain.ViewModels.Request;
using HubFintech.ControleContas.Api.Domain.ViewModels.Response;

namespace HubFintech.ControleContas.Api.Domain.Services
{
    public class PessoaService : IPessoaService
    {
        private readonly ILogFactory _logFactory;
        private readonly IBaseRepository<Pessoa> _pessoaRepository;
        private readonly IBaseRepository<PessoaFisica> _pessoaFisicaRepository;
        private readonly IBaseRepository<PessoaJuridica> _pessoaJuridicaRepository;

        public PessoaService(ILogFactory logFactory, IBaseRepository<Pessoa> pessoaRepository,
            IBaseRepository<PessoaFisica> pessoaFisicaRepository,
            IBaseRepository<PessoaJuridica> pessoaJuridicaRepository)
        {
            _logFactory = logFactory;
            _pessoaRepository = pessoaRepository;
            _pessoaFisicaRepository = pessoaFisicaRepository;
            _pessoaJuridicaRepository = pessoaJuridicaRepository;
        }

        public List<PessoaResponse> ObtemTodas()
        {
            List<PessoaResponse> retorno;

            try
            {
                var pessoas = _pessoaRepository.All().ToList();

                var pessoasFisica = pessoas.Where(p => p is PessoaFisica)
                    .Select(x => new PessoaResponse((PessoaFisica) x)).ToList();

                var pessoasJuridica = pessoas.Where(p => p is PessoaJuridica)
                    .Select(x => new PessoaResponse((PessoaJuridica) x)).ToList();

                retorno = pessoasFisica.Union(pessoasJuridica).ToList();
            }
            catch (Exception ex)
            {
                _logFactory.Log().Error($"Erro ao ObtemTodas. {ex.Message} - {ex.StackTrace}");
                throw;
            }

            return retorno;
        }

        public PessoaResponse ObtemPessoa(int pessoaId)
        {
            PessoaResponse retorno;
            try
            {
                var pessoa = _pessoaRepository.GetById(pessoaId);

                Ensure.NotNull(pessoa, $"Pessoa com Id {pessoaId} Não encontrada");

                if (pessoa is PessoaFisica fisica)
                    retorno = new PessoaResponse(fisica);
                else
                    retorno = new PessoaResponse((PessoaJuridica) pessoa);
            }
            catch (Exception ex)
            {
                _logFactory.Log().Error($"Erro ao ObtemPessoa. {ex.Message} - {ex.StackTrace}");
                throw;
            }

            return retorno;
        }

        public Pessoa ObtemPessoaPorCpf(string cpf)
        {
            return _pessoaFisicaRepository.Find(x => x.Cpf == cpf).FirstOrDefault();
        }

        public Pessoa ObtemPessoaPorCnpj(string cnpj)
        {
            return _pessoaJuridicaRepository.Find(x => x.Cnpj == cnpj).FirstOrDefault();
        }

        public PessoaResponse CriaPessoa(PessoaRequest pessoaRequest)
        {
            PessoaResponse retorno;
            try
            {
                var pessoa = PessoaRequest.Cria(pessoaRequest);
                pessoa = _pessoaRepository.Add(pessoa);

                if (pessoa is PessoaFisica fisica)
                    retorno = new PessoaResponse(fisica);
                else
                    retorno = new PessoaResponse((PessoaJuridica) pessoa);
            }
            catch (Exception ex)
            {
                _logFactory.Log().Error($"Erro ao CriaPessoa. {ex.Message} - {ex.StackTrace}");
                throw;
            }

            return retorno;
        }
    }
}