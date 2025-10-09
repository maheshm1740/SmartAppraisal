using DLSmartAppraisal.Model;
using DLSmartAppraisal.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLSmartAppraisal
{
    public class DesigManagement
    {
        DesignationRepository _repo = new DesignationRepository();

        public List<DesignationDetails> GetDesignations()
        {
            return _repo.GetDesignations();
        }
    }
}
