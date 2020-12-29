using Hangfire;
using Hangfire.Common;
using Hit.Services.Models.Models;
using Hit.Scheduler.Jobs;
using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Hangfire.Storage;
using System.Diagnostics;
using Hit.Services.Helpers.Classes.Classes;
using Hit.Services.MainLogic.Flows.SQLJobs;
using System.Threading;

namespace Hit.Services.Scheduler.Core
{
    public class HangfireBootstrapper
    {
        ILog logger;

     //   BackgroundJobServer _backgroundJobServer;

        public HangfireBootstrapper()
        {
            log4net.Config.XmlConfigurator.Configure();
            logger = log4net.LogManager.GetLogger(this.GetType());
        }

        /// <summary>
        /// Start HangFire using Recurring JobManager only
        /// </summary>
        /// <returns></returns>
        public bool StartRecurring()
        {
            logger.Info("Initialising Scheduler...");
            try
            {
                SchedulerModel sch = null; 
                string constring = null;

                //1. Read file /Json/ScheduledJobs.json, create table SqlParameters
                ReadJson(out sch, out constring,40);

                //2. check if scheduler should be active
                if (!sch.IsActive)
                {
                    logger.Warn("Scheduler is inactive !!");
                    return false;
                }
                logger.Info("SchedulerDB: " + constring.Substring(0, 35) + "...");


                //3. Configure Scheduler
                ConfigureScheduler(constring);

                //4. Remove existing reccuring jobs
                using (var connection = JobStorage.Current.GetConnection())
                {
                    foreach (var recurringJob in connection.GetRecurringJobs())
                    {
                        RecurringJob.RemoveIfExists(recurringJob.Id);
                    }
                }

                //4. activate jobs
                addJobs(sch);

                logger.Info("");
               
               //  _backgroundJobServer = new BackgroundJobServer();
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return false;
            }
            return true;
        }

        /// <summary>
        ///  1. Read file /Json/ScheduledJobs.json, 
        ///  2. set conString to static HangFireDB.ConString,  
        ///  3. create table SqlParameters
        /// </summary>
        /// <param name="sch"></param>
        /// <param name="constring"></param>
        public void ReadJson(out SchedulerModel sch, out string constring, int retry)
        {
            //1. read ScheduledJobs
            ConfigHelper reader = new ConfigHelper();
            sch = reader.ReadScheduledJobsFile(HitServices_Core.Properties.Settings.Default.ScheduledJobsFileIsEcrypted);
            constring = reader.ConnectionString(sch.SchedulerDBServer, sch.SchedulerDB, sch.SchedulerDBUser, sch.SchedulerDBPassword);

            //2.set conString to HangFireDB.ConString
            HangFireDB.ConString = constring;

            //2. create table SqlParameters
            SqlParametersFlows sqlparamflow = new SqlParametersFlows();
            sqlparamflow.CreateSqlParametersTable(retry);

        }

        /// <summary>
        /// Configure Scheduler (set DB and filters)
        /// </summary>
        public void ConfigureScheduler(string constring)
        {
            try
            {
                Hangfire.GlobalConfiguration.Configuration.UseSqlServerStorage(constring);
                // Hangfire.GlobalConfiguration.Configuration.UseFilter(new GetScheduledJobId());
                Hangfire.GlobalJobFilters.Filters.Add(new ProlongExpirationTimeAttribute());
            }
            catch (Exception ex)
            {
                logger.Info("Error connected to HangFire DB: "+ex.ToString());
            }
        }

