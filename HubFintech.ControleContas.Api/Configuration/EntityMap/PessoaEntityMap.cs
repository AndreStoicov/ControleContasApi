using System.Data.Entity.ModelConfiguration;
using HubFintech.ControleContas.Api.Domain;

namespace HubFintech.ControleContas.Api.Configuration.EntityMap
{
    public class PessoaEntityMap : EntityTypeConfiguration<Pessoa>
    {
        public PessoaEntityMap()
        {
            ToTable("Pessoa");

            HasKey(x => x.Id)
                .Map<PessoaFisica>(m => m.Requires("TipoPessoa").HasValue("PF"))
                .Map<PessoaJuridica>(m => m.Requires("TipoPessoa").HasValue("PJ"));

            HasMany(x => x.Contas)
                .WithRequired(x => x.Pessoa)
                .HasForeignKey(x => x.PessoaId);
        }
    }
}