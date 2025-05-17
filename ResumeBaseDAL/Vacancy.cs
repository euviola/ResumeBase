using System.ComponentModel.DataAnnotations;

namespace ResumeBaseDAL
{
    //Клас "Вакансія". Містить опис вакансії, її назву та заробітню плату.
    public class Vacancy
    {
        [Key]
        public int ID { get; set; }

        public string Title { get; set; }
        public double Salary { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return $"Опис вакансії: {Title}; Заробітня плата: {Salary} грн в місяць;" + $"\nДетальний опис вакансії: {Description}";
        }
    }
}
