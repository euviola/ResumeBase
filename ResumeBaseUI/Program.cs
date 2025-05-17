using ResumeBaseBLL;

namespace ResumeBaseUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Оголшення об'єктів сервісів для реалізації основної логіки.
            var resumeService = ServiceFactory.CreateResumeService();
            var vacancyService = ServiceFactory.CreateVacancyService();
            var appService = ServiceFactory.CreateApplicationService();

            //Оголошення консольного меню для використання його методів.
            ConsoleMenu cs = new ConsoleMenu();
            
            //Об'єкт енумератора ролі для надання доступу до даних.
            UserRole userRole = new UserRole();
            bool isRunning = false, StartMenu = false, ResumeMenu = false, VacancyMenu = false, AppMenu = false;

            //Виклик методу Логін для визначення ролі користувача
            userRole = AccessManager.Login();

            isRunning = true;
            StartMenu = true;
            Console.Clear();

            while (isRunning)
            {
                while (StartMenu)
                {
                    var options = cs.StartMenu();
                    switch (options)
                    {
                        case 1:
                            if (userRole != UserRole.Guest)
                            {
                                ResumeMenu = true; StartMenu = false; break;
                            }
                            else { Console.WriteLine("You need to log in to have an access"); Console.ReadKey(); break; }

                        case 2:
                            if (userRole != UserRole.Guest)
                            {
                                VacancyMenu = true; StartMenu = false; break;
                            }
                            else { Console.WriteLine("You need to log in to have an access"); Console.ReadKey(); break; }

                        case 3:
                            if (userRole != UserRole.Guest)
                            {
                                AppMenu = true; StartMenu = false; break;
                            }
                            else { Console.WriteLine("You need to log in to have an access"); Console.ReadKey(); break; }

                        case 4:
                            StartMenu = false; isRunning = false; break;
                    }
                    Console.Clear();
                }

                while (ResumeMenu)
                {
                    var options = cs.ResumeConf();
                    try
                    {
                        switch (options)
                        {
                            case 1:
                                AccessManager.ExecuteIfAuthorized(userRole, "addresume", resumeService.AddResume); break;
                            case 2:
                                AccessManager.ExecuteIfAuthorized(userRole, "editresume", resumeService.EditResume); break;
                            case 3:
                                AccessManager.ExecuteIfAuthorized(userRole, "removeresume", resumeService.RemoveResume); break;
                            case 4:
                                AccessManager.ExecuteIfAuthorized(userRole, "getallresume", resumeService.GetAllResume); break;
                            case 5:
                                AccessManager.ExecuteIfAuthorized(userRole, "findresume", resumeService.FindResume); break;
                            case 6:
                                ResumeMenu = false; StartMenu = true; break;
                        }
                    }
                    catch (UnauthorizedAccessException ex)
                    {
                        Console.WriteLine($"Access denied: {ex.Message}");
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine($"Invalid input: {ex.Message}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                    }

                }

                while (VacancyMenu)
                {
                    var options = cs.VacancyConf();
                    try
                    {
                        switch (options)
                        {
                            case 1:
                                AccessManager.ExecuteIfAuthorized(userRole, "addvacancy", vacancyService.AddVacancy); Console.ReadKey(); break;
                            case 2:
                                AccessManager.ExecuteIfAuthorized(userRole, "editvacancy", vacancyService.EditVacancy); Console.ReadKey(); break;
                            case 3:
                                AccessManager.ExecuteIfAuthorized(userRole, "removevacancy", vacancyService.RemoveVacancy); Console.ReadKey(); break;
                            case 4:
                                AccessManager.ExecuteIfAuthorized(userRole, "getallvacancies", vacancyService.GetAllVacancies); Console.ReadKey(); break;
                            case 5:
                                AccessManager.ExecuteIfAuthorized(userRole, "findvacancy", vacancyService.FindVacancy); Console.ReadKey(); break;
                            case 6:
                                StartMenu = true; VacancyMenu = false; break;
                        }
                        Console.Clear();
                    }
                    catch (UnauthorizedAccessException ex)
                    {
                        Console.WriteLine($"Access denied: {ex.Message}");
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine($"Invalid input: {ex.Message}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                    }
                }

                while (AppMenu)
                {
                    var options = cs.AppConf();

                    try
                    {
                        switch (options)
                        {
                            case 1:
                                AccessManager.ExecuteIfAuthorized(userRole, "addapplication", appService.AddApplication); Console.ReadKey(); break;
                            case 2:
                                AccessManager.ExecuteIfAuthorized(userRole, "removeapplication", appService.RemoveApplication); Console.ReadKey(); break;
                            case 3:
                                AccessManager.ExecuteIfAuthorized(userRole, "getallapplications", appService.GetAllApplications); Console.ReadKey(); break;
                            case 4:
                                AccessManager.ExecuteIfAuthorized(userRole, "findapplication", appService.FindApplication); Console.ReadKey(); break;
                            case 5:
                                AccessManager.ExecuteIfAuthorized(userRole, "setapplicationstatus", appService.SetApplicationStatus); Console.ReadKey(); break;
                            case 6:
                                StartMenu = true; AppMenu = false; break;
                        }
                        Console.Clear();
                    }
                    catch (UnauthorizedAccessException ex)
                    {
                        Console.WriteLine($"Access denied: {ex.Message}");
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine($"Invalid input: {ex.Message}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                    }
                }
            }
        }
    }
}

