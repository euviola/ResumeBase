using ResumeBaseBLL.Models;
using ResumeBaseDAL;
using System;
using System.Linq;

namespace ResumeBaseBLL
{
    // Клас реалізує бізнес-логіку для роботи із вакансіями.

    public class VacancyService
    {
        private readonly IRepository<Vacancy> _repository;

        public VacancyService(IRepository<Vacancy> repository)
        {
            _repository = repository;
        }

        // Додавання вакансії
        public void AddVacancy()
        {
            Console.WriteLine("Enter vacancy title:");
            string title = Console.ReadLine();

            double salary;
            Console.WriteLine("Enter salary (UAH per month):");
            while (!double.TryParse(Console.ReadLine(), out salary))
            {
                Console.WriteLine("Invalid number. Please enter a valid salary:");
            }

            Console.WriteLine("Enter description:");
            string description = Console.ReadLine();

            var vacancyDTO = new VacancyDTO
            {
                Title = title,
                Salary = salary,
                Description = description
            };

            var entity = Mapper.Mapper.ToEntity(vacancyDTO);
            _repository.Add(entity);
            _repository.SaveChanges();

            Console.WriteLine("Vacancy added successfully!");
            Console.WriteLine($"Vacancy saved with ID: {entity.ID}");
        }

        // Видалення вакансії
        public void RemoveVacancy()
        {
            Console.WriteLine("Enter the ID of the vacancy to remove:");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID.");
                return;
            }

            var vacancy = _repository.GetById(id);
            if (vacancy == null)
            {
                Console.WriteLine("Vacancy not found.");
                return;
            }

            _repository.Delete(id);
            _repository.SaveChanges();

            Console.WriteLine("Vacancy removed successfully.");
        }

        // Редагування вакансії
        public void EditVacancy()
        {
            Console.WriteLine("Enter the ID of the vacancy to edit:");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID.");
                return;
            }

            var vacancy = _repository.GetById(id);
            if (vacancy == null)
            {
                Console.WriteLine("Vacancy not found.");
                return;
            }

            Console.WriteLine("Enter new title (leave blank to keep current):");
            string title = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(title))
                vacancy.Title = title;

            Console.WriteLine("Enter new salary (leave blank to keep current):");
            string salaryInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(salaryInput) && double.TryParse(salaryInput, out double salary))
                vacancy.Salary = salary;

            Console.WriteLine("Enter new description (leave blank to keep current):");
            string description = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(description))
                vacancy.Description = description;

            _repository.Update(vacancy);
            _repository.SaveChanges();

            Console.WriteLine("Vacancy updated successfully.");
        }

        // Повернення вакансій
        public void GetAllVacancies()
        {
            var vacancies = _repository.GetAll().ToList();

            if (!vacancies.Any())
            {
                Console.WriteLine("No vacancies found.");
                return;
            }

            Console.WriteLine("List of all vacancies:");
            foreach (var vacancy in vacancies)
            {
                Console.WriteLine($"ID: {vacancy.ID}, Title: {vacancy.Title}, Salary: {vacancy.Salary} UAH");
                Console.WriteLine($"Description: {vacancy.Description}");
                Console.WriteLine(new string('-', 40));
            }
        }

        // Пошук вакансій
        public void FindVacancy()
        {
            Console.WriteLine("Enter title or keyword to search:");
            string searchTerm = Console.ReadLine().ToLower();

            var results = _repository.GetAll()
                .Where(v => v.Title != null && v.Title.ToLower().Contains(searchTerm))
                .Take(10)
                .ToList();

            if (!results.Any())
            {
                Console.WriteLine("No vacancies found matching the search term.");
                return;
            }

            Console.WriteLine("Matching vacancies (showing up to 10):");
            foreach (var vacancy in results)
            {
                Console.WriteLine($"ID: {vacancy.ID}, Title: {vacancy.Title}, Salary: {vacancy.Salary} UAH");
                Console.WriteLine($"Description: {vacancy.Description}");
                Console.WriteLine(new string('-', 40));
            }

            if (results.Count == 10)
                Console.WriteLine("More results may exist. Please refine your search if needed.");
        }
    }
}
