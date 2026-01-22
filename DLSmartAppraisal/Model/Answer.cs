using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLSmartAppraisal.Model
{
    public class Answer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public long QuestionId { get; set; }

        [Required, MaxLength(500)]
        public string Option1 { get; set; }

        [Required, MaxLength(500)]
        public string Option2 { get; set; }

        [Required, MaxLength(500)]
        public string Option3 { get; set; }

        [Required, MaxLength(500)]
        public string Option4 { get; set; }
    }
}
