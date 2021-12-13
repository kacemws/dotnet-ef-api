using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API_2
{
    [ApiController]
    [Route("/api/v1/quizzes")]
    public class QuizController : ControllerBase
    {
        
        private readonly ILogger<QuizController> _logger;
        IQuizService _quizService;
        IQuizQuestionsService _quizQuestionsService;
        IQuestionService _questionService;
        IAnswerService _answerService;


        public QuizController(
            ILogger<QuizController> logger,
            IQuizService quizService,
            IQuizQuestionsService quizQuestionsService,
            IQuestionService questionService,
            IAnswerService answerService
        ){
            _logger = logger;
            _quizService = quizService;
            _quizQuestionsService = quizQuestionsService;
            _questionService = questionService;
            _answerService = answerService;
        }

        [HttpGet]
        public IActionResult GetAllQuizzes(int? type)
        {
            try
            {
                int? filter = type;
                Console.WriteLine(type);
                IEnumerable<Quiz> quizzes = new List<Quiz>();

                if (filter == null)
                {
                    quizzes = _quizService.GetAll();

                }else 
                {
                    if (filter > 2 || filter < 0) filter = 1;
                    quizzes = _quizService.GetFiltered((int)filter);
                }
                
                if (quizzes == null) return NotFound();
                return Ok(quizzes);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetQuizById(Guid id)
        {
            try
            {
                var quiz = _quizService.GetByID(id);
                if (quiz == null) return NotFound();
                QuizQuestions questions = quiz.quizQuestions;
                return Ok(questions);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult PostQuiz(Quiz quiz)
        {
            try
            {
                var existing = _quizService.GetByName(quiz.name);
                if (existing != null) throw new Exception("quiz with the same name already exists");


                _quizService.Create(quiz);
                return Created("",quiz);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteQuiz(Guid id, string password)
        {
            try
            {
                var quiz = _quizService.GetByID(id);

                if (quiz == null) return NotFound();

                if (quiz.password != password) throw new Exception("passwords aren't matching");

                // instead of deleting the entity, we would rather archive it, so that we can recover it at any given time

                quiz.state = QuizState.ARCHIVED;

                _quizService.Update(quiz);

                return Ok();
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }



        [HttpPut("{id}")]
        public IActionResult UpdateQuiz(Guid id, Quiz updatedQuiz)
        {
            try
            {
                var quiz = _quizService.GetByID(id);

                if (quiz == null) return NotFound();

                if (quiz.password != updatedQuiz.password) throw new Exception("passwords aren't matching");

                if (quiz.state != QuizState.DRAFT) throw new Exception("can't edit the quiz, it's already published");

                _quizService.Update(updatedQuiz);
                return Ok();
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }


        [HttpPut("{orderId}/get-questions/{id}")]
        public IActionResult UpdateQuizQuestions(Guid id, Quiz updatedQuiz)
        {
            try
            {
                //var quiz = _quizService.GetByID(id);
                //Console.WriteLine(quiz);
                //if (quiz == null) return NotFound();
                //_quizService.Update(updatedQuiz);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }



    }
}