        /// <summary>
        /// Activete Jobs using RecurringJobManager
        /// </summary>
        /// <param name="sch"></param>
        public void addJobs(SchedulerModel sch)
        {
            logger.Info("Adding Scheduled Jobs...");
            var manager = new RecurringJobManager();
            foreach (Hit.Services.Models.Models.SchedulerJob job in sch.Jobs)
            {
                JobsAssemblyModel model = (from u in ScheduledJob.JobsOriginList where u.ClassName == job.ClassName select u).FirstOrDefault();

                if (model != null)
                {
                    if (model.ClassName == job.ClassName)
                    {
                        if (model.JobsOrigin == 2)
                        {
                            logger.Info("Job: " + job.ToString());
                            if (!job.IsActive)
                                logger.Info("  Job is Inactive");

                            manager.AddOrUpdate(job.ID, () => InvokeJob(job.ClassName, job.Settings, job.ID, sch.Installation), job.Cron, TimeZoneInfo.Local);
                            logger.Info("  Job is ACTIVATED");
                        }
                        else
                        {
                            logger.Info("Job: " + job.ToString());
                            if (!job.IsActive)
                                logger.Info("  Job is Inactive");
                            else
                            {
                                Type LoadType = (from t in Assembly.GetExecutingAssembly().GetExportedTypes()
                                                 where !t.IsInterface && !t.IsAbstract
                                                 where t.IsSubclassOf(typeof(ScheduledJob)) && t.FullName == job.ClassName
                                                 //where typeof(IScheduledJob).IsAssignableFrom(t) && t.FullName == job.ClassName
                                                 select t).FirstOrDefault();

                                if (LoadType == null) logger.Error(">>>>>>> Class :" + job.ClassName + " not found !!!");
                                object instance = Activator.CreateInstance(LoadType);

                                MethodInfo method = LoadType.GetMethod("Execute");
                                var hfjob = new Job(LoadType, method, new object[3] { job.Settings, job.ID, sch.Installation });


                                manager.AddOrUpdate(job.ID, hfjob, job.Cron, TimeZoneInfo.Local);
                                logger.Info("  Job is ACTIVATED");
                            }
                        }
                    }
                }
                else
                {
                    logger.Info(" No Job Found");
                }

                //logger.Info("Job: " + job.ToString());
                //if (!job.IsActive)
                //    logger.Info("  Job is Inactive");
                //else
                //{
                //    Type LoadType = (from t in Assembly.GetExecutingAssembly().GetExportedTypes()
                //                     where !t.IsInterface && !t.IsAbstract
                //                     where t.IsSubclassOf(typeof(ScheduledJob)) && t.FullName == job.ClassName
                //                     //where typeof(IScheduledJob).IsAssignableFrom(t) && t.FullName == job.ClassName
                //                     select t).FirstOrDefault();

                //    if (LoadType == null) logger.Error(">>>>>>> Class :" + job.ClassName + " not found !!!");
                //    object instance = Activator.CreateInstance(LoadType);

                //    MethodInfo method = LoadType.GetMethod("Execute");
                //    var hfjob = new Job(LoadType, method, new object[3] { job.Settings, job.ID, sch.Installation });

                //    manager.AddOrUpdate(job.ID, hfjob, job.Cron, TimeZoneInfo.Local);
                //    logger.Info("  Job is ACTIVATED");
                //}

            }
        }


        public void InvokeJob(string ClassName, string settingsfile, string settingsId, string installation)
        {
            JobsAssemblyModel model = (from u in ScheduledJob.JobsOriginList where u.ClassName == ClassName select u).FirstOrDefault();

            Type LoadType = (from t in model.Assembly.GetExportedTypes()
                             where !t.IsInterface && !t.IsAbstract
                             where t.IsSubclassOf(model.Type) && t.FullName == ClassName
                             select t).FirstOrDefault();

            if (LoadType == null)
            {
                logger.Error(">>>>>>> Class :" + ClassName + " not found !!!");
            }

            object[] obj = { settingsfile, settingsId, installation };
            object instance = Activator.CreateInstance(LoadType);
            var method = LoadType.GetMethod("Execute", new[] { typeof(string), typeof(string), typeof(string) });

            var res = method.Invoke(instance, obj);
        }

        ///// <summary>
        ///// Read file /Json/ScheduledJobs.json and Decrypt SchedulerDB
        ///// </summary>
        ///// <returns>SchedulerModel</returns>
        //public  SchedulerModel readScheduledJobsfile()
        //{
        //    //1. Read file /Json/ScheduledJobs.json
        //    SchedulerModel sch;
        //    using (StreamReader r = new StreamReader(Properties.Settings.Default.ScheduledJobs))
        //    {
        //        string json = r.ReadToEnd();
        //        sch = Newtonsoft.Json.JsonConvert.DeserializeObject<SchedulerModel>(json);
        //    }

        //    foreach (var job in sch.Jobs)
        //    {
        //        if (job.ID == null || job.ID == "")
        //        {
        //            job.ID = job.ClassName;
        //        }
        //    }

        //    //2. decrypt SchedulerDB 
        //    EncryptionHelper Ecryptor = new EncryptionHelper();
        //    sch.SchedulerDB = Ecryptor.Decrypt(sch.SchedulerDB);

        //    return sch;
        //}



        //public  string GetJsonPath()
        //{
        //    string path = new Uri(Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase)).LocalPath + @"\Json\ScheduledJobs.json";
        //    FileInfo file = new FileInfo(path);
        //    if (!file.Exists)
        //    {
        //        path = file.Directory.Parent.Parent.Parent.FullName + @"\Json\ScheduledJobs.json";
        //        //System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(basepath);
        //        //basepath = dir.Parent.Parent.FullName + "\\WebApi\\";
        //    }
        //    return path;
        //}

        //public void init()
        //{

        //}
    }
}
