using System.Data.Entity.ModelConfiguration;
using HubFintech.ControleContas.Api.Domain;

namespace HubFintech.ControleContas.Api.Configuration.EntityMap
{
    public class TransacaoEntityMap : EntityTypeConfiguration<Transacao>
    {
        public TransacaoEntityMap()
        {
            ToTable("Transacao");

            HasKey(x => x.Id);

            HasRequired(x => x.ContaDestino)
                .WithMany(x => x.Transacoes)
                .HasForeignKey(x => x.ContaDestinoId);

            HasOptional(x => x.ContaOrigem)
                .WithMany(x => x.Transacoes)
                .HasForeignKey(x => x.ContaOrigemId);
        }
    }
}