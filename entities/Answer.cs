using System;
using Microsoft.EntityFrameworkCore;

namespace API_2
{
    
    public class Answer : EntityWithId
    {
        // the answer suggestion itSelf
        public string content
        {
            get;
            set;
        }

        // if it's the valid answer or not
        public bool valid
        {
            get;
            set;
        }

        public Answer()
        {
        }
    }
}
