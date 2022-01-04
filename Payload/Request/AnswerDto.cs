using System;
namespace API_2
{
    public class AnswerDto
    {
        public Guid questionId
        {
            get;
            set;
        }

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


        public AnswerDto()
        {
        
        }
    }
}
