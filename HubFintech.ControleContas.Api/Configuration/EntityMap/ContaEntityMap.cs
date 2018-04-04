using System.Data.Entity.ModelConfiguration;
using HubFintech.ControleContas.Api.Domain;

namespace HubFintech.ControleContas.Api.Configuration.EntityMap
{
    public class ContaEntityMap : EntityTypeConfiguration<Conta>
    {
        public ContaEntityMap()
        {
            ToTable("Conta");

            HasKey(x => x.Id);

            HasRequired(s => s.Pessoa)
                .WithMany(g => g.Contas)
                .HasForeignKey(s => s.PessoaId);

            HasOptional(x => x.ContaPai)
                .WithMany(x => x.ContasFilha);
        }
    }
}