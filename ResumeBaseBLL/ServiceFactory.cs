using ResumeBaseDAL;
using ResumeBaseBLL.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResumeBaseBLL
{
    // Фабричний клас для створення екземплярів сервісів бізнес-логіки разом з їхніми залежностями.
    // Ініціалізує відповідні репозиторії та передає їх у конструктори сервісів.
    // Забезпечує створення окремого екземпляра AppDbContext для кожного сервісу.

    public static class ServiceFactory
    {
        public static ResumeService CreateResumeService()
        {
            var dbContext = new AppDbContext(); 
            var resumeRepository = new ResumeRepository(dbContext);
            return new ResumeService(resumeRepository);
        }

        public static VacancyService CreateVacancyService()
        {
            var dbContext = new AppDbContext();
            var vacancyRepository = new VacancyRepository(dbContext);
            return new VacancyService(vacancyRepository);
        }

        public static ApplicationService CreateApplicationService()
        {
            var dbContext = new AppDbContext();
            var applicationService = new ApplicationService(
                        new ApplicationRepository(dbContext),
                        new ResumeRepository(dbContext),
                        new VacancyRepository(dbContext)
            );
            return applicationService;
        }
    }
}
