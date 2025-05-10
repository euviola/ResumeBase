using ResumeBaseBLL;
using ResumeBaseBLL.Mapper;
using ResumeBaseBLL.Models;
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

            Console.WriteLine("Resume:");
            resumeService.AddResume();
            resumeService.AddResume();
            Console.ReadKey();
        }
    }
}
