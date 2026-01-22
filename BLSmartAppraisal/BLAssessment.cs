using DLSmartAppraisal.Abstract;
using DLSmartAppraisal.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLSmartAppraisal
{
    public class BLAssessment
    {
        private readonly IAssessment _repo;
        private readonly IQuestion _qrepo;

        public BLAssessment(IAssessment repo, IQuestion ques)
        {
            _repo = repo;
            _qrepo = ques;
        }
        public List<AssessmentDetails> GetAllAssessments()
        {
            return _repo.GetAllAssessments();
        }

        public string AddAssessment(AssessmentDetails assessment)
        {
            if (assessment == null) return "invalid";

            assessment.UpdatedOn = null;
            assessment.UpdatedBy = null;

            var res = _repo.CreateAssessment(assessment);

            if (res == null) return "failed";

            return "success";
        }

        public string UpdateAssessment(AssessmentDetails assessmentDetails)
        {
            if (assessmentDetails == null) return "Invalid";

            assessmentDetails.UpdatedOn = DateTime.Now;

            var res =_repo.UpdateAssessment(assessmentDetails);

            if (res == null) return "failed";

            return "updated";
        }

        public AssessmentDetails GetAssessment(long ID)
        {
            var assessment = _repo.GetAssessment(ID);

            if (assessment == null) return null;

            return assessment;
        }

        public string MapAssessments(long assessmentId, List<string> userIds)
        {
            if (assessmentId <= 0 || userIds.IsNullOrEmpty()) return "invalid";

            var result = _repo.MapAssessments(assessmentId, userIds);

            if (result.IsNullOrEmpty()) return "failed";

            return "success";
        }

        public AssessmentDetails? GetUserActiveAssessments(string userId)
        {
            return _repo.GetActiveAssessment(userId);
        }

        public List<TestQuestionViewModel> GetQuestionsForTest(long assessmentId)
        {
            var assessment = _repo.GetAssessment(assessmentId);

            var compId = assessment.CompId;

            var questions = _qrepo.GetQuestionsByCompetency(compId, 10);

            return questions;
        }

        public void SaveAnswer(string userId, long questionId, int selectedOption, long assessmentId)
        {
            var response = new TestResponse
            {
                UserId = userId,
                QuestionId = questionId,
                SelectedOption = selectedOption,
                AssessmentId = assessmentId,
                SubmittedOn = DateTime.UtcNow
            };

            _repo.SaveTestResponse(response);
        }

        public Assessment? GetAssessmentCompletion(string userId, long assId)
        {
            return _repo.GetAssessmentCompletion(userId, assId);
        }

        public void SetCompleted(string userId, long assId)
        {
            _repo.SetCompleted(userId, assId);
        }

        public TestResult EvaluateResult(string userId, long assessmentId)
        {
            var responses = _repo.GetTestResponses(userId, assessmentId);
            if(responses == null || !responses.Any())
            {
                return null;
            }

            int totalQuestions = 10;
            int correctanswer = 0;

            foreach (var response in responses)
            {
                var question = _qrepo.GetQuestion(response.QuestionId);
                if (question != null && question.CorrectAnswer == response.SelectedOption)
                {
                    correctanswer++;
                }    
            }
            int hike = 0;

            double percentage = (double)correctanswer / totalQuestions * 100;

            if (percentage >= 90) hike = 20;
            else if (percentage < 90 && percentage >= 60) hike = 12;
            else if (percentage < 60 && percentage >= 40) hike = 7;
            else hike = 2;

            var result = new TestResult
            {
                UserId = userId,
                AssessmentId = assessmentId,
                TotalQuestions = totalQuestions,
                Score = correctanswer,
                Hike = hike
            };

            _repo.SaveResult(result);
            return result;
        }

        public TestResult? GetResult(string userId, long assessmentId)
        {
            return _repo.GetResult(assessmentId, userId);
        }

        public List<TestResult> TestResults()
        {
            return _repo.GetTestResults();
        }
    }
}
