using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace API_2
{
    public interface IQuestionService : ICRUDService<Question>
    {
        public IEnumerable<Question> GetQuestionsByQuiz(Guid id);
        public void DeleteUnused(ICollection<Question> current, Guid id);
    }
}
