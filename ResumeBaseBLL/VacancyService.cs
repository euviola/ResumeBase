using ResumeBaseBLL.Models;
using ResumeBaseBLL.Mapper;
using ResumeBaseDAL;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResumeBaseBLL
{
    public class VacancyService : IVacancyService
    {
        private readonly AppDbContext _context;

        public VacancyService(AppDbContext context)
        {
            _context = context;
        }

        public void AddVacancy()
        {
            Console.WriteLine("Write a title");
            string title = Console.ReadLine();

            Console.WriteLine("Write a salary (UAH per month)");
            double salary = double.Parse(Console.ReadLine());

            while (!double.TryParse(Console.ReadLine(), out salary))
            {
                Console.WriteLine("Invalid number. Please enter a valid salary:");
            }

            Console.WriteLine("Write a desciprion");
            string description = Console.ReadLine();

            var vacancyDTO = new VacancyDTO
            {
                Title = title,
                Salary = salary,
                Description = description
            };

            var entity = Mapper.Mapper.ToEntity(vacancyDTO);
            _context.Vacancies.Add(entity);
            _context.SaveChanges();

            Console.WriteLine("Added successfully");
            Console.WriteLine($"Vacancy saved with ID: {entity.ID}");
        }

        public void RemoveVacancy()
        {
            Console.WriteLine("Enter the ID of the vacancy to remove:");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID.");
                return;
            }

            var entity = _context.Vacancies.Find(id);
            if (entity == null)
            {
                Console.WriteLine("Vacancy not found.");
                return;
            }

            _context.Vacancies.Remove(entity);
            _context.SaveChanges();
            Console.WriteLine("Vacancy removed successfully.");
        }


        public void EditVacancy()
        {
            Console.WriteLine("Enter the ID of the vacancy to edit:");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID.");
                return;
            }

            var entity = _context.Vacancies.Find(id);
            if (entity == null)
            {
                Console.WriteLine("Vacancy not found.");
                return;
            }

            Console.WriteLine("Enter new title (leave blank to keep current):");
            string title = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(title))
            {
                entity.Title = title;
            }

            Console.WriteLine("Enter new salary (leave blank to keep current):");
            string salaryInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(salaryInput) && double.TryParse(salaryInput, out double salary))
            {
                entity.Salary = salary;
            }

            Console.WriteLine("Enter new description (leave blank to keep current):");
            string description = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(description))
            {
                entity.Description = description;
            }

            _context.SaveChanges();
            Console.WriteLine("Vacancy updated successfully.");
        }

        public void GetAllVacancies()
        {
            var entities = _context.Vacancies.ToList();
            var vacancies = entities.Select(Mapper.Mapper.ToDTO).ToList();

            if (vacancies.Count == 0)
            {
                Console.WriteLine("No vacancies found.");
                return;
            }

            Console.WriteLine("List of all vacancies:");
            foreach (var vacancy in vacancies)
            {
                Console.WriteLine($"ID: {vacancy.ID}, Title: {vacancy.Title}, Salary: {vacancy.Salary} UAH, Description: {vacancy.Description}");
            }
        }

        public void FindVacancy()
        {
            Console.WriteLine("Enter title or keyword to search:");
            string searchTerm = Console.ReadLine().ToLower();

            var results = _context.Vacancies
                .Where(v => v.Title.ToLower().Contains(searchTerm))
                .Take(10)
                .ToList()
                .Select(Mapper.Mapper.ToDTO)
                .ToList();

            if (results.Count == 0)
            {
                Console.WriteLine("No vacancies found matching the search term.");
                return;
            }

            Console.WriteLine("Matching vacancies (showing up to 10):");
            foreach (var vacancy in results)
            {
                Console.WriteLine($"ID: {vacancy.ID}, Title: {vacancy.Title}, Salary: {vacancy.Salary} UAH, Description: {vacancy.Description}");
            }

            if (results.Count == 10)
            {
                Console.WriteLine("More results may exist. Please refine your search if needed.");
            }
        }

    }
}
