using DLSmartAppraisal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLSmartAppraisal.Abstract
{
    public interface ICompetency
    {
        List<CompetencyDetails> GetAllCompetencies();
        //CompetencyDetails GetCompetency(int id);

        string AddCompetency(CompetencyDetails competency);

        List<CompetencyDetails> GetCompetencyByRoleId(int roleId);

        //void UpdateCompetency(CompetencyDetails competency);

        //void DeleteCOmpetency(int Id);
    }
}
