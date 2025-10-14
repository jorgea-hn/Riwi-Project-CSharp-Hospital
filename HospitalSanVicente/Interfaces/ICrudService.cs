using System.Collections.Generic;

namespace HospitalSanVicente.Interfaces
{
    public interface ICrudService<TEntity, TIdentifier> where TEntity : class
    {
        TEntity Create(TEntity entity);
        TEntity Get(TIdentifier id);
        IEnumerable<TEntity> GetAll();
        TEntity Update(TEntity entity);
        void Delete(TIdentifier id);
    }
}
