using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace API_2
{
    public class QuizQuestionsRepository : CRUDRepository<QuizQuestions>, IQuizQuestionsRepository
    {

        public QuizQuestionsRepository(AppDbContext context) : base(context)
        {
            
        }

        public QuizQuestions GetQuizQuestionsByQuiz(Guid id)
        {
            
            try
            {
                var quizQuestions = dbSet.Where(qz => qz.quizId == id).First();
                return quizQuestions;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
