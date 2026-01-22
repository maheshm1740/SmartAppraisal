using DLSmartAppraisal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLSmartAppraisal.Abstract
{
    public interface IQuestion
    {
        long AddQuestion(Question question);
        string AddAnswer(Answer answer);

        Question UpdateQuestion(Question question);
        string UpdateAnswer(Answer answer);

        Question GetQuestion(long Id);

        Answer GetAnswerByQuestionId(long questionId);

        List<Question> AllQuestions();

        List<TestQuestionViewModel> GetQuestionsByCompetency(int compId, int limit);

        List<Question> GetQuestionsBySME(string userId);
    }
}
