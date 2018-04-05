using System;
using System.Collections.Generic;
using System.Linq;

namespace HubFintech.ControleContas.Api.Domain.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        T Add(T entity);
        void Delete(T entity);
        void Delete(int id);
        void Update(T entity);
        T GetById(int Id);
        IEnumerable<T> All();
        IEnumerable<T> Find(Func<T, bool> predicate);
    }
}