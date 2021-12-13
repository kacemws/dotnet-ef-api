using System;
using System.Collections.Generic;

namespace API_2
{
    public interface IQuizRepository : ICRUDRepository<Quiz>
    {
        Quiz GetByName(string name);
        IEnumerable<Quiz> GetFiltered(int type);
    }
}
