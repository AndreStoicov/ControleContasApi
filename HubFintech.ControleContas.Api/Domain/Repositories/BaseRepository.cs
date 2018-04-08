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
        public DbContext Context { get; set; }
        public DbSet<T> DbSet { get; set; }

        public BaseRepository(DbContext dbContext)
        {
            Context = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            DbSet = Context.Set<T>();
        }

        public T Add(T entity)
        {
            DbSet.Add(entity);
            Context.SaveChanges();
            return entity;
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
            Context.Entry(entity).State = EntityState.Modified;
            Context.SaveChanges();
        }

        public T GetById(int id)
        {
            return DbSet.Find(id);
        }

        public IEnumerable<T> All()
        {
            return DbSet.ToList();
        }

        public IEnumerable<T> Find(Func<T, bool> predicate)
        {
            return DbSet.Where(predicate);
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