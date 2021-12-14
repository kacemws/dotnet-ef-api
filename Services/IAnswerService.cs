using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace API_2
{
    public interface IAnswerService : ICRUDService<Answer>
    {
        public IEnumerable<Answer> GetAnswersByQuestion(Guid id);
    }
}
