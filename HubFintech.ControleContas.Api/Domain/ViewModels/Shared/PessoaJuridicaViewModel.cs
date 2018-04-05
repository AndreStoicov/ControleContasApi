namespace HubFintech.ControleContas.Api.Domain.ViewModels.Shared
{
    public class PessoaJuridicaViewModel
    {
        public PessoaJuridicaViewModel()
        {
        }

        public PessoaJuridicaViewModel(PessoaJuridica pessoa)
        {
            Cnpj = pessoa.Cnpj;
            RazaoSocial = pessoa.RazaoSocial;
            NomeFantasia = pessoa.NomeFantasia;
        }

        public string Cnpj { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
    }
}