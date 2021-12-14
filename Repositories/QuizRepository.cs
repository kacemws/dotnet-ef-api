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

        public IEnumerable<Quiz> GetFiltered(int type)
        {
            QuizState[] states = { QuizState.DRAFT, QuizState.PUBLISHED, QuizState.ARCHIVED };
            try
            {
                var quizzes = dbSet.Where(qz => qz.state == states[type]);
                return quizzes;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
