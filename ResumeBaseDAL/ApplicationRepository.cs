using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResumeBaseDAL
{
    //Репозиторій заявок. Реалізує інтерфейс IRepository стосовно саме заявок.
    public class ApplicationRepository : IRepository<Application>
    {
        private readonly AppDbContext _context;

        public ApplicationRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(Application entity) => _context.Applications.Add(entity);

        public void Delete(int id)
        {
            var entity = _context.Applications.Find(id);
            if (entity != null)
                _context.Applications.Remove(entity);
        }

        public IEnumerable<Application> GetAll() => _context.Applications.ToList();

        public Application GetById(int id) => _context.Applications.Find(id);

        public void Update(Application entity)
        {
            _context.SaveChanges();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

    }
}
