using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.SQLite;
using HubFintech.ControleContas.Api.Domain;

namespace HubFintech.ControleContas.Api.Configuration
{
    public class BaseContext : DbContext
    {
        public BaseContext()
            : base(new SQLiteConnection()
            {
                ConnectionString =
                    new SQLiteConnectionStringBuilder()
                    {
                        DataSource = "../banco.db",
                        ForeignKeys = true,
                        DateTimeFormat = SQLiteDateFormats.UnixEpoch
                    }.ConnectionString
            }, true)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Conta>()
                .HasKey(x => x.Id)
                .HasRequired(s => s.Pessoa)
                .WithMany(g => g.Contas)
                .HasForeignKey(s => s.PessoaId);
            
            modelBuilder.Entity<Conta>()
                .HasOptional(x => x.ContaPai)
                .WithMany(x => x.ContasFilha);
            
            modelBuilder.Entity<Pessoa>()
                .HasKey(x => x.Id)
                .Map<PessoaFisica>(m => m.Requires("TipoPessoa").HasValue("PF"))
                .Map<PessoaJuridica>(m => m.Requires("TipoPessoa").HasValue("PJ"));

        }

        public DbSet<Conta> Conta { get; set; }
        public DbSet<Pessoa> Pessoa { get; set; }
    }
}