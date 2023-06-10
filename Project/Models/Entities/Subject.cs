using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace Project.Models.Entities
{
    public partial class Subject
    {
        [Key]
        public string SubjectId { get; set; } = null!;

        public string SubjectName { get; set; } = null!;
    }
}
