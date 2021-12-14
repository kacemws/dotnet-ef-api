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

                // if no filter passed, get all quizzes
                if (filter == null)
                {
                    quizzes = _quizService.GetAll();

                }else 
                {
                    // if out of range, return only public
                    if (filter > 2 || filter < 0) filter = 1;
                    quizzes = _quizService.GetFiltered((int)filter);
                }

                // none were found
                if (quizzes == null) return NotFound();
                return Ok(quizzes);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetQuizById(Guid id, string? password)
        {
            try
            {
                var quiz = _quizService.GetByID(id);

                // if no quiz was found
                if (quiz == null) return NotFound();

                // if found, but still in draft, only owner can access (hense, the password comparaison)
                if(quiz.state == QuizState.DRAFT && quiz.password != password)
                {
                    return Unauthorized();
                }

                // if draft and visited by admin, or public, get all questions and answer, player 1 ready to play
                QuizQuestions quizQuestions = _quizQuestionsService.GetQuizQuestionsByQuiz(id);
                quiz.quizQuestions = quizQuestions;
                return Ok(quiz);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPost]
        public IActionResult PostQuiz(Quiz quiz)
        {
            try
            {
                var existing = _quizService.GetByName(quiz.name);

                // if a quiz by the same name exist, raise an exception
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

                // before deleting, we should check that's its an admin doing it
                if (quiz.password != password) return Unauthorized();

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

                // check that an admin is doing the update
                if (quiz.password != updatedQuiz.password) return Unauthorized();

                // no update while published 
                if (quiz.state != QuizState.DRAFT) throw new Exception("can't edit the quiz, it's already published");

                _quizService.Update(updatedQuiz);
                return Ok();
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }


        //[HttpPut("{orderId}/get-questions/{id}")]
        //public IActionResult UpdateQuizQuestions(Guid id, Quiz updatedQuiz)
        //{
        //    try
        //    {
        //        //var quiz = _quizService.GetByID(id);
        //        //Console.WriteLine(quiz);
        //        //if (quiz == null) return NotFound();
        //        //_quizService.Update(updatedQuiz);
        //        return Ok();
        //    }
        //    catch (Exception)
        //    {
        //        return BadRequest();
        //    }
        //}

        //------------------------
        [HttpGet("quiz-states")]
        public IActionResult GetQuizStates()
        {
            try
            {
                Dictionary<QuizState, int> states = new Dictionary<QuizState, int>();

                states.Add(QuizState.DRAFT, 0);
                states.Add(QuizState.PUBLISHED, 1);
                states.Add(QuizState.ARCHIVED, 2);

                return Ok(states);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("question-types")]
        public IActionResult GetQuestionType()
        {
            try
            {
                Dictionary<QuestionType, int> types = new Dictionary<QuestionType, int>();

                types.Add(QuestionType.INPUT, 0);
                types.Add(QuestionType.SINGLE, 1);
                types.Add(QuestionType.MULTI, 2);

                return Ok(types);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("difficulties")]
        public IActionResult GetDifficulties()
        {
            try
            {
                Dictionary<Difficulty, int > difficulties = new Dictionary<Difficulty, int>();

                difficulties.Add(Difficulty.EASY, 0);
                difficulties.Add(Difficulty.MEDIUM,1);
                difficulties.Add(Difficulty.HARD,2);

                return Ok(difficulties);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
