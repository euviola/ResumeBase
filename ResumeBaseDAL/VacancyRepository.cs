using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResumeBaseDAL
{
    public class VacancyRepository : IRepository<Vacancy>
    {
        private readonly AppDbContext _context;

        public VacancyRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(Vacancy entity) => _context.Vacancies.Add(entity);

        public void Delete(int id)
        {
            var entity = _context.Vacancies.Find(id);
            if (entity != null)
                _context.Vacancies.Remove(entity);
        }

        public IEnumerable<Vacancy> GetAll() => _context.Vacancies.ToList();

        public Vacancy GetById(int id) => _context.Vacancies.Find(id);

        public void Update(Vacancy entity)
        {
            _context.SaveChanges();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

    }
}
