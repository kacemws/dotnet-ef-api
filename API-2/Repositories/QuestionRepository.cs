using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace API_2
{
    public class QuestionRepository : CRUDRepository<Question>, IQuestionRepository
    {

        public QuestionRepository(AppDbContext context) : base(context)
        {
            
        }
        // IQUERYABLE
        public IEnumerable<Question> GetQuestionsByQuiz(Guid id)
        {
            

            try
            {
                var quizQuestions = dbSet.AsNoTracking().Where(qz => qz.quizQuestionsId == id).ToList();
                return quizQuestions;
            }
            catch (Exception)
            {
                    return null;
            }
            
        }
        public Question DetachEntity(Question question)
        {
            context.Entry(question).State = EntityState.Detached;
            return question;
        }
    }
}
