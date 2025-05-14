using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResumeBaseBLL
{
    public enum UserRole
    {
        Admin,
        Employee,
        Worker,
        Guest
    }

    public class AccessManager
    {
        public static UserRole Login()
        {
            Console.WriteLine("##### Log into your account #####");
            string password = Console.ReadLine()?.ToLower();

            if (password == "admin") return UserRole.Admin;
            if (password == "employee") return UserRole.Employee;
            if (password == "worker") return UserRole.Worker;
            return UserRole.Guest;
        }

        public static bool HasAccess(UserRole role, string action)
        {
            if (role == UserRole.Admin)
                return true;

            switch (action.ToLower())
            {
                case "addvacancy":
                    return role == UserRole.Employee;

                case "addresume":
                    return role == UserRole.Worker;

                case "findapplication":
                    return role == UserRole.Employee || role == UserRole.Worker;

                default:
                    return false;
            }
        }

        public static void ExecuteIfAuthorized(UserRole role, string action, Action method)
        {
            if (HasAccess(role, action))
            {
                method();
            }
            else
            {
                Console.WriteLine("Access denied.");
            }

            Console.ReadKey();
        }


    }
}
