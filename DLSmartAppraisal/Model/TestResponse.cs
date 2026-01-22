using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLSmartAppraisal.Model
{
    public class TestResponse
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public long QuestionId { get; set; }

        [Required]
        public int SelectedOption { get; set; }  

        [Required]
        public long AssessmentId { get; set; }

        public DateTime SubmittedOn { get; set; }
    }
}
