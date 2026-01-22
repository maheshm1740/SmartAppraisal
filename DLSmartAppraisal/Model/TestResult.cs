using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLSmartAppraisal.Model
{
    public class TestResult
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public long AssessmentId { get; set; }

        [Required]
        public int Score { get; set; }

        [Required]
        public int TotalQuestions { get; set; }

        [Required]
        public int Hike { get; set; }

        public DateTime TakenOn { get; set; } = DateTime.UtcNow;
    }
}
