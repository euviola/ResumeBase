using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResumeBaseDAL
{
    //Клас "Резюме"
    public class Resume
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Description { get; set; }
    }
}
