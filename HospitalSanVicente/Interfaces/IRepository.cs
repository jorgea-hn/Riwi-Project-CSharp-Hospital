using System;
using System.Collections.Generic;

namespace HospitalSanVicente.Interfaces
{
    public interface IRepository<T> where T : class
    {
        T GetById(Guid id);
        IEnumerable<T> GetAll();
        T Create(T entity);
        T Update(T entity);
        void Delete(Guid id);
    }
}
