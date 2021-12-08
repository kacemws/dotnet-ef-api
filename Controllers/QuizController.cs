using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API_2
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class QuizController : ControllerBase
    {
        
        private readonly ILogger<QuizController> _logger;
        IQuizService _quizService;
        

        public QuizController(ILogger<QuizController> logger, IQuizService quizService )
        {
            _logger = logger;
            _quizService = quizService;
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
                _quizService.Create(quiz);
                return Created("",quiz);
            }
            catch (Exception)
            {
                return BadRequest();
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
            catch (Exception)
            {
                return BadRequest();
            }
        }



        [HttpPut("{id}")]
        public IActionResult UpdateQuiz(Guid id, Quiz updatedQuiz)
        {
            try
            {
                var quiz = _quizService.GetByID(id);
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
