using System;
namespace API_2
{
    public class Quiz : EntityWithId
    {
        //name of the quiz
        public string name
        {
            get;
            set;
        }

        //overall rating, ex : 289
        public int rating
        {
            get;
            set;
        }

        // number of people who played the quiz
        public int numberOfPlays
        {
            get;
            set;
        }

        public Difficulty difficulty
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
            return this.rating / this.numberOfPlays;
        }
    }
}
