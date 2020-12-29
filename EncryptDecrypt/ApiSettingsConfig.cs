using Hit.Services.Helpers.Classes.Classes;
using Hit.Services.Models.Models;
using Hit.Services.Scheduler.Core;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hit.Services.EncryptDecrypt
{
   public class ApiSettingsConfig
    {
        ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// list that relates Controllers with settings files.
        /// </summary>
        public List<WebApiSettingsModel> ApiSettings = null;

        FilesEncryptStatus encrStatus;
        ConfigHelper config;

        public ApiSettingsConfig()
        {
            encrStatus = FilesState.GetEncryptFilesStatus();
            config = new ConfigHelper();
            try
            {
                ApiSettings = config.ReadWebApiSettings(encrStatus.WebApiSettingsIsEcrypted);
            }
            catch (Exception ex)
            {
                log.Error(ex.ToString());
                MessageBox.Show(ex.Message, "Error Reading Api Settings file");
            }
        }


        public List<string> GetSettingsfiles()
        {
           return config.GetSettingsFiles().ToList();
        }


        public List<string> GetControllers()
        {

            FileHelpers fh = new FileHelpers();
            string path = fh.GetHitServicesWAPath();
            try
            {
                Assembly asm = Assembly.LoadFrom(path); 
                List<string> LoadTypes = (from t in asm.GetExportedTypes()
                                          where !t.IsInterface && !t.IsAbstract
                                          where t.IsSubclassOf(asm.GetType("Hit.WebApi.Controllers.HitController")) //&& t.FullName == job.ClassName
                                                                                     //where typeof(IScheduledJob).IsAssignableFrom(t) && t.FullName == job.ClassName
                                          select t.FullName).ToList();
                LoadTypes.Sort();
                return LoadTypes;
            }
            catch (Exception ex)
            {
                log.Error(ex.ToString());
                MessageBox.Show(ex.Message, "Error Reading available Controllers");
            }
            return new List<string>();
        }


        public void SaveUsers()
        {
            try
            {
                config.WriteWebApiSettings(ApiSettings, encrStatus.WebApiSettingsIsEcrypted);
                MessageBox.Show("Api Settings are saved!", "SAVE");
            }
            catch (Exception ex)
            {
                log.Error(ex.ToString());
                MessageBox.Show(ex.Message,  "ERROR");
            }
        }

    }
}
