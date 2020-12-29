using Hit.Scheduler.Jobs;
using Hit.Services.Core;
using Hit.Services.Helpers.Classes.Classes;
using Hit.Services.Models.Models;
using Hit.Services.Scheduler.Core;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Hit.WebApi.Controllers
{
    public class HitController : ApiController
    {
        /// <summary>
        /// Log4net logger
        /// </summary>
        protected ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected LoginModel UserModel = null;


        /// <summary>
        /// Extract Username and Password from Request. GroupAuthorizationAttribute is required.
        /// </summary>
        protected LoginModel GetUser()
        {
            //Extract username and password from Request. SEE: Hit.Services.WebApi.Filters.GroupAuthorizationAttribute
            object _loginModel = null;

            if (Request.Properties.TryGetValue("LOGINMODEL", out _loginModel))
            {
                try
                {
                   if(_loginModel!=null) UserModel = (LoginModel)_loginModel;
                }
                catch (Exception ex)
                {
                    logger.Error("Invalid UserModel: " + ex.ToString());
                }
            }
            return UserModel;
        }

      


        /// <summary>
        /// Return the settings model 
        /// </summary>
        /// <param name="settingsFile">the settings file</param>
        /// <param name="throwEception">true: if SettingsModel==null then throw exception</param>
        /// <returns></returns>
        protected SettingsModel GetSettings(string settingsFile="", bool throwEception = true)
        {
            SettingsModel settings = null;

            if (settingsFile == "")
            {

                // Return the settings model based on configuration (Api config)
                object _settingsModel = null;
                if (Request.Properties.TryGetValue("SETTINGSMODEL", out _settingsModel))
                {
                    try
                    {
                        if (_settingsModel != null) settings = (SettingsModel)_settingsModel;
                        if (_settingsModel == null && throwEception) throw new Exception("_settingsModel is null");
                        logger.Info("Settings file " + (settings.SettingsFile ?? "<null>") + " is selected based on configuration");
                        settings.CsvDelimenter = ScheduledJob.SetDelimeter(settings.CsvDelimenter);
                        settings.Encoding = ScheduledJob.SetEncoding(settings.CsvEncoding);
                    }
                    catch (Exception ex)
                    {
                        logger.Error("Invalid Settings : " + ex.ToString());
                        if (throwEception) throw new Exception("Invalid Settings");
                    }
                }
                else
                {
                    logger.Error("Invalid Settings: Request.Property SETTINGSMODEL did not return proper value. ");
                    if (throwEception) throw new Exception("Invalid Settings");
                }
                return settings;

            }
            else
            {

                // Return the settings model based on  settingsfile  caller provides (caller's parameter)
                ConfigHelper ch = new ConfigHelper();
                try
                {
                    settings = ch.ReadSettingsFile(settingsFile, FilesState.GetEncryptFilesStatus().SettingsFilesAreEncrypted);
                    if(settings==null) logger.Error("Settings is null....!!!!");
                    settings.CsvDelimenter = ScheduledJob.SetDelimeter(settings.CsvDelimenter);
                    settings.Encoding = ScheduledJob.SetEncoding(settings.CsvEncoding);
                }
                catch (Exception ex)
                {
                    logger.Error("Invalid Settings(2) : " + ex.ToString());
                    if (throwEception) throw new Exception("Invalid Settings");
                }
                logger.Info("Settings file " + (settings.SettingsFile ?? "<null>") + " is selected based on caller's parameter");
                return settings;
            }

            
        }


        /// <summary>
        /// Return the Script which is already saved in a scriptfile
        /// </summary>
        /// <param name="scriptfile">the name of scriptfile.</param>
        /// <param name="throwEception">true: if scriptfile does not exist or script==null then throw exception</param>
        /// <returns>SettingsModel or null</returns>
        public string GetScript(string scriptfile, bool throwEception = true)
        {
            try
            {
                ConfigHelper ch = new ConfigHelper();
                if (scriptfile == null || scriptfile == "")
                {
                    if (!throwEception) return null;
                    throw new Exception("Invalid Script file");

                }
                string script = ch.ReadScriptFile(scriptfile, FilesState.GetEncryptFilesStatus().ScriptFilesAreEncrypted);
                logger.Info("Scripts file " + (scriptfile ?? "<null>") + " is selected");
                return script;
            }
            catch (Exception ex)
            {
                logger.Error("Invalid Script : " + ex.ToString());
                if (throwEception) throw new Exception("Invalid Script");
            }
            return null;
        }

        /// <summary>
        /// Return if files ScheduledJobs, ScriptFiles, WebApiSettings and SettingsFiles are encrypted or not
        /// </summary>
        /// <returns></returns>
        public FilesEncryptStatus GetFilesEncryptStatus()
        {
            return FilesState.GetEncryptFilesStatus();
        }

    }
}
