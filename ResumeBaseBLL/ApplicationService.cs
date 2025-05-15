using ResumeBaseDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using ResumeBaseBLL.Models;
using ResumeBaseBLL.Mapper;


namespace ResumeBaseBLL
{
    public class ApplicationService : IApplicationService
    {
        private readonly AppDbContext _context;

        public ApplicationService(AppDbContext context)
        {
            _context = context;
        }

        public void AddApplication()
        {
            Console.WriteLine("Enter Resume ID:");
            int resumeId = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter Vacancy ID:");
            int vacancyId = int.Parse(Console.ReadLine());

            var resume = _context.Resumes.FirstOrDefault(r => r.ID == resumeId);
            var vacancy = _context.Vacancies.FirstOrDefault(v => v.ID == vacancyId);

            if (resume == null)
            {
                Console.WriteLine($"Resume with ID {resumeId} not found!");
                return;
            }

            if (vacancy == null)
            {
                Console.WriteLine($"Vacancy with ID {vacancyId} not found!");
                return;
            }

            var applicationDto = new ApplicationDTO
            {
                ResumeID = resumeId.ToString(),
                VacancyID = vacancyId.ToString(),
                Status = "Pending"
            };

            var applicationEntity = Mapper.Mapper.ToEntity(applicationDto);
            applicationEntity.Resume = resume;
            applicationEntity.Vacancy = vacancy;

            _context.Applications.Add(applicationEntity);
            _context.SaveChanges();

            Console.WriteLine("Application added successfully!");
            Console.WriteLine($"Saved with ID: {applicationEntity.ID}");
        }


        public void RemoveApplication()
        {
            Console.WriteLine("Enter Application ID to remove:");
            int id = int.Parse(Console.ReadLine());

            var application = _context.Applications.FirstOrDefault(a => a.ID == id);

            if (application == null)
            {
                Console.WriteLine($"Application with ID {id} not found.");
                return;
            }

            _context.Applications.Remove(application);
            _context.SaveChanges();

            Console.WriteLine($"Application with ID {id} removed successfully.");
        }

        public void GetAllApplications()
        {
            var applications = _context.Applications
                .Include(a => a.Resume)
                .Include(a => a.Vacancy)
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

        public void FindApplication()
        {
            Console.WriteLine("Enter Application ID to find:");
            int id = int.Parse(Console.ReadLine());

            var app = _context.Applications
                .Include(a => a.Resume)
                .Include(a => a.Vacancy)
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

        public void SetApplicationStatus()
        {
            Console.WriteLine("Enter Application ID to find:");
            int id = int.Parse(Console.ReadLine());

            var app = _context.Applications
                .Include(a => a.Resume)
                .Include(a => a.Vacancy)
                .FirstOrDefault(a => a.ID == id);

            if (app == null)
            {
                Console.WriteLine($"Application with ID {id} not found.");
                return;
            }

            Console.WriteLine("Which status do you choose?");
            Console.WriteLine("1. Approve");
            Console.WriteLine("2. Reject");
            Console.WriteLine("3. Keep pending");

            int option = int.Parse(Console.ReadLine());
            switch(option)
            {
                case 1: ApproveApplication(app); break;
                case 2: RejectApplication(app); break;
                case 3: break;
            }
        }

        public void ApproveApplication(Application app)
        {
            app.Status = "Approved";
        }

        public void RejectApplication(Application app)
        {
            app.Status = "Rejected";
        }

    }
}
