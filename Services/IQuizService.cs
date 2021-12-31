using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace API_2
{
    public interface IQuizService : ICRUDService<Quiz>
    {
        
        IDictionary<string, Object> GetAllPaginated(int page, int size);

        Quiz GetByName(string name);
        IDictionary<string, Object> GetFiltered(int type, int page, int size);
    }
}
