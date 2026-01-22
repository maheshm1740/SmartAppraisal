using DLSmartAppraisal.Abstract;
using DLSmartAppraisal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLSmartAppraisal.Repository
{
    public class AssessmentRepository:IAssessment
    {
        private readonly AssessmentContext _context;

        public AssessmentRepository(AssessmentContext context)
        {
            _context = context;
        }

        public List<AssessmentDetails> GetAllAssessments()
        {
            return _context.AssessmentDetails.OrderByDescending(x => x.CreatedOn).ToList();
        }

        public AssessmentDetails CreateAssessment(AssessmentDetails assessment)
        {
            _context.AssessmentDetails.Add(assessment);
            _context.SaveChanges();

            return assessment;
        }

        public AssessmentDetails UpdateAssessment(AssessmentDetails assessmentDetails)
        {
            var existing = _context.AssessmentDetails.FirstOrDefault(a => a.AssesmentId == assessmentDetails.AssesmentId);

            if (existing == null) return null;

            existing.DateOfAssessment = assessmentDetails.DateOfAssessment;
            existing.IsActive = assessmentDetails.IsActive;
            existing.UpdatedOn = assessmentDetails.UpdatedOn;
            existing.UpdatedBy = assessmentDetails.UpdatedBy;

            _context.SaveChanges();
            
            return existing;
        }

        public AssessmentDetails GetAssessment(long Id)
        {
            return _context.AssessmentDetails.FirstOrDefault(a => a.AssesmentId == Id);
        }

        public List<Assessment> MapAssessments(long assessmentId, List<string> userIds)
        {
            var mapped = new List<Assessment>();

            foreach (var userId in userIds)
            {
                var exists = _context.Assessments
                    .FirstOrDefault(x => x.AssessmentId == assessmentId && x.UserId == userId);

                if (exists == null)
                {
                    var mapping = new Assessment
                    {
                        AssessmentId = assessmentId,
                        UserId = userId
                    };
                    _context.Assessments.Add(mapping);
                    mapped.Add(mapping);
                }
            }

            _context.SaveChanges();
            return mapped;
        }

        public AssessmentDetails? GetActiveAssessment(string userId)
        {
            var activeAssessment = (from ad in _context.AssessmentDetails
                                    join a in _context.Assessments on ad.AssesmentId equals a.AssessmentId
                                    where a.UserId == userId && !a.IsCompleted && ad.IsActive
                                    select ad)
                        .FirstOrDefault();

            return activeAssessment;
        }

        public void SaveTestResponse(TestResponse testResponse)
        {
            _context.testResponses.Add(testResponse);
            _context.SaveChanges();
        }

        public Assessment? GetAssessmentCompletion(string userId, long assId)
        {
            return _context.Assessments.FirstOrDefault(a => a.UserId == userId && a.AssessmentId == assId);
        }

        public void SetCompleted(string userId, long assId)
        {
            var assessment = _context.Assessments.FirstOrDefault(a => a.UserId == userId && a.AssessmentId == assId);
            assessment.IsCompleted = true;
            _context.SaveChanges();
        }

        public void SaveResult(TestResult result)
        {
            _context.TestResults.Add(result);
            _context.SaveChanges();
        }

        public TestResult? GetResult(long assessmentId, string userId)
        {
            return _context.TestResults
                .FirstOrDefault(r => r.AssessmentId == assessmentId && r.UserId == userId);
        }

        public bool Exists(long assessmentId, string userId)
        {
            return _context.TestResults
                .Any(r => r.AssessmentId == assessmentId && r.UserId == userId);
        }

        public List<TestResponse> GetTestResponses(string userId, long assessmentId)
        {
            var responses = _context.testResponses.Where(r => r.UserId == userId && r.AssessmentId == assessmentId)
                .ToList();

            return responses;
        }

        public List<TestResult> GetTestResults()
        {
            return _context.TestResults.ToList();
        }
    }
}
