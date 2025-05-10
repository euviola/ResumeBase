using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResumeBaseBLL.Models;
using ResumeBaseBLL.Mapper;
using ResumeBaseDAL;

namespace ResumeBaseBLL
{
    //Клас ResumeService - тут відбуватимуться усі дії над списком резюме
    public class ResumeService
    {
        private readonly AppDbContext _context;

        public ResumeService()
        {
            _context = new AppDbContext(); 
        }

        public void AddResume()
        {
            Console.WriteLine("Enter full name:");
            string fullName = Console.ReadLine();

            Console.WriteLine("Enter description:");
            string description = Console.ReadLine();

            var resumeDto = new ResumeDTO
            {
                FullName = fullName,
                Description = description
            };

            var entity = Mapper.Mapper.ToEntity(resumeDto);
            _context.Resumes.Add(entity);
            _context.SaveChanges();

            Console.WriteLine("Added successfully");
        }

        public void EditResume()
        {

        }

        public void RemoveResume()
        {

        }

        public void GetAllResume()
        {

        }

        public void FindResume()
        {

        }
    }
}
