using DLSmartAppraisal.Abstract;
using DLSmartAppraisal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLSmartAppraisal.Repository
{
    public class DesignationRepository:IDesignation
    {
        DesignationContext context = new DesignationContext();
        public List<DesignationDetails> GetDesignations()
        {
            return context.designations.ToList();
        }
    }
}
