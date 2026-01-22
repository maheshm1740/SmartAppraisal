using Azure.Core;
using DLSmartAppraisal.Abstract;
using DLSmartAppraisal.Model;
using DLSmartAppraisal.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ULSmartAppraisal;

namespace BLSmartAppraisal
{
    public class BLQuestion
    {
        private readonly IQuestion _repo;
        private readonly IUserManagementRepositoryAdmin _urepo;
        private readonly EmailService _email;

        public BLQuestion(IQuestion repo, IUserManagementRepositoryAdmin urepo, EmailService email)
        {
            _repo = repo;
            _urepo = urepo;
            _email = email;
        }

        public string AddQuestionAnswer(QuestionViewModel model, string curId, string curRole)
        {
            if (model == null) return "Invalid Data";

            UserDetails curUser = _urepo.GetUserByUserId(curId);

            var question = new Question
            {
                QuestionText = model.QuestionText,
                ComptencyId = model.ComptencyId,
                CorrectAnswer = model.CorrectAnswer,
                CreatedBy = curUser.UserId,
                IsReviewed = false
            };

            long questionId = _repo.AddQuestion(question);

            if (questionId == 0) return "Failed add qustion";


            var answer = new Answer
            {
                QuestionId = questionId,
                Option1 = model.Option1,
                Option2 = model.Option2,
                Option3 = model.Option3,
                Option4 = model.Option4
            };

            string result = _repo.AddAnswer(answer);

            return "Question added successfully";
        }

        public async Task<string> UpdateQuestionAnswer(Question question, Answer answer)
        {
            Question qResult = _repo.UpdateQuestion(question);
            string aResult = _repo.UpdateAnswer(answer);

            if (qResult!=null && aResult.Contains("successfully"))
            {
                try
                {
                    UserDetails smeUser = _urepo.GetUserByUserId(qResult.CreatedBy);

                    if (smeUser == null)
                        return $"SME with UserId '{question.CreatedBy}' not found.";

                    if (string.IsNullOrEmpty(smeUser.Email))
                        return $"SME '{smeUser.Name}' does not have a valid email address.";

                    await _email.SendEmailAsync(
                        smeUser.Email,
                        "Question Updated",
                        $"Hi {smeUser.Name},\n\nYour question (ID: {question.Id}) has been reviewed and updated successfully by the PM.\n\nRegards,\nSmart Appraisal Team"
                    );

                    return "Question and Answer updated successfully, and SME notified.";
                }
                catch (Exception ex)
                {
                    return $"Update succeeded, but failed to send email: {ex.Message}";
                }
            }

            if (qResult == null)
                return "Question failed to update";

            if (!aResult.Contains("successfully"))
                return aResult;

            return "Update failed";
        }
        public (Question, Answer) GetQuestionAnswer(long questionId)
        {
            var question = _repo.GetQuestion(questionId);
            var answer = _repo.GetAnswerByQuestionId(questionId);

            return (question, answer);
        }

        public List<Question> AllQuestions()
        {
            return _repo.AllQuestions();
        }

        public List<Question> GetQuestionsBySME(string userId)
        {
            return _repo.GetQuestionsBySME(userId);
        }
    }
}
