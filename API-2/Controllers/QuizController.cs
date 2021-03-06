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
        public IActionResult GetAllQuizzes(int? type, int? page = 1, int? size = 10)
        {
            try
            {
                int? filter = type;
                
                IDictionary<string, Object> pageable = new Dictionary<string, Object>();

                //pagination check
                if (page < 1) page = 1;
                if (size <= 0) size = 10;

                Console.WriteLine(type);
                // if no filter passed, get all quizzes
                if (filter == null)
                {
                    pageable = _quizService.GetAllPaginated((int)page, (int)size);
                }
                else 
                {
                    // if out of range, return only public
                    if (filter > 2 || filter < 0) filter = 1;

                    pageable = _quizService.GetFiltered((int)filter, (int)page, (int)size);
                }
 
                // none were found
                if ((pageable["items"] as IEnumerable<Quiz>).Count() == 0) return NoContent();
                return Ok(pageable);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
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
                if(quiz.state != QuizState.PUBLISHED  && !_quizService.VerifyPassword(password, quiz.password))
                {
                    return Unauthorized();
                }

                // if draft and visited by admin, or public, get all questions and answer, player 1 ready to play
                QuizQuestions quizQuestions = _quizQuestionsService.GetQuizQuestionsByQuiz(id);
               
                quiz.quizQuestions = quizQuestions;

                if (quizQuestions != null) {
                    ICollection<Question> questions = (ICollection<Question>) _questionService.GetQuestionsByQuiz(quizQuestions.Id);
                    
                    if (questions != null && questions.Count > 0)
                    {
                        foreach(Question question in questions)
                        {
                            question.answers = (ICollection<Answer>)_answerService.GetAnswersByQuestion(question.Id);
                        }
                    }
                    quiz.quizQuestions.questions = questions;
                }


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


                _quizService.CreateQuiz(quiz);
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
                if (!_quizService.VerifyPassword(password, quiz.password)) return Unauthorized();

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
                Quiz quiz = _quizService.GetByID(id);

                if (quiz == null) return NotFound();

                // check that an admin is doing the update
                
                if (!_quizService.VerifyPassword(updatedQuiz.password, quiz.password)) return Unauthorized();

                // hashed
                updatedQuiz.password = quiz.password;

                // no update while published 
                if (quiz.state != QuizState.DRAFT) throw new Exception("can't edit the quiz, it's already published");

                // update question if not new
                foreach (Question question in updatedQuiz.quizQuestions.questions)
                {
                    
                    if (question?.Id != new Guid()) // equal to 00000000-0000-0000-0000-000000000000
                    {
                        //update answers content if not new
                        foreach (Answer answer in question.answers)
                        {
                            if (answer?.Id != new Guid()) // equal to 00000000-0000-0000-0000-000000000000
                            {
                                _answerService.Update(answer);
                            }
                        }
                        /*Testing delete*/
                        _answerService.DeleteUnused(question.answers, question.Id);
                        /*Testing delete*/

                        _questionService.Update(question);
                    }
                }

                /*Testing delete*/
                _questionService.DeleteUnused(updatedQuiz.quizQuestions.questions, updatedQuiz.quizQuestions.Id);
                /*Testing delete*/

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
                types.Add(QuestionType.CHECKBOX, 1);

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
