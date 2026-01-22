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
        private readonly DesignationContext _context;

        public DesignationRepository(DesignationContext context)
        {
            _context = context;
        }
        public List<DesignationDetails> GetDesignations()
        {
            return _context.designations.ToList();
        }
    }
}
