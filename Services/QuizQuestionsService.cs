using System;
using System.Threading.Tasks;

namespace API_2
{
    public class QuizQuestionsService : CRUDService<QuizQuestions> , IQuizQuestionsService
    {
        IUnitOfWork _unitOfWork;
        private readonly IQuizQuestionsRepository _quizQuestionsRepository;

        public QuizQuestionsService(IUnitOfWork unitOfWork, IQuizQuestionsRepository quizQuestionsRepository) : base(unitOfWork, quizQuestionsRepository)
        {
            _unitOfWork = unitOfWork;
            _quizQuestionsRepository = quizQuestionsRepository;
        }

    }
}
