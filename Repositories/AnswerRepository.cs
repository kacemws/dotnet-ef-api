using System;
namespace API_2
{
    public class AnswerRepository : CRUDRepository<Answer>, IAnswerRepository
    {

        public AnswerRepository(AppDbContext context) : base(context)
        {
            
        }
    }
}
