using ResumeBaseDAL;
using ResumeBaseBLL.Models;
using System;
using System.Linq;

namespace ResumeBaseBLL
{
    // Клас реалізує бізнес-логіку для роботи із заявками на вакансії.
    // Надає методи для додавання, видалення, перегляду, пошуку та зміни статусу заявок.
    // Використовує репозиторії для доступу до даних заявок, резюме та вакансій.
    public class ApplicationService
    {
        private readonly IRepository<Application> _applicationRepository;
        private readonly IRepository<Resume> _resumeRepository;
        private readonly IRepository<Vacancy> _vacancyRepository;

        public ApplicationService(
            IRepository<Application> applicationRepository,
            IRepository<Resume> resumeRepository,
            IRepository<Vacancy> vacancyRepository)
        {
            _applicationRepository = applicationRepository;
            _resumeRepository = resumeRepository;
            _vacancyRepository = vacancyRepository;
        }

        //Метод додавання заявки.
        public void AddApplication()
        {
            Console.WriteLine("Enter Resume ID:");
            if (!int.TryParse(Console.ReadLine(), out int resumeId))
            {
                Console.WriteLine("Invalid Resume ID.");
                return;
            }

            Console.WriteLine("Enter Vacancy ID:");
            if (!int.TryParse(Console.ReadLine(), out int vacancyId))
            {
                Console.WriteLine("Invalid Vacancy ID.");
                return;
            }

            var resume = _resumeRepository.GetAll().FirstOrDefault(r => r.ID == resumeId);
            var vacancy = _vacancyRepository.GetAll().FirstOrDefault(v => v.ID == vacancyId);

            if (resume != null && vacancy != null)
            {
                var applicationDto = new ApplicationDTO
                {
                    ResumeID = resumeId.ToString(),
                    VacancyID = vacancyId.ToString(),
                    Status = "Pending"
                };

                var applicationEntity = Mapper.Mapper.ToEntity(applicationDto);
                applicationEntity.Resume = resume;
                applicationEntity.Vacancy = vacancy;

                _applicationRepository.Add(applicationEntity);
                _applicationRepository.SaveChanges();

                Console.WriteLine("Application added successfully!");
                Console.WriteLine($"Saved with ID: {applicationEntity.ID}");
            }
            else
            {
                Console.WriteLine("Resume not found!");
                return;
            }
        }

        //Метод видалення заявки
        public void RemoveApplication()
        {
            Console.WriteLine("Enter Application ID to remove:");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID.");
                return;
            }

            var application = _applicationRepository.GetAll().FirstOrDefault(a => a.ID == id);

            if (application == null)
            {
                Console.WriteLine($"Application with ID {id} not found.");
                return;
            }

            _applicationRepository.Delete(application.ID);
            _applicationRepository.SaveChanges();

            Console.WriteLine($"Application with ID {id} removed successfully.");
        }

        //Метод отримання заявок
        public void GetAllApplications()
        {
            var applications = _applicationRepository.GetAll()
                .ToList();

            if (!applications.Any())
            {
                Console.WriteLine("No applications found.");
                return;
            }

            foreach (var app in applications)
            {
                Console.WriteLine($"Application ID: {app.ID}");
                Console.WriteLine($"   Resume: {app.Resume?.FullName ?? "N/A"}");
                Console.WriteLine($"   Vacancy: {app.Vacancy?.Title ?? "N/A"}");
                Console.WriteLine($"   Status: {app.Status}");
                Console.WriteLine(new string('-', 40));
            }
        }

        //Метод пошуку заявки
        public void FindApplication()
        {
            Console.WriteLine("Enter Application ID to find:");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID.");
                return;
            }

            var app = _applicationRepository.GetAll()
                .FirstOrDefault(a => a.ID == id);

            if (app == null)
            {
                Console.WriteLine($"Application with ID {id} not found.");
                return;
            }

            Console.WriteLine($"Application ID: {app.ID}");
            Console.WriteLine($"   Resume: {app.Resume?.FullName ?? "N/A"}");
            Console.WriteLine($"   Vacancy: {app.Vacancy?.Title ?? "N/A"}");
            Console.WriteLine($"   Status: {app.Status}");
        }

        //Метод встановлення статусу зявки
        public void SetApplicationStatus()
        {
            Console.WriteLine("Enter Application ID:");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID.");
                return;
            }

            var app = _applicationRepository.GetAll().FirstOrDefault(a => a.ID == id);

            if (app == null)
            {
                Console.WriteLine($"Application with ID {id} not found.");
                return;
            }

            Console.WriteLine("Choose a new status:");
            Console.WriteLine("1. Approve");
            Console.WriteLine("2. Reject");
            Console.WriteLine("3. Keep Pending");

            if (!int.TryParse(Console.ReadLine(), out int option))
            {
                Console.WriteLine("Invalid option.");
                return;
            }

            switch (option)
            {
                case 1:
                    ApproveApplication(app);
                    break;
                case 2:
                    RejectApplication(app);
                    break;
                case 3:
                    Console.WriteLine("Status left unchanged.");
                    return;
                default:
                    Console.WriteLine("Invalid option.");
                    return;
            }

            _applicationRepository.Update(app);
            _applicationRepository.SaveChanges();
            Console.WriteLine("Application status updated successfully.");
        }

        //Метод одобрення заявки
        private void ApproveApplication(Application app)
        {
            app.Status = "Approved";
        }

        //Метод відмови заявці.
        private void RejectApplication(Application app)
        {
            app.Status = "Rejected";
        }
    }
}
