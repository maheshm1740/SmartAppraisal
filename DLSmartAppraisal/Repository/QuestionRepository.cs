using DLSmartAppraisal.Abstract;
using DLSmartAppraisal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLSmartAppraisal.Repository
{
    public class QuestionRepository : IQuestion
    {
        private readonly QuestionContext _context;

        public QuestionRepository(QuestionContext context)
        {
            _context = context;
        }

        public Question GetQuestion(long Id)
        {
            return _context.Questions.FirstOrDefault(q => q.Id == Id);
        }

        public Answer GetAnswerByQuestionId(long questionId)
        {
            return _context.Answers.FirstOrDefault(a => a.QuestionId == questionId);
        }


        public long AddQuestion(Question question)
        {
            if(question == null )
            {
                return 0;
            }

            _context.Questions.Add(question);
            _context.SaveChanges();

            return question.Id;
        }
        public string AddAnswer(Answer answer)
        {
            if (answer == null)
            {
                return "Answer is empty";
            }

            _context.Answers.Add(answer);
            _context.SaveChanges();

            return "Answer added successfully";
        }

        public Question UpdateQuestion(Question question)
        {
            var existing = _context.Questions.FirstOrDefault(q => q.Id == question.Id);
            if (existing == null) return null;

            existing.QuestionText = question.QuestionText;
            existing.CorrectAnswer = question.CorrectAnswer;
            existing.IsReviewed = question.IsReviewed;

            _context.SaveChanges();

            return existing;
        }

        public string UpdateAnswer(Answer answer)
        {
            var existing = _context.Answers.FirstOrDefault(a => a.QuestionId == answer.QuestionId);
            if (existing == null) return "Answer not found";

            existing.Option1 = answer.Option1;
            existing.Option2 = answer.Option2;
            existing.Option3 = answer.Option3;
            existing.Option4 = answer.Option4;

            _context.SaveChanges();
            return "Answer updated successfully";
        }

        public List<Question> AllQuestions()
        {
            return _context.Questions.ToList();
        }

        public List<TestQuestionViewModel> GetQuestionsByCompetency(int compId, int limit)
        {
            var questions = (from q in _context.Questions
                             join a in _context.Answers on q.Id equals a.QuestionId
                             where q.ComptencyId == compId && q.IsReviewed
                             select new TestQuestionViewModel
                             {
                                 QuestionId = q.Id,
                                 QuestionText = q.QuestionText,
                                 Option1 = a.Option1,
                                 Option2 = a.Option2,
                                 Option3 = a.Option3,
                                 Option4 = a.Option4
                             })
                             .ToList();
            var randomized = questions.OrderBy(q => Guid.NewGuid())
                                      .Take(limit)
                                      .ToList();

            return randomized;
        }
        public List<Question> GetQuestionsBySME(string userId)
        {
            return _context.Questions
                .Where(q => q.CreatedBy == userId)
                .OrderByDescending(q => q.Id)
                .ToList();
        }
    }
}
