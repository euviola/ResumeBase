using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResumeBaseUI
{
    public class ConsoleMenu
    {
        public int StartMenu()
        {
            Console.WriteLine("—- Start Menu —-");
            Console.WriteLine("1. Resume configuration");
            Console.WriteLine("2. Vacancy configuration");
            Console.WriteLine("3. Application configuration");
            Console.WriteLine("4. Exit");
            int.TryParse(Console.ReadLine(), out var result);
            return result;
        }

    }
}
