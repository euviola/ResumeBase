using ResumeBaseBLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResumeBaseBLL.ServiceInterfaces
{
    //Інтерфейс для сервісу керування резюме. 
    //Методи для додавання, видалення, перегляду, редагування та пошуку резюме.
    public interface IResumeService
    {
        void AddResume();
        void EditResume();
        void RemoveResume();
        void GetAllResume();
        void FindResume();
    }
}
