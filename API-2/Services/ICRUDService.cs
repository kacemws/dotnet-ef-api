using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace API_2
{
    public interface ICRUDService<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        TEntity GetByID(object id);
        void Create(TEntity entity);
        void Delete(object entityID);
        void Delete(TEntity entityToDelete);
        void Update(TEntity entityToUpdate);
    }
}
