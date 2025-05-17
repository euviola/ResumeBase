using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResumeBaseBLL.ServiceInterfaces
{
    //Інтерфейс для сервісу керування вакансіями. 
    //Методи для додавання, видалення, перегляду, редагування та пошуку вакансій.
    public interface IVacancyService
    {
        void AddVacancy();
        void EditVacancy();
        void RemoveVacancy();
        void GetAllVacancies();
        void FindVacancy();
    }
}
