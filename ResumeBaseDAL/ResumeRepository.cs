using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResumeBaseDAL
{
    public class ResumeRepository : IRepository<Resume>
    {
        private readonly AppDbContext _context;

        public ResumeRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(Resume entity) => _context.Resumes.Add(entity);

        public void Delete(int id)
        {
            var entity = _context.Resumes.Find(id);
            if (entity != null)
                _context.Resumes.Remove(entity);
        }

        public IEnumerable<Resume> GetAll() => _context.Resumes.ToList();

        public Resume GetById(int id) => _context.Resumes.Find(id);

        public void Update(Resume entity)
        {
            _context.SaveChanges(); 
        }
    }

}
