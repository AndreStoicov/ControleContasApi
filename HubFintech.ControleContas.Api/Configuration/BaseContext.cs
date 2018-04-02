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
                        ForeignKeys = true
                    }.ConnectionString
            }, true)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Conta> Conta { get; set; }
    }
}