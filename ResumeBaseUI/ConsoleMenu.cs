using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResumeBaseUI
{
    public class ConsoleMenu
    {
        public string DataAccessMenu()
        {
            Console.WriteLine("--- Log into your account ---");
            string password = Console.ReadLine();
            if (password.ToLower().ToString() == "admin")
            {
                return "admin";
            }
            else if (password.ToLower().ToString() == "employee")
            {
                return "employee";
            }
            else if(password.ToLower().ToString() == "worker")
            {
                return "worker";
            }
            else
            {
                return "guest";
            }
        }

        public int StartMenu()
        {
            Console.WriteLine("--- Start Menu ---");
            int.TryParse(Console.ReadLine(), out var result);
            return result;
        }

    }
}
