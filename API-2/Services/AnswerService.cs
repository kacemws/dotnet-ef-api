using System;
using System.Collections.Generic;
using System.Linq;

namespace API_2
{
    public class AnswerService : CRUDService<Answer> , IAnswerService
    {
        IUnitOfWork _unitOfWork;
        private readonly IAnswerRepository _answerRepository;

        public AnswerService(IUnitOfWork unitOfWork, IAnswerRepository answerRepository) : base(unitOfWork, answerRepository)
        {
            _unitOfWork = unitOfWork;
            _answerRepository = answerRepository;
        }

        public IEnumerable<Answer> GetAnswersByQuestion(Guid id)
        {
            return _answerRepository.GetAnswersByQuestion(id);
        }

        public void DeleteUnused(ICollection<Answer> current, Guid id)
        {
            var old = GetAnswersByQuestion(id).ToList();

            foreach(Answer answer in old)
            {
                Console.WriteLine(old.Count);
                Console.WriteLine(current.Count);
                Answer found = current.Where((ans) => ans.Id == answer.Id).FirstOrDefault();
                
                if(found == null)
                {
                    Console.WriteLine("not found");
                    _answerRepository.DetachEntity(answer);
                    _answerRepository.Delete(answer);
                }
                else
                {
                    Console.WriteLine("found");
                    Console.WriteLine(found.Id);

                    _answerRepository.DetachEntity(found);

                }
            }
        }
    }
}
