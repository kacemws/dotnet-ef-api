using System;
using Microsoft.EntityFrameworkCore;

namespace API_2
{
    public class QuizQuestionsRepository : CRUDRepository<QuizQuestions>, IQuizQuestionsRepository
    {

        public QuizQuestionsRepository(AppDbContext context) : base(context)
        {
            
        }
        
    }
}
