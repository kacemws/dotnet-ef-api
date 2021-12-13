using System;
namespace API_2
{
    public interface IQuizRepository : ICRUDRepository<Quiz>
    {
        Quiz GetByName(string name);
    }
}
