using ResumeBaseDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;


namespace ResumeBaseBLL
{
    public class ApplicationService
    {
        private readonly AppDbContext _context;

        public ApplicationService()
        {
            _context = new AppDbContext();
        }

        public void AddApplication()
        {
            Console.WriteLine("Enter Resume ID:");
            int resumeId = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter Vacancy ID:");
            int vacancyId = int.Parse(Console.ReadLine());

            var resume = _context.Resumes.FirstOrDefault(r => r.ID == resumeId);
            var vacancy = _context.Vacancies.FirstOrDefault(v => v.ID == vacancyId);

            if (resume == null || vacancy == null)
            {
                Console.WriteLine("Resume or Vacancy not found!");
                return;
            }

            Console.WriteLine("Enter Application ID:");
            int applicationId = int.Parse(Console.ReadLine());

            if (_context.Applications.Any(r => r.ID == applicationId))
            {
                Console.WriteLine($"Resume with ID {applicationId} already exists!");
                return;
            }

            var application = new ResumeBaseDAL.Application
            {
                ID = applicationId,
                ResumeID = resumeId.ToString(),
                VacancyID = vacancyId.ToString(),
                Resume = resume,
                Vacancy = vacancy,
                Status = "Pending"
            };

            _context.Applications.Add(application);
            _context.SaveChanges();

            Console.WriteLine("Application added successfully!");
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



    }
}
