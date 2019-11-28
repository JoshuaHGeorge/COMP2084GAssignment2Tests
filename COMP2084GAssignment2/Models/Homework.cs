using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace COMP2084GAssignment2.Models
{
    public partial class Homework
    {
        [Key]
        public int HomeworkId { get; set; }
        public int? CourseId { get; set; }
        public int? AssignmentId { get; set; }
        [Column(TypeName = "datetime")]
        [Required]
        public DateTime? Due { get; set; }
        [StringLength(50)]
        public string Description { get; set; }

        [ForeignKey("AssignmentId")]
        [InverseProperty("Homework")]
        public virtual Assignment Assignment { get; set; }
        [ForeignKey("CourseId")]
        [InverseProperty("Homework")]
        public virtual Course Course { get; set; }
    }
}
