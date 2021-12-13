using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace API_2
{
    public interface IQuizService : ICRUDService<Quiz>
    {
        Quiz GetByName(string name);
        IEnumerable<Quiz> GetFiltered(int type);
    }
}
