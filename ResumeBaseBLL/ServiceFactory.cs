using ResumeBaseDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResumeBaseBLL
{
    public static class ServiceFactory
    {
        private static AppDbContext _context = new AppDbContext();

        private static IRepository<Resume> _resumeRepository = new ResumeRepository(_context);

        public static IResumeService CreateResumeService()
        {
            return new ResumeService(_resumeRepository);
        }


        public static IVacancyService CreateVacancyService()
        {
            return new VacancyService(new AppDbContext());
        }

        public static IApplicationService CreateApplicationService()
        {
            return new ApplicationService(new AppDbContext());
        }
    }
}
