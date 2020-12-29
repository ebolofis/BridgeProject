using Hangfire;
using Hangfire.Common;
using Hangfire.Dashboard;
using Hangfire.Storage;
using Hit.Scheduler.Jobs;
using Hit.Services.Helpers.Classes.Classes;
using Hit.Services.MainLogic.Flows.SQLJobs;
using Hit.Services.Models.Models;
using Hit.Services.Scheduler.Core;
using Hit.Services.WebApi.Filters;

using log4net;
using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.Hosting;
using System.Web.Http;
using System.Web.Mvc;

namespace Hit.Services.WebApi
{
    public class Startup
    {
        ILog logger;

        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public void Configuration(IAppBuilder appBuilder)
        {
            //1. Configure Web API for self-host. 
            HttpConfiguration config = new HttpConfiguration();

            //2. Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
             name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            appBuilder.UseWebApi(config);

            AreaRegistration.RegisterAllAreas();
            
            

            //3. config.Filters and Handles
            config.Filters.Add(new ExceptionAttribute());
            config.Filters.Add(new SettingsAttribute());
            config.MessageHandlers.Add(new Hit.Services.WebApi.Modules.TraceHandler());


            log4net.Config.XmlConfigurator.Configure();
            logger = log4net.LogManager.GetLogger(this.GetType());
            logger.Info("");
            logger.Info("");
            logger.Info("*****************************************");
            logger.Info("*                                       *");
            logger.Info("*          Hit Services Started         *");
            logger.Info("*                                       *");
            logger.Info("*****************************************");
            logger.Info("");
            System.Diagnostics.Debug.WriteLine("Application Started");
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            logger.Info("version: " + fvi.FileVersion);
            logger.Info("");
            logger.Info("Ready to serve requests!!");
            logger.Info("");
            logger.Info("");

            ConfigHelper reader = new ConfigHelper();
            SchedulerModel sch = reader.ReadScheduledJobsFile(FilesState.GetEncryptFilesStatus().ScheduledJobsFileIsEcrypted);
            string constring = reader.ConnectionString(sch.SchedulerDBServer, sch.SchedulerDB, sch.SchedulerDBUser, sch.SchedulerDBPassword);
            HangFireDB.ConString = constring;
            //create Table SqlParameters to DB
            SqlParametersFlows sqlparamflow = new SqlParametersFlows();
            sqlparamflow.CreateSqlParametersTable();

            if (sch.IsActive && sch.Dashboard)
            {
                logger.Info("Enabling Scheduler's Dashboard...");
                Hangfire.GlobalConfiguration.Configuration.UseSqlServerStorage(constring);
               // appBuilder.UseHangfireServer();
                appBuilder.UseHangfireDashboard("/Scheduler");
            }
            else
                logger.Info("Scheduler's Dashboard is disabled.");


            //4. Initialize Scheduler (see also IIS config  https://www.red-gate.com/simple-talk/blogs/speeding-up-your-application-with-the-iis-auto-start-feature/)
            //  StartScheduler(appBuilder);


            //   //1. find all types in an assembly that implement IClass1 interface
            //   Type iLoadType = (from t in Assembly.LoadFrom(@"C:\Users\gzax\Documents\Visual Studio 2017\Projects\HitServices\HitServices_WebApi\bin\HitServicesML.dll").GetExportedTypes()
            //                  where t.IsInterface
            //                  where //typeof(Hit.Services.MainLogic.Jobs.IScheduledJob).IsAssignableFrom(t)   && 
            //                 t.FullName == "Hit.Services.MainLogic.Jobs.IScheduledJob" //<--<<< if you want to get only one
            //                  select t).FirstOrDefault();

            ////1. find all types in an assembly that implement IClass1 interface
            //Type LoadType = (from t in Assembly.LoadFrom(@"C:\Users\gzax\Documents\Visual Studio 2017\Projects\HitServices\HitServices_WebApi\bin\HitServicesML.dll").GetExportedTypes()
            //                 where !t.IsInterface && !t.IsAbstract
            //                 where iLoadType.IsAssignableFrom(t) &&
            //                t.FullName == "Hit.Services.MainLogic.Jobs.TestJob" //<--<<< if you want to get only one
            //                 select t).FirstOrDefault();


            //object instance = Activator.CreateInstance(LoadType);
            //MethodInfo method = LoadType.GetMethod("Execute");

            //var job = new Job(LoadType, method, new object[1] { new List<string>() { "qqqq", "cccc" } });
            //var manager = new RecurringJobManager();

            //manager.AddOrUpdate("TEST1", job, "* * * * *");

        }

        ///// <summary>
        ///// Initialise Scheduler
        ///// </summary>
        ///// <param name="appBuilder"></param>
        //private void StartScheduler(IAppBuilder appBuilder)
        //{
        //    logger.Info("Initialising Scheduler...");

