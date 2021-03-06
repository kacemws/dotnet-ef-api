using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace API_2
{
    public class QuizRepository : CRUDRepository<Quiz>, IQuizRepository
    {

        public QuizRepository(AppDbContext context) : base(context)
        {
            
        }

        public Quiz GetByName(string name)
        {
            try
            {
                // first or default
                var entity = dbSet.Where(qz => qz.name == name).First();
                context.Entry(entity).State = EntityState.Detached;
                return entity;
            }
            catch (Exception)
            {
                return null;
            }
            
        }

        public IDictionary<string, Object> GetAllPaginated(int page, int size)
        {
            try
            {
                
                var quizzes = dbSet.OrderBy(qz=>qz.Id).Skip((page - 1) * size).Take(size).ToList();
                var count = dbSet.Count();
                IDictionary<string, Object> pageable = new Dictionary<string, Object>();

                pageable.Add("items", quizzes);
                pageable.Add("count", count);

                return pageable;
            }
            catch (Exception)
            {
                return null;
            }
        }


        public IDictionary<string, Object> GetFiltered(int type, int page, int size)
        {
            QuizState[] states = { QuizState.DRAFT, QuizState.PUBLISHED, QuizState.ARCHIVED };
            try
            {
                var quizzes = dbSet.Where(qz => qz.state == states[type]).OrderBy(qz => qz.Id).Skip((page-1) * size).Take(size).ToList();
                var count = dbSet.Where(qz => qz.state == states[type]).Count();

                IDictionary<string, Object> pageable = new Dictionary<string, Object>();

                pageable.Add("items", quizzes);
                pageable.Add("count", count);

                return pageable;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
