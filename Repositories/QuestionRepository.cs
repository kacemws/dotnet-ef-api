using System;
namespace API_2
{
    public class QuestionRepository : CRUDRepository<Question>, IQuestionRepository
    {

        public QuestionRepository(AppDbContext context) : base(context)
        {
            
        }
    }
}