        //    //1. Read file /Json/ScheduledJobs.json
        //    SchedulerModel sch = readScheduledJobsfile();
        //    if (!sch.IsActive)
        //    {
        //        logger.Warn("Scheduler is inactive !!");
        //        return;
        //    }
        //    logger.Info("SchedulerDB: "+ sch.SchedulerDB.Substring(0,35)+"...");

        //    //2. Configure Scheduler
        //    Hangfire.GlobalConfiguration.Configuration.UseSqlServerStorage(sch.SchedulerDB);
        //    Hangfire.GlobalConfiguration.Configuration.UseFilter(new GetScheduledJobId());
        //   if (sch.Dashboard)
        //    {
        //        logger.Info("Enabling Scheduler's Dashboard...");
        //        appBuilder.UseHangfireDashboard();
        //    }
        //   else
        //        logger.Info("Scheduler's Dashboard is disabled.");

        //    appBuilder.UseHangfireServer();
        //    using (var connection = JobStorage.Current.GetConnection())
        //    {
        //        foreach (var recurringJob in connection.GetRecurringJobs())
        //        {
        //            RecurringJob.RemoveIfExists(recurringJob.Id);
        //        }
        //    }

        //    //3. activate jobs
        //    addJobs(sch);

        //    logger.Info("");
        //}

        ///// <summary>
        ///// Read file /Json/ScheduledJobs.json and Decrypt SchedulerDB
        ///// </summary>
        ///// <returns>SchedulerModel</returns>
        //private SchedulerModel readScheduledJobsfile()
        //{
        //    //1. Read file /Json/ScheduledJobs.json
        //    SchedulerModel sch;
        //    using (StreamReader r = new StreamReader(GetJsonPath()))
        //    {
        //        string json = r.ReadToEnd();
        //        sch=Newtonsoft.Json.JsonConvert.DeserializeObject<SchedulerModel>(json);
        //    }

        //    //2. decrypt SchedulerDB 
        //    EncryptionHelper Ecryptor = new EncryptionHelper();
        //    sch.SchedulerDB = Ecryptor.Decrypt(sch.SchedulerDB);

        //    return sch;
        //}

        ///// <summary>
        ///// Activete Jobs
        ///// </summary>
        ///// <param name="sch"></param>
        //public void addJobs(SchedulerModel sch)
        //{
        //    logger.Info("Adding Scheduled Jobs...");
        //    foreach (Hit.Services.Models.Models.SchedulerJob job in sch.Jobs)
        //    {
        //        logger.Info("Job: " + job.ToString());
        //        if (!job.IsActive)
        //            logger.Info("  Job is Inactive");
        //        else
        //        {
        //            Type LoadType = (from t in Assembly.GetExecutingAssembly().GetExportedTypes()
        //                                 where !t.IsInterface && !t.IsAbstract
        //                                 where typeof(IScheduledJob).IsAssignableFrom(t) && t.FullName== job.ClassName select t).FirstOrDefault();

        //            object instance = Activator.CreateInstance(LoadType);
                   
        //            MethodInfo method = LoadType.GetMethod("Execute");
        //            var hfjob = new Job(LoadType, method, new object[1] { job.Params });
        //            var manager = new RecurringJobManager();

        //            manager.AddOrUpdate(job.ClassName,hfjob, job.Cron);
        //            logger.Info("  Job is Activated");
        //        }

        //    }
        }

    //public static string GetJsonPath()
    //    {
    //        string path = new Uri(Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase)).LocalPath + @"\Json\ScheduledJobs.json";
    //        FileInfo file = new FileInfo(path);
    //        if (!file.Exists)
    //        {
    //            path = AppDomain.CurrentDomain.BaseDirectory + @"\Json\ScheduledJobs.json";
    //            //System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(basepath);
    //            //basepath = dir.Parent.Parent.FullName + "\\WebApi\\";
    //        }
    //        return path;
    //    }

    //}

    //public class ApplicationPreload : System.Web.Hosting.IProcessHostPreloadClient
    //{
    //    public void Preload(string[] parameters)
    //    {
    //        HangfireBootstrapper.Instance.Start();
    //    }
    //}

    //public class MyAuthorizationFilter : IDashboardAuthorizationFilter
    //{
    //    public bool Authorize(DashboardContext context)
    //    {
    //        // In case you need an OWIN context, use the next line, `OwinContext` class
    //        // is the part of the `Microsoft.Owin` package.
    //        var owinContext = new OwinContext(context.GetOwinEnvironment());

    //        // Allow all authenticated users to see the Dashboard (potentially dangerous).
    //        return owinContext.Authentication.User.Identity.IsAuthenticated;
    //    }
    //}

}