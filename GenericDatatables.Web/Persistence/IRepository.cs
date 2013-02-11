using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GenericDatatables.Web.Persistence
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> All();
        T Find(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Delete(int id);
        void SaveChanges();
    }
}