using Project.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models.Entities;

public partial class Score
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ScoreId { get; set; }
    public string StudentId { get; set; } = null!;

    public string SubjectClassId { get; set; } = null!;

    public double? ScoreSubject { get; set; }

    public DateTime? Date { get; set; }

    public virtual Student? Student { get; set; }

    public virtual SubjectClass? SubjectClass { get; set; }
}
