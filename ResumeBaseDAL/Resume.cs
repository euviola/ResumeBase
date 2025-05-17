using System.ComponentModel.DataAnnotations;

namespace ResumeBaseDAL
{
    //Клас "Резюме". Містить повне ім'я автора та довільний опис.
    public class Resume
    {
        [Key]
        public int ID { get; set; }

        public string FullName { get; set; }
        public string Description { get; set; }
    }
}
