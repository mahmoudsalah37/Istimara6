namespace Astmara6.Data
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class CollegeContext : DbContext
    {
        public CollegeContext()
            : base("name=CollegeContext")
        {
        }

        public virtual DbSet<Branch> Branches { get; set; }
        public virtual DbSet<Level> Levels { get; set; }
        public virtual DbSet<Section> Sections { get; set; }
        public virtual DbSet<StudentStatment> StudentStatments { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<SubjectTeacher> SubjectTeachers { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<Teacher> Teachers { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<WorkHour> WorkHours { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Branch>()
                .HasMany(e => e.StudentStatments)
                .WithOptional(e => e.Branch)
                .HasForeignKey(e => e.IdBranch);

            modelBuilder.Entity<Branch>()
                .HasMany(e => e.SubjectTeachers)
                .WithOptional(e => e.Branch)
                .HasForeignKey(e => e.IdBranch);

            modelBuilder.Entity<Level>()
                .HasMany(e => e.StudentStatments)
                .WithOptional(e => e.Level)
                .HasForeignKey(e => e.IdLevel);

            modelBuilder.Entity<Level>()
                .HasMany(e => e.SubjectTeachers)
                .WithOptional(e => e.Level)
                .HasForeignKey(e => e.IdLevel);

            modelBuilder.Entity<Section>()
                .HasMany(e => e.Branches)
                .WithOptional(e => e.Section)
                .HasForeignKey(e => e.IdSection);

            modelBuilder.Entity<Section>()
                .HasMany(e => e.Teachers)
                .WithOptional(e => e.Section)
                .HasForeignKey(e => e.idSection);

            modelBuilder.Entity<Subject>()
                .HasMany(e => e.StudentStatments)
                .WithOptional(e => e.Subject)
                .HasForeignKey(e => e.IdSubject);

            modelBuilder.Entity<Subject>()
                .HasMany(e => e.SubjectTeachers)
                .WithOptional(e => e.Subject)
                .HasForeignKey(e => e.IdSubject);

            modelBuilder.Entity<Teacher>()
                .HasMany(e => e.SubjectTeachers)
                .WithOptional(e => e.Teacher)
                .HasForeignKey(e => e.IdTeacher);

            modelBuilder.Entity<WorkHour>()
                .HasMany(e => e.Teachers)
                .WithOptional(e => e.WorkHour)
                .HasForeignKey(e => e.IdWorkHours);
        }
    }
}
