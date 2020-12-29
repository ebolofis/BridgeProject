using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.ServiceProcess;
using Hit.Services.Scheduler.Core;
using Hangfire;
using System.Diagnostics;
using System.Security.Cryptography;
using System.IO;
using System.Threading;
using Hit.Services.Helpers.Classes.Classes;
using Hit.Services.Models.Models;
using System.Reflection;
using Hit.Scheduler.Jobs;

namespace Hit.Services.Scheduler.ServiceInstall
{
    public class Service : ServiceBase
    {
        /// <summary>
        /// MD5 hash for ScheduledJobs Config file (Properties.Settings.Default.ScheduledJobs)
        /// </summary>
        static string ConfigfileMd5;

        /// <summary>
        /// Logger
        /// </summary>
        ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        BackgroundJobServer server;
        /// <summary>
        /// Name for Application
        /// </summary>
        string ExecName;
        /// <summary>
        /// Name for application to create new Service Instance
        /// </summary>
        string SERVICENAME;

        /// <summary>
        /// Make Service Install and Uninstall
        /// </summary>
        /// <param name="ExecName"></param>
        /// <param name="SERVICENAME"></param>
        public Service(string ExecName, string SERVICENAME)
        {
            string exName = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
            if (!string.IsNullOrEmpty(exName))
            {
                this.ExecName = ExecName;
                this.SERVICENAME = SERVICENAME;
            }
            ServiceName = SERVICENAME;
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                FillJobsList();
                Startlogger(args);
                ConfigfileMd5 = GetConfigFileMd5(); 
                StartHangFire();
            }
            catch(Exception ex)
            {
                log.Error(ex.ToString());
            }
           
        }

        protected override void OnStop()
        {
            Stop();
            StopHangFire();
        }

        /// <summary>
        /// init Hangfire
        /// </summary>
        public void StartHangFire()
        {
            Thread.Sleep(5000);
            HangfireBootstrapper hf = new HangfireBootstrapper();
            HangfireConfigUpdater();
            if (!hf.StartRecurring()) return;
            server = new BackgroundJobServer();
           
           
        }

        public void StopHangFire(){
            if (server != null)
            {
                //server.Stop();
                server.Dispose();
            }
        }


        /// <summary>
        /// Service Start
        /// </summary>
        /// <param name="args"></param>
        public void Startlogger(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure();
            log = log4net.LogManager.GetLogger(this.GetType());
            log.Info("");
            log.Info("");
            log.Info("*****************************************");
            log.Info("*                                       *");
            log.Info("*          Hit Scheduler Started        *");
            log.Info("*                                       *");
            log.Info("*****************************************");
            log.Info("");
            System.Diagnostics.Debug.WriteLine("Hit Scheduler Started");
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            log.Info("version: " + fvi.FileVersion);
            log.Info("");
            log.Info("Application run as windows service...");
            log.Info("");
        }

        /// <summary>
        /// Service Stops
        /// </summary>
        public void Stop()
        {
            //conn.DisconnectSocket();

            if (!Environment.UserInteractive)
                log.Info("Application stopped as Windows Service");
            else
                log.Info("Application closed as Console Application");
        }


        /// <summary>
        /// Checks periodically if Config File has been change and reload it.
        /// </summary>
        public void HangfireConfigUpdater()
        {
            Thread th = new Thread(() => HangfireConfigCheck());
            th.IsBackground = true;
            th.Start();
        }
        public void HangfireConfigCheck()
        {
            while (true)
            {
                Thread.Sleep(30000);
                try
                {
                    
                    string newMd5 = GetConfigFileMd5();

                    //config file has been changed, Read it again...
                    if (newMd5 != ConfigfileMd5 && newMd5 != "")
                    {
                        log.Info("Config File ScheduledJobs has been changed.");
                        ConfigfileMd5 = newMd5;
                        StopHangFire();
                        StartHangFire();
                        return;
                    }
                }catch(Exception ex)
                {
                    log.Error(ex.ToString());
                }
              
            }
           
        }

        public string GetConfigFileMd5()
        {
            using (var md5 = MD5.Create())
            {
                ConfigHelper ch = new ConfigHelper();
                string path = ch.GetScheduledJobFile(HitServices_Core.Properties.Settings.Default.ScheduledJobsFileIsEcrypted);

                if (path == "JSON_PATH_NOT_FOUND") return "" ;
                
                using (var stream = File.OpenRead(path))
                {
                    var hash = md5.ComputeHash(stream);
                    return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                }

                
            }
        }

