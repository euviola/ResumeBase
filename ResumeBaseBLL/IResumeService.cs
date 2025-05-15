using ResumeBaseBLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResumeBaseBLL
{
    public interface IResumeService
    {
        void AddResume();
        void EditResume();
        void RemoveResume();
        void GetAllResume();
        void FindResume();
    }

}
