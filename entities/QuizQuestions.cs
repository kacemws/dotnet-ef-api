using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_2
{
    public class QuizQuestions : EntityWithId
    {
        public Guid quizId
        {
            get;
            set;
        }

        [ForeignKey("QuizRef")]
        public Quiz Quiz
        {
            get;
            set;
        }


        [Required]
        public virtual ICollection<Question> questions
        {
            get;
            set;
        }
        public QuizQuestions()
        {
            this.questions = new List<Question>();
        }
    }
}
