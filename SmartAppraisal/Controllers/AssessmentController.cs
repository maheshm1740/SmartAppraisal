using BLSmartAppraisal;
using DLSmartAppraisal.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SmartAppraisal.Helpers;

namespace SmartAppraisal.Controllers
{
    [Authorize]
    public class AssessmentController : Controller
    {
        private readonly BLAssessment _blAssessment;
        private readonly DesigManagement _blDesignation;
        private readonly BlCompetency _blCompetency;
        private readonly BLUserManagement _bLUser;

        public AssessmentController(BLAssessment bLAssessment,BLUserManagement bLUserManagement, DesigManagement desigManagement, BlCompetency blCompetency)
        {
            _blAssessment = bLAssessment;
            _blDesignation = desigManagement;
            _blCompetency = blCompetency;
            _bLUser = bLUserManagement;
        }
        public IActionResult Assessments()
        {
            var assessments = _blAssessment.GetAllAssessments();

            return View(assessments);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateAssessment()
        {
            LoadDropdown(new AssessmentDetails());
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateAssessment(AssessmentDetails assessmentDetails)
        {
            if (!ModelState.IsValid)
            {
                LoadDropdown(assessmentDetails);
                return View(assessmentDetails);
            }
            var curUser = HttpContext.Session.GetString("curUserId");
            assessmentDetails.CreatedBy = curUser;
            assessmentDetails.CreatedOn = DateTime.Now;

            var result = _blAssessment.AddAssessment(assessmentDetails);

            if(result == "success")
            {
                return RedirectToAction("Assessments");
            }

            ViewBag.Error = "Failed to create assessment";
            LoadDropdown(assessmentDetails);
            return View(assessmentDetails);
        }

        private void LoadDropdown(AssessmentDetails assessmentDetails)
        {
            ViewBag.DesigList = _blDesignation.GetDesignations()
                .Select(d => new SelectListItem
                {
                    Text = d.DesgName,
                    Value = d.DesgId.ToString(),
                    Selected = d.DesgId == assessmentDetails.Designation
                }).ToList();

            ViewBag.CompetencyList = _blCompetency.GetCompetenciesByRoleId(3)
                .Select(c => new SelectListItem
                {
                    Text = c.CompDescription,
                    Value = c.CompId.ToString(),
                    Selected = c.CompId == assessmentDetails.CompId
                }).ToList();
        }

        [HttpGet]
        [Authorize(Roles = "HR")]
        public IActionResult MapCandidates()
        {
            var assessments = _blAssessment.GetAllAssessments();
            ViewBag.Assessments = assessments.Select(a => new SelectListItem
            {
                Value = a.AssesmentId.ToString(),
                Text = a.Name
            }).ToList();

            int roleId = 3;

            var candidates = _bLUser.GetAllcandidates(roleId);

            var model = new CandidateMappingViewModel
            {
                Candidates = candidates.Select(c => new CandidateSelection
                {
                    UserId = c.UserId,
                    Name = c.Name,
                    IsSelected = true
                }).ToList()
            };

            return View(model);
        }
        [HttpPost]
        [Authorize(Roles = "HR")]
        public IActionResult MapCandidates(CandidateMappingViewModel model)
        {
            if (model.AssessmentId <= 0)
            {
                TempData["Error"] = "Select assessment.";
                return RedirectToAction("MapCandidates");
            }

            var selectedUserIds = model.Candidates
                .Where(c => c.IsSelected)
                .Select(c => c.UserId)
                .ToList();

            var result = _blAssessment.MapAssessments(model.AssessmentId, selectedUserIds);

            TempData[result == "success" ? "Success" : "Error"] =
                result == "success" ? "Candidates mapped successfully!" : "Failed to map candidates.";

            return RedirectToAction("MapCandidates");
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateAssessment(long Id)
        {
            var assessment = _blAssessment.GetAssessment(Id);

            if(assessment == null)
            {
                TempData["ErrorMessage"] = "Assessment not found";

                return RedirectToAction("Assessments");
            }
            return View(assessment);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateAssessment(AssessmentDetails assessmentDetails)
        {
            if (!ModelState.IsValid)
                return View(assessmentDetails);

            assessmentDetails.UpdatedBy = HttpContext.Session.GetString("curUserId");

            var result = _blAssessment.UpdateAssessment(assessmentDetails);

            if (result == "Invalid")
            {
                TempData["ErrorMessage"] = "Invalid request.";
                return View(assessmentDetails);
            }

            if (result == "failed")
            {
                TempData["ErrorMessage"] = "Failed to update assessment.";
                return View(assessmentDetails);
            }

            TempData["SuccessMessage"] = "Assessment updated successfully!";
            return RedirectToAction("Assessments");
        }

        public IActionResult Instruction()
        {
            var userId = HttpContext.Session.GetString("curUserId");
            if (string.IsNullOrEmpty(userId))
                return RedirectToAction("Index", "Login");

            var assessment = _blAssessment.GetUserActiveAssessments(userId);
            if (assessment == null)
                return RedirectToAction("Index", "Home");

            return View("Instruction", assessment);
        }

        [HttpPost]
        public IActionResult StartTest(long assessmentId)
        {
            var userId = HttpContext.Session.GetString("curUserId");
            if (string.IsNullOrEmpty(userId))
                return RedirectToAction("Index", "Login");

            var questions = _blAssessment.GetQuestionsForTest(assessmentId);

            if (questions == null || questions.Count == 0)
                return View("NoQuestions");

            HttpContext.Session.Set("TestQuestions", questions);
            HttpContext.Session.SetInt32("CurrentIndex", 0);
            HttpContext.Session.SetString("AssessmentId", assessmentId.ToString());

            return RedirectToAction("Test");
        }

        [HttpGet]
        public IActionResult Test()
        {
            var questions = HttpContext.Session.Get<List<TestQuestionViewModel>>("TestQuestions");
            var currentIndex = HttpContext.Session.GetInt32("CurrentIndex") ?? 0;

            if (questions == null || !questions.Any())
                return RedirectToAction("Instruction");

            if (currentIndex >= questions.Count)
                return RedirectToAction("TestCompleted");

            ViewBag.CurrentIndex = currentIndex;
            ViewBag.TotalQuestions = questions.Count;

            return View("Test", questions[currentIndex]);
        }

        [HttpPost]
        public IActionResult SubmitAnswer(long questionId, int selectedOption)
        {
            var userId = HttpContext.Session.GetString("curUserId");
            var assessmentIdStr = HttpContext.Session.GetString("AssessmentId");
            long assessmentId = string.IsNullOrEmpty(assessmentIdStr) ? 0 : Convert.ToInt64(assessmentIdStr);
            var questions = HttpContext.Session.Get<List<TestQuestionViewModel>>("TestQuestions");
            var currentIndex = HttpContext.Session.GetInt32("CurrentIndex") ?? 0;

            if (string.IsNullOrEmpty(userId) || questions == null)
                return RedirectToAction("Index", "Login");

            _blAssessment.SaveAnswer(userId, questionId, selectedOption, assessmentId);

            currentIndex++;
            HttpContext.Session.SetInt32("CurrentIndex", currentIndex);

            if (currentIndex >= questions.Count)
                return RedirectToAction("TestCompleted");

            return RedirectToAction("Test");
        }

        public IActionResult TestCompleted()
        {
            HttpContext.Session.Remove("TestQuestions");
            HttpContext.Session.Remove("AssessmentId");
            HttpContext.Session.Remove("CurrentIndex");

            return View("TestCompleted");
        }

        [HttpPost]
        public IActionResult SubmitTest(long? questionId, int? selectedOption)
        {
            var userId = HttpContext.Session.GetString("curUserId");
            var assessmentIdStr = HttpContext.Session.GetString("AssessmentId");
            long assessmentId = string.IsNullOrEmpty(assessmentIdStr) ? 0 : Convert.ToInt64(assessmentIdStr);
            var questions = HttpContext.Session.Get<List<TestQuestionViewModel>>("TestQuestions");

            if (string.IsNullOrEmpty(userId) || questions == null)
                return RedirectToAction("Index", "Login");

            if (questionId.HasValue && selectedOption.HasValue)
            {
                _blAssessment.SaveAnswer(userId, questionId.Value, selectedOption.Value, assessmentId);
            }

            _blAssessment.SetCompleted(userId, assessmentId);

            _blAssessment.EvaluateResult(userId, assessmentId);

            HttpContext.Session.Remove("TestQuestions");
            HttpContext.Session.Remove("AssessmentId");
            HttpContext.Session.Remove("CurrentIndex");

            TempData["Message"] = "Test submitted successfully!";
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Authorize(Roles = "PM")]
        public IActionResult TestResults()
        {
            var results = _blAssessment.TestResults();

            return View(results);
        }
    }
}
