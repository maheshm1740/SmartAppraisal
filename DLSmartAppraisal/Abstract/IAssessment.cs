using DLSmartAppraisal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLSmartAppraisal.Abstract
{
    public interface IAssessment
    {
        AssessmentDetails CreateAssessment(AssessmentDetails assessment);

        List<AssessmentDetails> GetAllAssessments();

        List<Assessment> MapAssessments(long assessmentId, List<string> userId);

        AssessmentDetails UpdateAssessment(AssessmentDetails assessmentDetails);

        AssessmentDetails GetAssessment(long ID);

        AssessmentDetails? GetActiveAssessment(string userId);

        void SaveTestResponse(TestResponse testResponse);

        Assessment GetAssessmentCompletion(string userId, long assId);

        void SetCompleted(string userId, long assId);

        void SaveResult(TestResult result);

        TestResult? GetResult(long assessmentId, string userId);

        bool Exists(long assessmentId, string userId);

        List<TestResponse> GetTestResponses(string userId, long assessmentId);

        List<TestResult> GetTestResults();
    }
}
