using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Project.Models.Entities;
using static System.Formats.Asn1.AsnWriter;

namespace Project.Models.Entities;

public partial class ManageContext : DbContext
{
    public ManageContext()
    {
    }

    public ManageContext(DbContextOptions<ManageContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Faculty> Faculties { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Subject> Subjects { get; set; }

    public virtual DbSet<Score> Scores { get; set; }    
    public virtual DbSet<SubjectClass> SubjectClasses { get; set; }
    public virtual DbSet<Lecturer> Lecturers { get; set; }
    public virtual DbSet<Machine> Machines { get; set; }
    public virtual DbSet<Predictive> Predictives { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=LAPTOP-TC1PJ34D\\LONG;Initial Catalog=student;Integrated Security=True;TrustServerCertificate=True;");

}
