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

    }
}
