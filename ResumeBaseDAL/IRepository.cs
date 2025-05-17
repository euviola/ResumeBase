using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResumeBaseDAL
{
    //Інтерфейс репозиторію. Відповідає за правила збереження, пошуку, повернення, оновлення та видалення даних у БД.
    public interface IRepository<T> where T : class
    {
        T GetById(int id);
        IEnumerable<T> GetAll();
        void Add(T entity);
        void Update(T entity);
        void Delete(int id);

        void SaveChanges();
    }

}
