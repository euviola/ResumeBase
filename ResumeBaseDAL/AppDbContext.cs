using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Reflection.Emit;

namespace ResumeBaseDAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("DefaultConnection") { }

        public DbSet<Resume> Resumes { get; set; }
        public DbSet<Vacancy> Vacancies { get; set; }
        public DbSet<Application> Applications { get; set; }
    }
}