        /// <summary>
        /// Fills Select Basic class combo Box
        /// </summary>
        public void FillJobsList()
        {
            /*Getting Local Jobs*/
            ExtAssemblyModel localClasses = GetJobs();
            foreach (string item in localClasses.ClassNames)
            {
                JobsAssemblyModel model = new JobsAssemblyModel();
                model.JobsOrigin = 1;
                model.ClassName = item;
                model.Assembly = localClasses.Assembly;
                model.Type = localClasses.Type;
                ScheduledJob.JobsOriginList.Add(model);
            }

            /*Getting Controllers Jobs*/
            ExtAssemblyModel LoadTypes = GetControllersName();
            if (LoadTypes.Assembly != null)
            {
                foreach (string item in LoadTypes.ClassNames)
                {
                    JobsAssemblyModel model = new JobsAssemblyModel();
                    model.JobsOrigin = 3;
                    model.ClassName = item;
                    model.Assembly = LoadTypes.Assembly;
                    model.Type = LoadTypes.Type;
                    ScheduledJob.JobsOriginList.Add(model);
                }
            }

            /*Getting External Jobs*/
            ExtAssemblyModel LoadExtTypes = GetExternalNames();
            if (LoadExtTypes.Assembly != null)
            {
                foreach (string item in LoadExtTypes.ClassNames)
                {
                    JobsAssemblyModel model = new JobsAssemblyModel();
                    model.JobsOrigin = 2;
                    model.ClassName = item;
                    model.Assembly = LoadExtTypes.Assembly;
                    model.Type = LoadExtTypes.Type;
                    ScheduledJob.JobsOriginList.Add(model);
                }
            }
        }


        /// <summary>
        /// Get Jobs full names
        /// </summary>
        /// <returns></returns>
        public ExtAssemblyModel GetJobs()
        {
            try
            {
                ExtAssemblyModel Model = new ExtAssemblyModel();
                Assembly asm = typeof(Hit.Services.Scheduler.Core.HangfireBootstrapper).Assembly;
                Model.ClassNames = (from t in asm.GetExportedTypes()
                                    where !t.IsInterface && !t.IsAbstract
                                    where t.IsSubclassOf(typeof(ScheduledJob)) //&& t.FullName == job.ClassName
                                                                               //where typeof(IScheduledJob).IsAssignableFrom(t) && t.FullName == job.ClassName
                                    select t.FullName).ToList();
                Model.ClassNames.Sort();
                Model.Assembly = asm;
                Model.Type = typeof(ScheduledJob);
                return Model;
            }
            catch (Exception ex)
            {
                log.Error(ex.ToString());
            }
            return new ExtAssemblyModel();
        }

        /// <summary>
        /// Returns Controllers name
        /// </summary>
        /// <returns></returns>
        private ExtAssemblyModel GetControllersName()
        {
            ExtAssemblyModel Model = new ExtAssemblyModel();
            FileHelpers fh = new FileHelpers();
            string path = fh.GetHitServicesWAPath();
            try
            {
                Assembly asm = Assembly.LoadFrom(path);
                Model.ClassNames = (from t in asm.GetExportedTypes()
                                    where !t.IsInterface && !t.IsAbstract
                                    where t.IsSubclassOf(asm.GetType("Hit.WebApi.Controllers.HitController")) 
                                                                                                             
                                    select t.FullName).ToList();
                Model.ClassNames.Sort();
                Model.Assembly = asm;
                Model.Type = typeof(ScheduledJob);
                return Model;
            }
            catch (Exception ex)
            {
                log.Error(ex.ToString());
            }
            return new ExtAssemblyModel();
        }

        /// <summary>
        /// Returns External Names
        /// </summary>
        /// <returns></returns>
        private ExtAssemblyModel GetExternalNames()
        {
            try
            {
                ExtAssemblyModel Model = new ExtAssemblyModel();
                FileHelpers fh = new FileHelpers();
                List<string> path = fh.GetExternalJobsPaths();

                string ipath = fh.GetExtPath();
                Assembly extAsm = Assembly.LoadFrom(ipath);
                Type iType = extAsm.GetType("Hit.Scheduler.Jobs.ScheduledJob");

                foreach (string p in path)
                {
                    Assembly asm = Assembly.LoadFile(p);
                    List<string> LoadTypes = new List<string>();

                    LoadTypes = (from t in asm.GetExportedTypes()
                                 where !t.IsInterface && !t.IsAbstract
                                 where t.IsSubclassOf(iType)
                                 select t.FullName).ToList();

                    if (LoadTypes.Count > 0)
                    {
                        Model.ClassNames = LoadTypes;
                        Model.Assembly = asm;
                        Model.Type = iType;
                    }
                }
                Model.ClassNames.Sort();
                return Model;
            }
            catch (Exception ex)
            {
                log.Error(ex.ToString());
            }
            return new ExtAssemblyModel();
        }

    }

}
