using System;
using System.Collections.Generic;

namespace API_2
{
    public interface IQuestionRepository : ICRUDRepository<Question>
    {
        public IEnumerable<Question> GetQuestionsByQuiz(Guid id);
        public Question DetachEntity(Question question);
    }
}
