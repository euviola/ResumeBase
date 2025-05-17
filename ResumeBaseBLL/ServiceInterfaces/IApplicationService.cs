using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResumeBaseBLL
{
    //Інтерфейс для сервісу керування заявок. 
    //Методи для додавання, видалення, перегляду, пошуку та зміни статусу заявок.
    public interface IApplicationService
    {
        void AddApplication();
        void RemoveApplication();
        void GetAllApplications();
        void FindApplication();
        void SetApplicationStatus();
        void ApproveApplication(ResumeBaseDAL.Application app);
        void RejectApplication(ResumeBaseDAL.Application app);
    }
}
