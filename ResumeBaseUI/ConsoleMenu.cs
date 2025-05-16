namespace ResumeBaseUI
{
    public class ConsoleMenu
    {
        public int StartMenu()
        {
            Console.WriteLine("-—- Start Menu —--");
            Console.WriteLine("1. Resume configuration");
            Console.WriteLine("2. Vacancy configuration");
            Console.WriteLine("3. Application configuration");
            Console.WriteLine("4. Exit");
            int.TryParse(Console.ReadLine(), out var result);
            return result;
        }

        public int ResumeConf()
        {
            Console.WriteLine("--- Resume Configuration ---");
            Console.WriteLine("1. Add a resume");
            Console.WriteLine("2. Edit a resume");
            Console.WriteLine("3. Delete a resume");
            Console.WriteLine("4. Get all resumes");
            Console.WriteLine("5. Find resume");
            Console.WriteLine("6. Exit");
            int.TryParse(Console.ReadLine(), out var result);
            return result;
        }

        public int VacancyConf()
        {
            Console.WriteLine("--- Vacancy Configuration ---");
            Console.WriteLine("1. Add a vacancy");
            Console.WriteLine("2. Edit a vacancy");
            Console.WriteLine("3. Delete a vacancy");
            Console.WriteLine("4. Get all vacancies");
            Console.WriteLine("5. Find a vacancy");
            Console.WriteLine("6. Exit");
            int.TryParse(Console.ReadLine(), out var result);
            return result;
        }

        public int AppConf()
        {
            Console.WriteLine("--- Application Configuration ---");
            Console.WriteLine("1. Add an application to existing vacancy");
            Console.WriteLine("2. Delete an application");
            Console.WriteLine("3. Get all applications");
            Console.WriteLine("4. Find an application");
            Console.WriteLine("5. Change an application status");
            Console.WriteLine("6. Exit");
            int.TryParse(Console.ReadLine(), out var result);
            return result;
        }
    }
}
