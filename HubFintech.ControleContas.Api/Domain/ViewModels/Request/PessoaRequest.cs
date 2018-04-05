using System.Runtime.Serialization;
using HubFintech.ControleContas.Api.Domain.ViewModels.Shared;

namespace HubFintech.ControleContas.Api.Domain.ViewModels.Request
{
    [DataContract]
    public class PessoaRequest
    {
        [DataMember]
        public PessoaFisicaViewModel PessoaFisica { get; set; }
        [DataMember]
        public PessoaJuridicaViewModel PessoaJuridica { get; set; }

        public static Pessoa Cria(PessoaRequest request)
        {
            if (request.PessoaFisica != null)
            {
                var pf = request.PessoaFisica;
                return Domain.PessoaFisica.Criar(pf.Cpf, pf.NomeCompleto, pf.DataNascimento);
            }

            var pj = request.PessoaJuridica;
            return Domain.PessoaJuridica.Criar(pj.Cnpj, pj.RazaoSocial, pj.NomeFantasia);
        }
    }
}