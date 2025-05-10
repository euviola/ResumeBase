using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResumeBaseDAL
{
    //Клас "Вакансія"
    public class Vacancy
    {
        public string ID { get; set; }
        public string Title { get; set; }
        public double Salary { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return $"Опис вакансії: {Title}; Заробітня плата: {Salary} грн в місяць;" + $"\nДетальний опис вакансії: {Description}";
        }
    }
}
