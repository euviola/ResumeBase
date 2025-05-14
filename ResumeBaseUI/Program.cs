using ResumeBaseBLL;
using ResumeBaseBLL.Mapper;
using ResumeBaseBLL.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
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
            var appService = new ApplicationService();
            ConsoleMenu cs = new ConsoleMenu();

            UserRole userRole = new UserRole();
            bool isRunning = false, StartMenu = false, ResumeMenu = false, VacancyMenu = false, AppMenu = false;

            userRole = AccessManager.Login();

            isRunning = true;
            StartMenu = true;
            Console.Clear();

            while(isRunning)
            {
                while(StartMenu)
                {
                    var options = cs.StartMenu();
                    switch (options)
                    {
                        case 1:
                            if(userRole != UserRole.Guest)
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

                while(ResumeMenu)
                {
                    var options = cs.ResumeConf();
                    switch(options)
                    {
                        case 1:
                            AccessManager.ExecuteIfAuthorized(userRole, "addresume", resumeService.AddResume); Console.ReadKey(); break;
                        case 2:
                            AccessManager.ExecuteIfAuthorized(userRole, "editresume", resumeService.EditResume); Console.ReadKey(); break;
                        case 3:
                            AccessManager.ExecuteIfAuthorized(userRole, "removeresume", resumeService.RemoveResume); ; Console.ReadKey(); break;
                        case 4:
                            AccessManager.ExecuteIfAuthorized(userRole, "getallresume", resumeService.GetAllResume); ; Console.ReadKey(); break;
                        case 5:
                            AccessManager.ExecuteIfAuthorized(userRole, "findresume", resumeService.FindResume); Console.ReadKey(); break;
                        case 6:
                            ResumeMenu = false; StartMenu = true; break;
                    }
                    Console.Clear();
                }

                while(VacancyMenu)
                {
                    var options = cs.VacancyConf();
                    switch(options)
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

                while(AppMenu)
                {
                    var options = cs.AppConf();
                    switch(options)
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
            }
        }
    }
}

