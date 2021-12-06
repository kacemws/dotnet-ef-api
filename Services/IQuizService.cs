using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace API_2
{
    public interface IQuizService
    {
        IEnumerable<Quiz> GetAll(Expression<Func<Quiz, bool>> filter, Func<IQueryable<Quiz>, IOrderedQueryable<Quiz>> orderBy, string includeProperties);
        Quiz GetByID(object id);
        void Create(Quiz entity);
        void Delete(object quizID);
        void Delete(Quiz entityToDelete);
        void Update(Quiz entityToUpdate);
        void Save();
    }
}
