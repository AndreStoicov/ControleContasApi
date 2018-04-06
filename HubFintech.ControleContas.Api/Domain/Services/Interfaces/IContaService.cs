using System.Collections.Generic;
using HubFintech.ControleContas.Api.Domain.ViewModels.Request;
using HubFintech.ControleContas.Api.Domain.ViewModels.Response;

namespace HubFintech.ControleContas.Api.Domain.Services.Interfaces
{
    public interface IContaService
    {
        List<ContaResponse> ObtemContas(int pessoaId);
        ContaResponse ObtemConta(int pessoaId, int contaId);
        List<ContaResponse> ObtemContasFilha(int pessoaId, int contaId);
        ContaResponse CriaConta(ContaRequest contaRequest);
        ContaResponse CriaContaFilha(ContaFilhaRequest contaRequest);
    }
}