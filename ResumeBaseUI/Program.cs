using ResumeBaseBLL;
using ResumeBaseBLL.Mapper;
using ResumeBaseBLL.Models;
using ResumeBaseBLL.Enums
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResumeBaseUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var resumeService = new ResumeService();
            var vacancyService = new VacancyService();
            ConsoleMenu cs = new ConsoleMenu();

            Console.WriteLine("Resume:");
            resumeService.GetAllResume();
            Console.ReadKey();
        }
    }
}
