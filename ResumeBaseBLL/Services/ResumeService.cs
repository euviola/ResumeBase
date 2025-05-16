using System;
using System.Collections.Generic;
using System.Linq;
using ResumeBaseBLL.Models;
using ResumeBaseBLL.Mapper;
using ResumeBaseDAL;
using ResumeBaseBLL.ServiceInterfaces;

namespace ResumeBaseBLL
{
    public class ResumeService : IResumeService
    {
        private readonly IRepository<Resume> _repository;

        public ResumeService(IRepository<Resume> repository)
        {
            _repository = repository;
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
            _repository.Add(entity);

            Console.WriteLine("Added successfully");
            Console.WriteLine($"Resume saved with ID: {entity.ID}");
        }

        public void EditResume()
        {
            Console.WriteLine("Enter the ID of the resume to edit:");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID.");
                return;
            }

            var entity = _repository.GetById(id);
            if (entity == null)
            {
                Console.WriteLine("Resume not found.");
                return;
            }

            Console.WriteLine("Enter new full name (leave blank to keep current):");
            string fullName = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(fullName))
            {
                entity.FullName = fullName;
            }

            Console.WriteLine("Enter new description (leave blank to keep current):");
            string description = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(description))
            {
                entity.Description = description;
            }

            _repository.Update(entity);
            Console.WriteLine("Resume updated successfully.");
        }

        public void RemoveResume()
        {
            Console.WriteLine("Enter the ID of the resume to remove:");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID.");
                return;
            }

            var entity = _repository.GetById(id);
            if (entity == null)
            {
                Console.WriteLine("Resume not found.");
                return;
            }

            _repository.Delete(id);
            Console.WriteLine("Resume removed successfully.");
        }

        public void GetAllResume()
        {
            var entities = _repository.GetAll().ToList();
            var resumes = entities.Select(Mapper.Mapper.ToDTO).ToList();

            if (resumes.Count == 0)
            {
                Console.WriteLine("No resumes found.");
                return;
            }

            Console.WriteLine("List of all resumes:");
            foreach (var resume in resumes)
            {
                Console.WriteLine($"ID: {resume.Id}, Name: {resume.FullName}, Description: {resume.Description}");
            }
        }

        public void FindResume()
        {
            Console.WriteLine("Enter full or partial name to search:");
            string searchTerm = Console.ReadLine().ToLower();

            var results = _repository.GetAll()
                .Where(r => r.FullName.ToLower().Contains(searchTerm))
                .Take(10)
                .ToList()
                .Select(Mapper.Mapper.ToDTO)
                .ToList();

            if (results.Count == 0)
            {
                Console.WriteLine("No resumes found matching the search term.");
                return;
            }

            Console.WriteLine("Matching resumes (showing up to 10):");
            foreach (var resume in results)
            {
                Console.WriteLine($"ID: {resume.Id}, Name: {resume.FullName}, Description: {resume.Description}");
            }

            if (results.Count == 10)
            {
                Console.WriteLine("More results may exist. Please refine your search if needed.");
            }
        }
    }
}
