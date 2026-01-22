using DLSmartAppraisal.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace DLSmartAppraisal
{
    public class UserContextFactory : IDesignTimeDbContextFactory<UserContext>
    {
        public UserContext CreateDbContext(string[] args)
        {
            var projectPath = Path.Combine(Directory.GetCurrentDirectory(), "..\\SmartAppraisal");

            var configuration = new ConfigurationBuilder()
                .SetBasePath(projectPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<UserContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            return new UserContext(optionsBuilder.Options);
        }
    }
    public class CompetencyContextFactory : IDesignTimeDbContextFactory<CompetencyContext>
    {
        public CompetencyContext CreateDbContext(string[] args)
        {
            var projectPath = Path.Combine(Directory.GetCurrentDirectory(), "..\\SmartAppraisal");

            var configuration = new ConfigurationBuilder()
                .SetBasePath(projectPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<CompetencyContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            return new CompetencyContext(optionsBuilder.Options);
        }
    }
    public class QuestionContextFactory : IDesignTimeDbContextFactory<QuestionContext>
    {
        public QuestionContext CreateDbContext(string[] args)
        {
            var projectPath = Path.Combine(Directory.GetCurrentDirectory(), "..\\SmartAppraisal");

            var configuration = new ConfigurationBuilder()
                .SetBasePath(projectPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<QuestionContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            return new QuestionContext(optionsBuilder.Options);
        }
    }
    public class AssessmentContextFactory : IDesignTimeDbContextFactory<AssessmentContext>
    {
        public AssessmentContext CreateDbContext(string[] args)
        {
            var projectPath = Path.Combine(Directory.GetCurrentDirectory(), "..\\SmartAppraisal");

            var configuration = new ConfigurationBuilder()
                .SetBasePath(projectPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<AssessmentContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            return new AssessmentContext(optionsBuilder.Options);
        }
    }
}
