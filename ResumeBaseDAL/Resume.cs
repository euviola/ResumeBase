using System.ComponentModel.DataAnnotations;

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
