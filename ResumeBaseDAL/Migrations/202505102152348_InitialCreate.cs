namespace ResumeBaseDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Applications",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ResumeID = c.String(),
                        VacancyID = c.String(),
                        Status = c.String(),
                        Resume_ID = c.Int(),
                        Vacancy_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Resumes", t => t.Resume_ID)
                .ForeignKey("dbo.Vacancies", t => t.Vacancy_ID)
                .Index(t => t.Resume_ID)
                .Index(t => t.Vacancy_ID);
            
            CreateTable(
                "dbo.Resumes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FullName = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Vacancies",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Salary = c.Double(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Applications", "Vacancy_ID", "dbo.Vacancies");
            DropForeignKey("dbo.Applications", "Resume_ID", "dbo.Resumes");
            DropIndex("dbo.Applications", new[] { "Vacancy_ID" });
            DropIndex("dbo.Applications", new[] { "Resume_ID" });
            DropTable("dbo.Vacancies");
            DropTable("dbo.Resumes");
            DropTable("dbo.Applications");
        }
    }
}
