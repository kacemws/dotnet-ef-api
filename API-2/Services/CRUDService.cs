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
            try
            {
                if (entityID == null || entityID == "")
                {
                    throw new ArgumentNullException("entity");
                }
                _repository.Delete(entityID);
                _unitOfWork.Commit();
            }
            catch (Exception exception) {
                Console.WriteLine(exception.Message);
                throw new Exception(exception.Message);
            }
            
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


        public IEnumerable<TEntity> GetAll()
        {
            return _repository.GetAll();
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
