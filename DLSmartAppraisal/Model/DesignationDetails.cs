using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLSmartAppraisal.Model
{
    public class DesignationDetails
    {
        [Key]
        public int DesgId { get; set; }

        public string DesgName { get; set; }
    }
}
