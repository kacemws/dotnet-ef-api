using System;
namespace API_2
{
    public interface IQuizQuestionsRepository : ICRUDRepository<QuizQuestions>
    {
        QuizQuestions GetQuizQuestionsByQuiz(Guid id);
    }
}
