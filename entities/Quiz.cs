using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace API_2
{
    [Index(nameof(name), IsUnique = true)]
    public class Quiz : EntityWithId
    {
        //name of the quiz
        [Required]
        public string name
        {
            get;
            set;
        }

        // password to protect the edition of the quiz
        [Required]
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

        //overall rating, ex : 289
        public int? rating
        {
            get;
            set;
        }

        // number of people who voted for this quiz
        public int? numberOfVotes
        {
            get;
            set;
        }

        // number of people who played the quiz
        public int? numberOfPlays
        {
            get;
            set;
        }

        public Difficulty? difficulty
        {
            get;
            set;
        }


        public QuizQuestions quizQuestions
        {
            get;
            set;
        }


        public Quiz()
        {
           
        }

        public double getRating()
        {
            if (this.numberOfPlays == 0) return 0;
            return (double)(this.rating / this.numberOfVotes);
        }
    }
}
