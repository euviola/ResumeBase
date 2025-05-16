namespace ResumeBaseBLL.Models
{
    public class ApplicationDTO
    {
        public int ID { get; set; }

        public string ResumeID { get; set; }
        public string VacancyID { get; set; }

        public ResumeDTO Resume { get; set; }
        public VacancyDTO Vacancy { get; set; }
        public string Status { get; set; }
    }
}
