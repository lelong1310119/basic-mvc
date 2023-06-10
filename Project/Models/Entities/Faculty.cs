using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Models.Entities;

public partial class Faculty
{
    [Key]
    public string FacultyId { get; set; } = null!;

    public string FacultyName { get; set; } = null!;   
    public virtual ICollection<Student> Students { get; } = new List<Student>();

}
