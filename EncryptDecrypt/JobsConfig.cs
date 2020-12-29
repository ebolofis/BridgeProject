using Hit.Services.Helpers.Classes.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hit.Services.Models.Models;
using System.Windows.Forms;
using System.Reflection;
using Hit.Scheduler.Jobs;
using System.IO;
using log4net;
using Hit.Services.Scheduler.Core;
using Hit.Services.Core;

namespace Hit.Services.EncryptDecrypt
{
   public class JobsConfig
    {
        ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        FilesEncryptStatus encrStatus;
        ConfigHelper config;
        FileHelpers fh;
        public SchedulerModel Scheduler;
        public JobsConfig()
        {
             config = new ConfigHelper();
             fh = new FileHelpers();
            string path = fh.getBasePath();
           
            
            try
            {
                encrStatus = FilesState.GetEncryptFilesStatus();
                Scheduler = config.ReadScheduledJobsFile(encrStatus.ScheduledJobsFileIsEcrypted);
                //if (Scheduler.Jobs.Count == 0) Scheduler.Jobs.Add(new SchedulerJob() { ID="Test",Description="Test Job"});
            }
            catch(Exception ex)
            {
                log.Error(ex.ToString());
                MessageBox.Show(ex.Message, "Reading Jobs Config file");
            }
            
        }

        /// <summary>
        /// Return the SettingsModel for a specific Settingsfile
        /// </summary>
        /// <param name="SettingsModel">SettingsModel</param>
        /// <returns></returns>
        public SettingsModel GetSettingsModel(string Settingsfile)
        {
            return config.ReadSettingsFile(Settingsfile, encrStatus.SettingsFilesAreEncrypted);
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
            catch(Exception ex)
            {
                log.Error(ex.ToString());
                MessageBox.Show(ex.Message, "Error Reading available jobs");
            }
            return new ExtAssemblyModel();
        }

        /// <summary>
        /// Get settings files
        /// </summary>
        /// <returns></returns>
        public List<string> GetSettingsfiles()
        {
            try
            {
                string path = fh.GetSettingsPath();
                List<string> filePaths = Directory.GetFiles(path).ToList();
                for(int i=0;i< filePaths.Count();i++)
                {
                    filePaths[i] = Path.GetFileName(filePaths[i]);
                }
                filePaths.Sort();
                return filePaths;
            }
            catch (Exception ex)
            {
                log.Error(ex.ToString());
                MessageBox.Show(ex.Message, "Error getting Settings files");
            }
            return new List<string>();
        }


        public void SaveSettings(SchedulerModel Scheduler)
        {
         
            config.WriteScheduledJobsFile(Scheduler, encrStatus.ScheduledJobsFileIsEcrypted);
        }

    }
}
