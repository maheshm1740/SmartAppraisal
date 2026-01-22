using DLSmartAppraisal.Abstract;
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
        private readonly IDesignation _repo;

        public DesigManagement(IDesignation repo)
        {
            _repo = repo;
        }

        public List<DesignationDetails> GetDesignations()
        {
            return _repo.GetDesignations();
        }
    }
}
