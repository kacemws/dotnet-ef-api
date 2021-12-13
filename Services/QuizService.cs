using System;
using System.Threading.Tasks;

namespace API_2
{
    public class QuizService : CRUDService<Quiz> , IQuizService
    {
        IUnitOfWork _unitOfWork;
        private readonly IQuizRepository _quizRepository;

        public QuizService(IUnitOfWork unitOfWork, IQuizRepository quizRepository): base(unitOfWork, quizRepository)
        {
            _unitOfWork = unitOfWork;
            _quizRepository = quizRepository;
        }

        public Quiz GetByName(string name)
        {
            return _quizRepository.GetByName(name);

        }
    }
}
