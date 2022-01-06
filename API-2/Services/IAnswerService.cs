using System;
using System.Collections.Generic;

namespace API_2
{
    public interface IAnswerService : ICRUDService<Answer>
    {
        public IEnumerable<Answer> GetAnswersByQuestion(Guid id);
        public void DeleteUnused(ICollection<Answer> current, Guid id);
    }
}
