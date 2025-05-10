using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResumeBaseDAL
{
    //Клас "заявка". 
    public class Application
    {
        public int ID { get; set; }
        public int ResumeID { get; set; }
        public int VacancyID { get; set; }

        public Resume Resume { get; set; }
        public Vacancy Vacancy { get; set; }

        public string Status { get; set; }
    }
}
