using System;
using HubFintech.ControleContas.Api.Configuration.Factories;
using HubFintech.ControleContas.Api.Domain.Repositories.Interfaces;
using HubFintech.ControleContas.Api.Domain.Services.Interfaces;
using HubFintech.ControleContas.Api.Domain.ViewModels;
using HubFintech.ControleContas.Api.Domain.ViewModels.Response;

namespace HubFintech.ControleContas.Api.Domain.Services
{
    public class PessoaService : IPessoaService
    {
        private readonly ILogFactory _logFactory;
        private readonly IBaseRepository<Pessoa> _pessoaRepository;

        public PessoaService(ILogFactory logFactory, IBaseRepository<Pessoa> pessoaRepository)
        {
            _logFactory = logFactory;
            _pessoaRepository = pessoaRepository;
        }

        public Model<PessoaResponse> ObtemTodas()
        {
            var model = new Model<PessoaResponse>();

            try
            {
                var pessoas = _pessoaRepository.All();
                
            }
            catch (Exception ex)
            {
                _logFactory.Log().Error($"Erro ao ObtemTodas. {ex.Message} - {ex.StackTrace}");
                model.Erro(ex);
            }

            return model;
        }
    }
}