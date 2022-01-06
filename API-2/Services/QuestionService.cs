using System;
using System.Collections.Generic;
using System.Linq;

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

        public IEnumerable<Question> GetQuestionsByQuiz(Guid id)
        {
            return _questionRepository.GetQuestionsByQuiz(id);
        }

        public void DeleteUnused(ICollection<Question> current, Guid id)
        {
            var old = GetQuestionsByQuiz(id).ToList();

            foreach (Question question in old)
            {
                Console.WriteLine(old.Count);
                Console.WriteLine(current.Count);
                Question found = current.Where((qs) => qs.Id == question.Id).FirstOrDefault();

                if (found == null)
                {
                    Console.WriteLine("not found");
                    _questionRepository.DetachEntity(question);
                    _questionRepository.Delete(question);
                }
                else
                {
                    Console.WriteLine("found");
                    Console.WriteLine(found.Id);

                    _questionRepository.DetachEntity(found);

                }
            }
        }
    }
}
