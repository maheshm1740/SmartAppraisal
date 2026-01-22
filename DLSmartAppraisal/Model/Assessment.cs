using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLSmartAppraisal.Model
{
    public class Assessment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required (ErrorMessage = "Select assesment")]
        public long AssessmentId { get; set; }

        [Required(ErrorMessage = "Select user")]
        public string UserId { get; set; }

        public bool IsCompleted { get; set; } = false;
    }
}
