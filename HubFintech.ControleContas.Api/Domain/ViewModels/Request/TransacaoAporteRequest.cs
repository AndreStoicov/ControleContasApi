namespace HubFintech.ControleContas.Api.Domain.ViewModels.Request
{
    public class TransacaoAporteRequest
    {
        public int ContaDestinoId { get; set; }
        public decimal Valor { get; set; }
    }
}