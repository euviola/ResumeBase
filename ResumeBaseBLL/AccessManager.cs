namespace ResumeBaseBLL
{
    //енумератор ролей доступу
    public enum UserRole
    {
        Admin,
        Employee,
        Worker,
        Guest
    }

    public class AccessManager
    {
        //Клас, який відповідає за доступ до певних даних.

        //Метод Login() повертає певне значення із енумератора, яке потім використовується для перевірки доступу
        public static UserRole Login()
        {
            Console.WriteLine("##### Log into your account #####");
            string password = Console.ReadLine()?.ToLower();

            if (password == "admin") return UserRole.Admin;
            if (password == "employee") return UserRole.Employee;
            if (password == "worker") return UserRole.Worker;
            return UserRole.Guest;
        }

        //Перевірка на наявність доступу ролі. Приймає дію (метод) і повертає булеве значення
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

        //Виконує перевірку. У разі успішності запускає обраний метод.
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
