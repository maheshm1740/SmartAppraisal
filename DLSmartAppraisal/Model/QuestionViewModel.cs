using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLSmartAppraisal.Model
{
    public class QuestionViewModel
    {
        [Required]
        [MaxLength(1000)]
        public string QuestionText { get; set; }

        [Required, MaxLength(500)]
        public string Option1 { get; set; }

        [Required, MaxLength(500)]
        public string Option2 { get; set; }

        [Required, MaxLength(500)]
        public string Option3 { get; set; }

        [Required, MaxLength(500)]
        public string Option4 { get; set; }

        [Required]
        [Range(1, 4, ErrorMessage = "Select correct option (1 to 4)")]
        public int CorrectAnswer { get; set; }

        [Required]
        public int ComptencyId { get; set; }

    }
}
