using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLSmartAppraisal.Model
{
    public class CandidateMappingViewModel
    {
        public long AssessmentId { get; set; }
        public List<CandidateSelection> Candidates { get; set; } = new();
    }
    public class CandidateSelection
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public bool IsSelected { get; set; } 
    }
}
