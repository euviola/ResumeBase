using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResumeBaseBLL.ServiceInterfaces
{
    public interface IVacancyService
    {
        void AddVacancy();
        void EditVacancy();
        void RemoveVacancy();
        void GetAllVacancies();
        void FindVacancy();
    }
}
