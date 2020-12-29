using Hit.Services.Scheduler.Core;
using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using Hit.Services.Helpers.Classes.Classes;
using Hangfire;
using System.Threading;
using Hit.Services.Scheduler.ServiceInstall;
using Hit.Services.Models;

namespace Hit.Services.Scheduler
{
    class Program
    {
        static string serviceName = "HitServices_Scheduler";  

        static ILog logger;    // = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
        static string path = System.Reflection.Assembly.GetEntryAssembly().Location;
        static string param = "";

        static void Main(string[] args)
        {
            log4net.GlobalContext.Properties["LogName"] = serviceName + ".log"; // <----- Set Log Files -----<<<
            log4net.Config.XmlConfigurator.Configure();
            logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

            //read command line parameter
            if (args.Length >= 1)
            {
                param = args[0];
                logger.Info("Application started with param: " + param);
            }

            //Config Automapper
            AutoMapperConfig.RegisterMappings();

            Initialize();
        }

        /// <summary>
        /// Initialize Dll and promt user to Install/Unistall or Run the Service
        /// </summary>
        static void Initialize()
        {
            string exName = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
            string sPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            if (sPath[sPath.Length - 1].ToString() != "\\")
                sPath = sPath + "\\";

            if (!Environment.UserInteractive)
            {
                logger.Info("Application opened as Windows Service");
                using (var service = new Service(exName, serviceName))
                    ServiceBase.Run(service);
            }
            else
            {
                //// running as console app
                logger.Info("Application opened as Console Application");
                ServiceController sc = new ServiceController(serviceName);

                bool isServicePresent = false;
                try { var test = sc.Status; isServicePresent = true; }
                catch { }

                ConsoleKeyInfo key;
                if (isServicePresent)
                {

                    Console.WriteLine("Application is installed as Windows Service.");
                    Console.WriteLine(sc.Status != ServiceControllerStatus.Running
                                            ? "Service is stopped\n\n[S] to start Service " : "Service is running!\n\n[S] to stop Service ");
                    Console.WriteLine("[U] to unistall as Windows Service ");
                    Console.WriteLine("ANY other key to exit");

                    if (param == "")
                        key = Console.ReadKey(true);
                    else
                        key = new ConsoleKeyInfo(param[0], new ConsoleKey(), false, false, false);

                    Console.Clear();
                    switch (key.KeyChar)
                    {
                        case 'S':
                        case 's':
                            if (sc.Status == ServiceControllerStatus.Running && sc.CanStop)
                            {
                                sc.Stop();
                                sc.WaitForStatus(ServiceControllerStatus.Stopped);
                                Console.WriteLine("Windows Service stopped!\n\nPress any key to exit...");
                                if (param == "") Console.ReadKey(true);
                            }
                            else
                                if (sc.Status == ServiceControllerStatus.Stopped || sc.Status == ServiceControllerStatus.Paused)
                            {
                                sc.Start();
                                sc.WaitForStatus(ServiceControllerStatus.Running);
                                Console.WriteLine("Windows Service is up and running!\n\nPress any key to exit...");
                                if (param == "") Console.ReadKey(true);
                            }


                            break;
                        case 'U':
                        case 'u':
                            try
                            {
                                if (sc.CanStop)
                                {
                                    sc.Stop();
                                    sc.WaitForStatus(ServiceControllerStatus.Stopped);
                                }
                            }
                            catch
                            { }

                            Hit.Services.Scheduler.ServiceInstall.ServiceInstaller cu = new Hit.Services.Scheduler.ServiceInstall.ServiceInstaller();
                            cu.UninstallService(serviceName);
                            logger.Info("Application uninstalled as Windows Service");
                            Console.WriteLine("Application uninstalled as Windows Service succesfully!\n\nPress any key to exit...");
                            if (param == "") Console.ReadKey(true);
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Application is not installed as Windows Service.");
                    Console.WriteLine("");
                    Console.WriteLine("[I] to install as Windows Service");
                   // Console.WriteLine("[R] to run as Console Application ");

                    if (param == "")
                        key = Console.ReadKey(true);
                    else
                        key = new ConsoleKeyInfo(param[0], new ConsoleKey(), false, false, false);

                    Console.Clear();
                    switch (key.KeyChar)
                    {
                        case 'I':
                        case 'i':
                            logger.Info("Application installing as Windows Service");
                            Hit.Services.Scheduler.ServiceInstall.ServiceInstaller ci = new Hit.Services.Scheduler.ServiceInstall.ServiceInstaller();
                            ci.InstallService(Environment.CurrentDirectory + @"\" + exName + ".exe", serviceName, serviceName);
                            
                            Console.WriteLine("Application installed as Windows Service succesfully and is up and runnning!\n\nPress any key to exit...");
                            if (param == "") Console.ReadKey(true);
                           
                           
                            break;
                        case 'R':
                        case 'r':
                            try
                            {
                                
                            }
                            finally
                            {

                            }
                            return;
                            break;
                        default:
                            break;
                    }
                }
            }
        }

       
    }

    
}
