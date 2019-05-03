namespace Astmara6.Data
{
    using System.Data.Entity;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<Branch> Branchs { get; set; }
        public virtual DbSet<Level> Levels { get; set; }
        public virtual DbSet<Section> Sections { get; set; }
        public virtual DbSet<StudentStatement> StudentStatements { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<Subjects_Teachers> Subjects_Teachers { get; set; }
        public virtual DbSet<Teacher> Teachers { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<WorkHour> WorkHours { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Branch>()
                .HasMany(e => e.StudentStatements)
                .WithOptional(e => e.Branch)
                .HasForeignKey(e => e.id_branch);

            modelBuilder.Entity<Branch>()
                .HasMany(e => e.Subjects_Teachers)
                .WithOptional(e => e.Branch)
                .HasForeignKey(e => e.id_branch);

            modelBuilder.Entity<Level>()
                .HasMany(e => e.StudentStatements)
                .WithOptional(e => e.Level)
                .HasForeignKey(e => e.id_level);

            modelBuilder.Entity<Section>()
                .HasMany(e => e.Branchs)
                .WithOptional(e => e.Section)
                .HasForeignKey(e => e.id_section);

            modelBuilder.Entity<Subject>()
                .HasMany(e => e.StudentStatements)
                .WithOptional(e => e.Subject)
                .HasForeignKey(e => e.id_subject);

            modelBuilder.Entity<Subject>()
                .HasMany(e => e.Subjects_Teachers)
                .WithOptional(e => e.Subject)
                .HasForeignKey(e => e.id_subject)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Teacher>()
                .HasMany(e => e.Subjects_Teachers)
                .WithOptional(e => e.Teacher)
                .HasForeignKey(e => e.id_teacher)
                .WillCascadeOnDelete();

            modelBuilder.Entity<WorkHour>()
                .HasMany(e => e.Teachers)
                .WithOptional(e => e.WorkHour)
                .HasForeignKey(e => e.ID_WorkHours);
        }
    }
}
