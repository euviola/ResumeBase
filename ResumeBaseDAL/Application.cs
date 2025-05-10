using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResumeBaseDAL
{
    //Клас "заявка". 
    public class Application
    {
        [Key]
        public int ID { get; set; }
        public string ResumeID { get; set; }
        public string VacancyID { get; set; }

        public Resume Resume { get; set; }
        public Vacancy Vacancy { get; set; }

        public string Status { get; set; }
    }
}
