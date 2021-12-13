using System;
using System.Threading.Tasks;

namespace API_2
{
    public class QuestionService : CRUDService<Question> , IQuestionService
    {
        IUnitOfWork _unitOfWork;
        private readonly IQuestionRepository _questionRepository;

        public QuestionService(IUnitOfWork unitOfWork, IQuestionRepository questionRepository) : base(unitOfWork, questionRepository)
        {
            _unitOfWork = unitOfWork;
            _questionRepository = questionRepository;
        }
    }
}
