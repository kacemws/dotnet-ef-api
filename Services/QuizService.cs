using System;
using System.Threading.Tasks;

namespace API_2
{
    public class QuizService : IQuizService
    {
        private readonly IQuizRepository _quizRepository;
        public QuizService(IQuizRepository quizRepository)
        {
            _quizRepository = quizRepository;
        }
        public async Task<Quiz> GetByID(object id)
        {

        }
    }
}
