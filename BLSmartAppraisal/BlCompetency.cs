using DLSmartAppraisal.Abstract;
using DLSmartAppraisal.Model;
using System.Collections;
using System.Collections.Generic;

namespace BLSmartAppraisal
{
    public class BlCompetency
    {
        private readonly ICompetency _repo;

        public BlCompetency(ICompetency repo)
        {
            _repo = repo;
        }

        public List<CompetencyDetails> GetAllCompetencies()
        {
            var competencies = _repo.GetAllCompetencies();
            if (competencies == null)
                return new List<CompetencyDetails>();

            return competencies;

        }

        public string AddCompetency(CompetencyDetails competency)
        {
            if (competency != null)
            {
                return _repo.AddCompetency(competency);
            }
            return "Competency is null or invalid";
        }

        public List<CompetencyDetails> GetCompetenciesByRoleId(int roleId)
        {
            return _repo.GetCompetencyByRoleId(roleId);
        }
    }
}
