using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API_2
{
    public class Question : EntityWithId
    {
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
