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
        public IActionResult GetAllQuizzes()
        {
            try
            {
                var quizzes = _quizService.GetAll();
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
                return Ok(quiz);
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
                Console.WriteLine(quiz?.quizQuestions);
                return Created("",quiz);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteQuiz(Guid id)
        {
            try
            {
                var quiz = _quizService.GetByID(id);
                if (quiz == null) return NotFound();
                _quizService.Delete(id);
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
                Console.WriteLine(quiz);
                if (quiz == null) return NotFound();
                _quizService.Update(updatedQuiz);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }



    }
}
