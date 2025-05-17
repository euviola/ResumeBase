
using System.Data.Entity;

namespace ResumeBaseDAL
{
    //Клас контексту. Потрібен для створення бази даних в EF
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("DefaultConnection") { }

        public DbSet<Resume> Resumes { get; set; }
        public DbSet<Vacancy> Vacancies { get; set; }
        public DbSet<Application> Applications { get; set; }
    }
}
