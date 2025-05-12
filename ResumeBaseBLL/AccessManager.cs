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
            Console.WriteLine("--- Log into your account ---");
            string password = Console.ReadLine()?.ToLower();

            if (password == "admin") return UserRole.Admin;
            if (password == "employee") return UserRole.Employee;
            if (password == "worker") return UserRole.Worker;
            return UserRole.Guest;
        }

        public static bool HasAccess(UserRole role, string action)
        {
            switch (action.ToLower())
            {
                case "addresume":
                case "editresume":
                case "deleteresume":
                    return role == UserRole.Admin || role == UserRole.Worker;

                case "addvacancy":
                case "editvacancy":
                case "removevacancy":
                    return role == UserRole.Admin || role == UserRole.Employee;

                case "addapplication":
                case "editapplication":
                    return role != UserRole.Guest;

                case "view":
                    return role != UserRole.Guest;

                default:
                    return role == UserRole.Admin;
            }
        }
    }
}
