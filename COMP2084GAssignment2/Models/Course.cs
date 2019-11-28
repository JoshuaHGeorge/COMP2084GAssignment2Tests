using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace COMP2084GAssignment2.Models
{
    public partial class Course
    {
        public Course()
        {
            Homework = new HashSet<Homework>();
        }
        [Key]
        public int CourseId { get; set; }
        [StringLength(50)]
        public string Name { get; set; }

        [InverseProperty("Course")]
        public virtual ICollection<Homework> Homework { get; set; }
    }
}
