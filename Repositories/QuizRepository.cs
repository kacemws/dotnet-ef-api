using System;
namespace API_2
{
    public class QuizRepository : CRUDRepository<Quiz>, IQuizRepository
    {

        public QuizRepository(AppDbContext context) : base(context)
        {
            
        }
    }
}
