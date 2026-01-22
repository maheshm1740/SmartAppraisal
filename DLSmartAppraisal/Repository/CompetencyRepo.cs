using DLSmartAppraisal.Abstract;
using DLSmartAppraisal.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DLSmartAppraisal.Repository
{
    public class CompetencyRepo : ICompetency
    {
        private readonly CompetencyContext _context;

        public CompetencyRepo(CompetencyContext context)
        {
            _context = context;
        }

        public List<CompetencyDetails> GetAllCompetencies()
        {
            return _context.Competencies?.ToList() ?? new List<CompetencyDetails>();
        }

        public string AddCompetency(CompetencyDetails competency)
        {
            try
            {
                if (competency == null) 
                    return "Invalid Competency Details.";

                bool exists = _context.Competencies
                    .Any(c => c.CompDescription == competency.CompDescription && c.RoleId == competency.RoleId);

                if (exists)
                    return "Competency already exists.";

                competency.CreatedDate = DateTime.Now;
                _context.Competencies.Add(competency);
                _context.SaveChanges();

                return "Competency Added Successfully.";
            }
            catch (Exception ex)
            {
                return $"Error while adding competency: {ex.Message}";
            }
        }

        public List<CompetencyDetails> GetCompetencyByRoleId(int roleId)
        {
            if (roleId <= 0) return null;

            return _context.Competencies
                .Where(c => c.RoleId == roleId)
                .ToList();
        }
    }
}
