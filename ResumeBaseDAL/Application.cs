using System.ComponentModel.DataAnnotations;

namespace ResumeBaseDAL
{
    //Клас "заява". Є інтерпретацією реальної заяви на роботу: бере айді уже існуючої вакансії та резюме.
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
