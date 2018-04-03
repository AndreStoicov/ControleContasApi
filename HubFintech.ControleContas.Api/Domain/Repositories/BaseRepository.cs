using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using HubFintech.ControleContas.Api.Domain.Repositories.Interfaces;

namespace HubFintech.ControleContas.Api.Domain.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T>, IDisposable where T : class
    {
        private DbContext Context { get; set; }
        private DbSet<T> DbSet { get; set; }

        public BaseRepository(DbContext dbContext)
        {
            Context = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            DbSet = Context.Set<T>();
        }

        public void Add(T entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }

        public T GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> All()
        {
            return DbSet.ToList();
        }

        public IEnumerable<T> Find(Func<T, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            if (Context.Database.Connection.State == ConnectionState.Open)
            {
                Context.Database.CurrentTransaction.Dispose();
                Context.Database.Connection.Close();
            }
        }
    }
}