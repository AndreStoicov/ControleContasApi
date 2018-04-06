using System.Data.Entity.ModelConfiguration;
using HubFintech.ControleContas.Api.Domain;

namespace HubFintech.ControleContas.Api.Configuration.EntityMap
{
    public class GestaoSaldoEntityMap : EntityTypeConfiguration<GestaoSaldo>
    {
        public GestaoSaldoEntityMap()
        {
            ToTable("GestaoSaldo");

            HasKey(x => x.Id);

            HasRequired(x => x.Conta)
                .WithMany(x => x.GestaoSaldos)
                .HasForeignKey(x => x.ContaId);

            HasRequired(x => x.Transacao)
                .WithMany(x => x.GestaoSaldo)
                .HasForeignKey(x => x.TransacaoId);
        }
    }
}