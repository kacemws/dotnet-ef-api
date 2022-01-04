using System;
namespace API_2
{
    public class QuizDto
    {
        
        public string name
        {
            get;
            set;
        }

        
        public string password
        {
            get;
            set;
        }

        // state of the quiz
        public QuizState? state
        {
            get;
            set;
        }

        public Difficulty? difficulty
        {
            get;
            set;
        }


        public QuizQuestionDto quizQuestions
        {
            get;
            set;
        }
        public QuizDto()
        {
        }
    }
}
