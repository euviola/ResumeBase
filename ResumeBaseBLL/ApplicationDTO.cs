using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResumeBaseBLL.Models
{
    public class ApplicationDTO
    {
        public int ID { get; set; }
        public int ResumeID { get; set; }
        public int VacancyID { get; set; }

        public ResumeDTO Resume { get; set; }
        public VacancyDTO Vacancy { get; set; }
        public string Status { get; set; }
    }
}
