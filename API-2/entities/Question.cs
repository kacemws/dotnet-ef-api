using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API_2
{
    public class Question : EntityWithId
    {
        public Guid quizQuestionsId
        {
            get;
            set;
        }

        public virtual QuizQuestions QuizQuestions
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

        [Required]
        public virtual ICollection<Answer> answers
        {
            get;
            set;
        }

        public Question()
        {
            this.answers = new List<Answer>();
        }
    }
}
