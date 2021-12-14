using System;
using System.Collections.Generic;
using System.Linq;

namespace API_2
{
    public class AnswerRepository : CRUDRepository<Answer>, IAnswerRepository
    {

        public AnswerRepository(AppDbContext context) : base(context)
        {
            
        }
        public IEnumerable<Answer> GetAnswersByQuestion(Guid id)
        {
            try
            {
                var answers = dbSet.Where(ans => ans.questionId == id).ToList();
                return answers;
            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}
