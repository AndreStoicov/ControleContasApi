using System.Collections.Generic;
using HubFintech.ControleContas.Api.Domain.ViewModels.Request;
using HubFintech.ControleContas.Api.Domain.ViewModels.Response;

namespace HubFintech.ControleContas.Api.Domain.Services.Interfaces
{
    public interface ITransacaoService
    {
        List<TransacoesResponse> ObtemTodasTransacoes(int contaId);
        TransacoesResponse ObtemTransacao(int transacaoId);
        TransacoesResponse ExtornaTransacao(TransacaoExtornoRequest input);
        TransacoesResponse CriaAporte(TransacaoAporteRequest input);
        TransacoesResponse CriaTransferencia(TransacaoTransferenciaRequest input);
    }
}