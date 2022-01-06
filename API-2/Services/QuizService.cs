using BC = BCrypt.Net.BCrypt;
using System;
using System.Collections.Generic;
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

        public IDictionary<string, Object> GetAllPaginated(int page, int size)
        {
            return _quizRepository.GetAllPaginated(page, size);
        }

        public IDictionary<string, Object> GetFiltered(int type, int page, int size)
        {
            return _quizRepository.GetFiltered(type, page, size);
        }

        public void CreateQuiz(Quiz quiz)
        {
            quiz.password = BC.HashPassword(quiz?.password);
            this.Create(quiz);
        }

        public Boolean VerifyPassword(String clear, String hashed)
        {
            return BC.Verify(clear, hashed);
        }
    }
}
