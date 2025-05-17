namespace ResumeBaseBLL.Models
{
    //DTO-варіант класу вакансій.
    public class VacancyDTO
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public double Salary { get; set; }
        public string Description { get; set; }
    }
}
