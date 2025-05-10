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
    public class VacancyService
    {
        private readonly AppDbContext _context;

        public VacancyService()
        {
            _context = new AppDbContext();
        }

        public void AddVacancy()
        {
            Console.WriteLine("Write a title");
            string title = Console.ReadLine();

            Console.WriteLine("Write a salary (UAH per month)");
            double salary = double.Parse(Console.ReadLine());

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
        }
    }
}
