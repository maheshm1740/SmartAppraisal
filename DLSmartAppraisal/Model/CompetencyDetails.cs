using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DLSmartAppraisal.Model
{
    [Table("Competency")]
    public class CompetencyDetails
    {
        [Key]
        public int CompId { get; set; }

        [Required(ErrorMessage = "Competency description is required")]
        [StringLength(100)]
        public string CompDescription { get; set; }

        [Required(ErrorMessage = "Role is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a role.")]
        public int RoleId { get; set; }

        public DateTime CreatedDate { get; set; }

        [StringLength(50)]
        public string? CreatedBy { get; set; }
    }
}
