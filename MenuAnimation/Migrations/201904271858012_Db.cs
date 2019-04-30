namespace Astmara6.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Db : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Branches",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdSection = c.Int(nullable: false),
                        Name = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sections", t => t.IdSection, cascadeDelete: true)
                .Index(t => t.IdSection);
            
            CreateTable(
                "dbo.Sections",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TypeOfSection = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StudentStatments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdLevel = c.Int(nullable: false),
                        IdBranch = c.Int(nullable: false),
                        IdSubject = c.Int(nullable: false),
                        NumberOfStudent = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Branches", t => t.IdBranch, cascadeDelete: true)
                .ForeignKey("dbo.Levels", t => t.IdLevel, cascadeDelete: true)
                .ForeignKey("dbo.Subjects", t => t.IdSubject, cascadeDelete: true)
                .Index(t => t.IdLevel)
                .Index(t => t.IdBranch)
                .Index(t => t.IdSubject);
            
            CreateTable(
                "dbo.Levels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Subjects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.Int(nullable: false),
                        Academic = c.String(),
                        Virtual = c.String(),
                        TotalHours = c.Int(nullable: false),
                        Name = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SubjectTeachers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdBranch = c.Int(nullable: false),
                        IdSubject = c.Int(nullable: false),
                        IdTeacher = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Branches", t => t.IdBranch, cascadeDelete: true)
                .ForeignKey("dbo.Subjects", t => t.IdSubject, cascadeDelete: true)
                .ForeignKey("dbo.Teachers", t => t.IdTeacher, cascadeDelete: true)
                .Index(t => t.IdBranch)
                .Index(t => t.IdSubject)
                .Index(t => t.IdTeacher);
            
            CreateTable(
                "dbo.Teachers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NickName = c.String(),
                        IdWorkHours = c.Int(nullable: false),
                        Name = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.WorkHours", t => t.IdWorkHours, cascadeDelete: true)
                .Index(t => t.IdWorkHours);
            
            CreateTable(
                "dbo.WorkHours",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GPA = c.Double(nullable: false),
                        Quorum = c.Int(nullable: false),
                        AcademicOrVirtual = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Password = c.String(),
                        Name = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StudentStatments", "IdSubject", "dbo.Subjects");
            DropForeignKey("dbo.SubjectTeachers", "IdTeacher", "dbo.Teachers");
            DropForeignKey("dbo.Teachers", "IdWorkHours", "dbo.WorkHours");
            DropForeignKey("dbo.SubjectTeachers", "IdSubject", "dbo.Subjects");
            DropForeignKey("dbo.SubjectTeachers", "IdBranch", "dbo.Branches");
            DropForeignKey("dbo.StudentStatments", "IdLevel", "dbo.Levels");
            DropForeignKey("dbo.StudentStatments", "IdBranch", "dbo.Branches");
            DropForeignKey("dbo.Branches", "IdSection", "dbo.Sections");
            DropIndex("dbo.Teachers", new[] { "IdWorkHours" });
            DropIndex("dbo.SubjectTeachers", new[] { "IdTeacher" });
            DropIndex("dbo.SubjectTeachers", new[] { "IdSubject" });
            DropIndex("dbo.SubjectTeachers", new[] { "IdBranch" });
            DropIndex("dbo.StudentStatments", new[] { "IdSubject" });
            DropIndex("dbo.StudentStatments", new[] { "IdBranch" });
            DropIndex("dbo.StudentStatments", new[] { "IdLevel" });
            DropIndex("dbo.Branches", new[] { "IdSection" });
            DropTable("dbo.Users");
            DropTable("dbo.WorkHours");
            DropTable("dbo.Teachers");
            DropTable("dbo.SubjectTeachers");
            DropTable("dbo.Subjects");
            DropTable("dbo.Levels");
            DropTable("dbo.StudentStatments");
            DropTable("dbo.Sections");
            DropTable("dbo.Branches");
        }
    }
}
