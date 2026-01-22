using BLSmartAppraisal;
using DLSmartAppraisal.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace SmartAppraisal.Controllers
{
    [Authorize]
    public class QuestionAnswerController : Controller
    {
        private readonly BLQuestion _bLQuestion;
        private readonly BlCompetency _blCompetency;

        public QuestionAnswerController(BLQuestion bLQuestion, BlCompetency blCompetency)
        {
            _bLQuestion = bLQuestion;
            _blCompetency = blCompetency;
        }

        [HttpGet]
        [Authorize(Roles = "PM")]
        public IActionResult QuestionList()
        {
            var questions = _bLQuestion.AllQuestions()
                            .OrderBy(q => q.IsReviewed)      
                            .ThenByDescending(q => q.Id)    
                            .ToList();


            var competencies = _blCompetency.GetAllCompetencies()
                        .ToDictionary(c => c.CompId, c => c.CompDescription);

            ViewBag.Competencies = competencies;

            return View(questions);
        }

        [HttpGet]
        [Authorize(Roles = "PM")]
        public IActionResult ReviewQuestion(long id)
        {
            var (question, answer) = _bLQuestion.GetQuestionAnswer(id);
            if (question == null || answer == null)
                return NotFound();

            var model = new QuestionReviewModel
            {
                QuestionId = question.Id,
                QuestionText = question.QuestionText,
                CorrectAnswer = question.CorrectAnswer,
                Option1 = answer.Option1,
                Option2 = answer.Option2,
                Option3 = answer.Option3,
                Option4 = answer.Option4,
            };

            return View(model);
        }
        [HttpPost]
        [Authorize(Roles = "PM")]
        public async Task<IActionResult> ReviewQuestion(QuestionReviewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var question = new Question
            {
                Id = model.QuestionId,
                QuestionText = model.QuestionText,
                CorrectAnswer = model.CorrectAnswer,
                IsReviewed = true,
            };

            var answer = new Answer
            {
                QuestionId = model.QuestionId,
                Option1 = model.Option1,
                Option2 = model.Option2,
                Option3 = model.Option3,
                Option4 = model.Option4
            };

            string result = await _bLQuestion.UpdateQuestionAnswer(question, answer);

            TempData["Message"] = result;

            return RedirectToAction("QuestionList");
        }


        [HttpGet]
        [Authorize(Roles = "SME")]
        public IActionResult Add()
        {
            int roleId = HttpContext.Session.GetInt32("curRoleId") ?? 0;

            var competencies = _blCompetency.GetCompetenciesByRoleId(3);

            ViewBag.CompeList = competencies.Select(c => new SelectListItem
            {
                Text = c.CompDescription,
                Value = c.CompId.ToString()
            }).ToList();

            return View();
        }

        [HttpPost]
        [Authorize(Roles = "SME")]
        public IActionResult Add(QuestionViewModel model)
        {
            var curUserId = HttpContext.Session.GetString("curUserId");
            var curRole = HttpContext.Session.GetString("curRole");
            int roleId = HttpContext.Session.GetInt32("curRoleId") ?? 0;

            var competencies = _blCompetency.GetCompetenciesByRoleId(3);

            ViewBag.CompeList = competencies.Select(c => new SelectListItem
            {
                Text = c.CompDescription,
                Value = c.CompId.ToString()
            }).ToList();

            if (!ModelState.IsValid)
                return View(model);

            var result = _bLQuestion.AddQuestionAnswer(model, curUserId, curRole);

            ViewBag.SuccessMessage = result ?? "Question added successfully!";
            ModelState.Clear();
            return View(new QuestionViewModel());
        }

        [Authorize(Roles = "SME")]
        [HttpGet]
        public IActionResult MyQuestions()
        {
            var userId = HttpContext.Session.GetString("curUserId");
            if (string.IsNullOrEmpty(userId))
                return RedirectToAction("Index", "Login");

            var questions = _bLQuestion.GetQuestionsBySME(userId);

            if (questions == null || !questions.Any())
                ViewBag.Message = "No questions found.";

            var competencies = _blCompetency.GetAllCompetencies()
                            .ToDictionary(c => c.CompId, c => c.CompDescription);
            ViewBag.Competencies = competencies;

            questions = questions
                .OrderByDescending(q => q.IsReviewed) 
                .ThenByDescending(q => q.Id)           
                .ToList();

            return View(questions);
        }


    }
}
