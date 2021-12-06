using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace API_2
{
    public class CRUDService<TEntity> : ICRUDService<TEntity> where TEntity : class
    {
        IUnitOfWork _unitOfWork;
        private readonly ICRUDRepository<TEntity> _repository;

        public CRUDService(IUnitOfWork unitOfWork, ICRUDRepository<TEntity> repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }
        public void Create(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _repository.Create(entity);
            _unitOfWork.Commit();
        }

        public void Delete(object entityID)
        {
            if (entityID == null || entityID == "")
            {
                throw new ArgumentNullException("entity");
            }
            _repository.Delete(entityID);
            _unitOfWork.Commit();
        }

        public void Delete(TEntity entityToDelete)
        {
            if (entityToDelete == null)
            {
                throw new ArgumentNullException("entity");
            }
            _repository.Delete(entityToDelete);
            _unitOfWork.Commit();
        }


        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, string includeProperties)
        {
            return _repository.GetAll(filter, orderBy, includeProperties);
        }

        public TEntity GetByID(object id)
        {
            return _repository.GetByID(id);
        }

        public void Update(TEntity entityToUpdate)
        {
            if (entityToUpdate == null)
            {
                throw new ArgumentNullException("entity");
            }
            _repository.Update(entityToUpdate);
            _unitOfWork.Commit();
        }
    }
}
