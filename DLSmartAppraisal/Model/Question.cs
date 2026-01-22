using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLSmartAppraisal.Model
{
    [Table("Questions")]
    public class Question
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        [MaxLength(1000)]
        public string QuestionText { get; set; }

        [Required]
        [Range(1,4)]
        public int CorrectAnswer { get; set; }

        [Required]
        [MaxLength(100)]
        public string CreatedBy { get; set; }

        [Required]
        public int ComptencyId { get; set; }

        public bool IsReviewed { get; set; }
    }
}
