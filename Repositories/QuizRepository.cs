using System;
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
                var entity = dbSet.Where(qz => qz.name == name).First();
                context.Entry(entity).State = EntityState.Detached;
                return entity;
            }
            catch (Exception)
            {
                return null;
            }
            
        }
    }
}
