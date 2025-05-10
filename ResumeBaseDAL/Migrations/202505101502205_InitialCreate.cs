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
                        ResumeID = c.Int(nullable: false),
                        VacancyID = c.Int(nullable: false),
                        Status = c.String(),
                        Vacancy_ID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Resumes", t => t.ResumeID, cascadeDelete: true)
                .ForeignKey("dbo.Vacancies", t => t.Vacancy_ID)
                .Index(t => t.ResumeID)
                .Index(t => t.Vacancy_ID);
            
            CreateTable(
                "dbo.Resumes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FullName = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Vacancies",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128),
                        Title = c.String(),
                        Salary = c.Double(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Applications", "Vacancy_ID", "dbo.Vacancies");
            DropForeignKey("dbo.Applications", "ResumeID", "dbo.Resumes");
            DropIndex("dbo.Applications", new[] { "Vacancy_ID" });
            DropIndex("dbo.Applications", new[] { "ResumeID" });
            DropTable("dbo.Vacancies");
            DropTable("dbo.Resumes");
            DropTable("dbo.Applications");
        }
    }
}
