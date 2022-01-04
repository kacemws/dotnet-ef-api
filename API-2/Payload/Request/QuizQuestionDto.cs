using System;
using System.Collections.Generic;

namespace API_2
{
    public class QuizQuestionDto
    {
        public Guid quizId
        {
            get;
            set;
        }
        
        public virtual ICollection<QuestionDto> questions
        {
            get;
            set;
        }
        public QuizQuestionDto()
        {
        }
    }
}
