using Project.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Models.Entities;

public partial class Student
{
    [Key]
    public string StudentId { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public string? National { get; set; }   

    public DateTime? Birthday { get; set; }

    public string? Gender { get; set; }

    public string FacultyId { get; set; } = null!;

    public virtual Faculty? Faculty { get; set; }
}
