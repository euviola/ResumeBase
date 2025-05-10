using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResumeBaseBLL.Models
{
    public class VacancyDTO
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public double Salary { get; set; }
        public string Description { get; set; }
    }
}
