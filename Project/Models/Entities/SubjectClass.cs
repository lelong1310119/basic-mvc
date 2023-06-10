using Project.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Models.Entities;

public partial class SubjectClass
{
    [Key]
    public string SubjectClassId { get; set; } = null!;

    public string SubjectId { get; set; } = null!;

    public string LecturerId { get; set; } = null!;

    public virtual Lecturer? Lecturer { get; set; }

    public virtual Subject? Subject { get; set; }
}
