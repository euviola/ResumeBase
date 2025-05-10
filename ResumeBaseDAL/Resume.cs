using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResumeBaseDAL
{
    //Клас "Резюме"
    public class Resume
    {
        [Key]
        public int ID { get; set; }

        public string FullName { get; set; }
        public string Description { get; set; }
    }
}
