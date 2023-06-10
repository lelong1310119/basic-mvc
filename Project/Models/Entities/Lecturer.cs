using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Models.Entities;

public partial class Lecturer
{
    [Key]
    public string LecturerId { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public DateTime? Date { get; set; }

    public string Gender { get; set; } = null!;

    public virtual ICollection<SubjectClass> SubjectClasses { get; } = new List<SubjectClass>();
}
