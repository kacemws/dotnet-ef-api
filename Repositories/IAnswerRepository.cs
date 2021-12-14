using System;
using System.Collections.Generic;

namespace API_2
{
    public interface IAnswerRepository : ICRUDRepository<Answer>
    {
        public IEnumerable<Answer> GetAnswersByQuestion(Guid id);
    }
}
