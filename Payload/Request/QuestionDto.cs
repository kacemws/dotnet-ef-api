using System;
using System.Collections.Generic;

namespace API_2
{
    public class QuestionDto
    {
        public Guid quizQuestionsId
        {
            get;
            set;
        }

        public string content
        {
            get;
            set;
        }

        public QuestionType type
        {
            get;
            set;
        }

        
        public virtual ICollection<AnswerDto> answers
        {
            get;
            set;
        }

        public QuestionDto()
        {
        }
    }
}
