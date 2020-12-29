using Hangfire;
using Hit.Scheduler.Jobs;
using Hit.Services.Helpers.Classes.Classes;
using Hit.Services.Models;
using Hit.Services.Models.Models;
using log4net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Hit.Services.Scheduler.Core
{
    public class FireAndForget
    {
        ILog logger;
        HangfireBootstrapper bootstr;
        BackgroundJobServer bgs;
        public FireAndForget()
        {
            log4net.Config.XmlConfigurator.Configure();
            logger = log4net.LogManager.GetLogger(this.GetType());

            bootstr = new HangfireBootstrapper();
            SchedulerModel sch = null;
            string constring = null;

            //1. Read file /Json/ScheduledJobs.json
            bootstr.ReadJson(out sch, out constring, 1);
            bootstr.ConfigureScheduler(constring);
            logger.Info("FireAndForget Started.");
        }

        /// <summary>
        /// fire-and-forget a job. Return the error
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        public string Execute(Hit.Services.Models.Models.SchedulerJob job, string installation)
        {
            if (bgs == null) bgs = new BackgroundJobServer();

            foreach (JobsAssemblyModel model in ScheduledJob.JobsOriginList)
            {
                if (model.ClassName == job.ClassName)
                {
                    if (model.JobsOrigin == 2)
                    {
                        //Type LoadType = (from t in model.Assembly.GetExportedTypes()
                        //                 where !t.IsInterface && !t.IsAbstract
                        //                 where t.IsSubclassOf(model.Type) && t.FullName == job.ClassName
                        //                 select t).FirstOrDefault();

                        //if (LoadType == null)
                        //{
                        //    logger.Error(">>>>>>> Class :" + job.ClassName + " not found !!!");
                        //}

                        //ScheduledJob instance = (ScheduledJob)Activator.CreateInstance(LoadType);
                        //object[] obj = { job.Settings, job.ID, installation };
                        //object instance = Activator.CreateInstance(LoadType);
                        //var method = LoadType.GetMethod("Execute", new[] { typeof(string), typeof(string), typeof(string) });

                        logger.Info("Executing (Fire-and-Forget) Job: " + job.ToString() + "...");
                        try
                        {
                            try
                            {
                                AutoMapperConfig.RegisterMappings();
                            }
                            catch { }

                            //var res = method.Invoke(instance, obj);
                            //BackgroundJob.Enqueue(() => instance.Execute(job.Settings, job.ID, installation));
                            BackgroundJob.Enqueue(() => InvokeJob(job.ClassName, job.Settings, job.ID, installation));
                            return "";
                        }
                        catch (Exception ex)
                        {
                            logger.Error("Error while executing (Fire-and-Forget) Job: " + job.ID + ": " + ex.ToString());
                            return ex.Message;
                        }
                    }
                    else
                    {
                        Type LoadType = (from t in model.Assembly.GetExportedTypes()
                                         where !t.IsInterface && !t.IsAbstract
                                         where t.IsSubclassOf(typeof(ScheduledJob)) && t.FullName == job.ClassName
                                         select t).FirstOrDefault();

                        if (LoadType == null)
                        {
                            logger.Error(">>>>>>> Class :" + job.ClassName + " not found !!!");
                        }

                        ScheduledJob instance = (ScheduledJob)Activator.CreateInstance(LoadType);

                        logger.Info("Executing (Fire-and-Forget) Job: " + job.ToString() + "...");
                        try
                        {
                            try
                            {
                                AutoMapperConfig.RegisterMappings();
                            }
                            catch { }

                            var settings = new JsonSerializerSettings
                            {
                                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                            };

                            string strJson = JsonConvert.SerializeObject(logger, Formatting.Indented, settings);

                            BackgroundJob.Enqueue(() => instance.Execute(job.Settings, job.ID, installation));
                            return "";
                        }
                        catch (Exception ex)
                        {
                            logger.Error("Error while executing (Fire-and-Forget) Job: " + job.ID + ": " + ex.ToString());
                            return ex.Message;
                        }
                    }
                }
            }

            //Type LoadType = (from t in Assembly.GetExecutingAssembly().GetExportedTypes()
            //                 where !t.IsInterface && !t.IsAbstract
            //                 where t.IsSubclassOf(typeof(ScheduledJob)) && t.FullName == job.ClassName
            //                 //where typeof(IScheduledJob).IsAssignableFrom(t) && t.FullName == job.ClassName
            //                 select t).FirstOrDefault();

            return "";
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

        ~FireAndForget()
        {
            if (bgs != null)
            {
                bgs.Dispose();
                bgs = null;
                logger.Info("FireAndForget Disposed.");
            }
        }


    }

}
