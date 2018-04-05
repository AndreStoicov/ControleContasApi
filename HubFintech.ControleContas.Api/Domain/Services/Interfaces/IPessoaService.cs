using System.Collections.Generic;
using HubFintech.ControleContas.Api.Domain.ViewModels;
using HubFintech.ControleContas.Api.Domain.ViewModels.Request;
using HubFintech.ControleContas.Api.Domain.ViewModels.Response;

namespace HubFintech.ControleContas.Api.Domain.Services.Interfaces
{
    public interface IPessoaService
    {
        List<PessoaResponse> ObtemTodas();
        PessoaResponse ObtemPessoa(int pessoaId);
        Pessoa ObtemPessoaPorCpf(string cpf);
        Pessoa ObtemPessoaPorCnpj(string cnpj);
        PessoaResponse CriaPessoa(PessoaRequest pessoaRequest);
    }
}