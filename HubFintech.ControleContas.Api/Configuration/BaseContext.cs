using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.SQLite;
using HubFintech.ControleContas.Api.Configuration.EntityMap;
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

            modelBuilder.Configurations.Add(new ContaEntityMap());
            modelBuilder.Configurations.Add(new PessoaEntityMap());
            modelBuilder.Configurations.Add(new TransacaoEntityMap());
            modelBuilder.Configurations.Add(new GestaoSaldoEntityMap());
        }

        public DbSet<Conta> Conta { get; set; }
        public DbSet<Pessoa> Pessoa { get; set; }
        public DbSet<Transacao> Transacao { get; set; }
        public DbSet<GestaoSaldo> GestaoSaldo { get; set; }
    }
}