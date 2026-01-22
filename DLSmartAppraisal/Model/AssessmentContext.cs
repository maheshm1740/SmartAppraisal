using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLSmartAppraisal.Model
{
    public class AssessmentContext:DbContext
    {
        public AssessmentContext(DbContextOptions<AssessmentContext> options) : base(options) { }
        public DbSet<AssessmentDetails> AssessmentDetails { get; set; }

        public DbSet<Assessment> Assessments { get; set; }

        public DbSet<TestResponse> testResponses { get; set; }

        public DbSet<TestResult> TestResults { get; set; }
    }
}
