using System;
using System.Collections.Generic;

namespace API_2
{
    public interface IQuizRepository : ICRUDRepository<Quiz>
    {
        Quiz GetByName(string name);
        IDictionary<string, Object> GetAllPaginated(int page, int size);
        IDictionary<string, Object> GetFiltered(int type, int page, int size);
    }
}
