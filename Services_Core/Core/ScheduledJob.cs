using log4net;
using Hit.Services.Core;
using Hit.Services.Helpers.Classes;
using Hit.Services.Helpers.Classes.Classes;
using Hit.Services.Models.Models;
using Hit.Services.Scheduler.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hit.Scheduler.Jobs
{

    /// <summary>
    /// Abstract class that every Job class  MUST be inherited from.
    /// </summary>
    public abstract class ScheduledJob
    {
       protected ILog logger;
        /// <summary>
        /// Keep if files ScheduledJobs, ScriptFiles and SettingsFiles are encrypted or not.
        /// </summary>
        protected FilesEncryptStatus FilesStatus;
        protected ConfigHelper ConfigHelper;

        //Determine Jobs Origin
        public static List<JobsAssemblyModel> JobsOriginList = new List<JobsAssemblyModel>();

        public ScheduledJob()
        {
            logger = log4net.LogManager.GetLogger(this.GetType());
            FilesStatus = FilesState.GetEncryptFilesStatus();
            ConfigHelper = new ConfigHelper();
        }


        /// <summary>
        /// Entry point for a job
        /// </summary>
        /// <param name="settingsfile">the name of the settings file (without path).</param>
        /// <param name="settingsId">Contain the JobId or the Controller full class name .</param>
        /// <param name="installation">Contain the Installation unique name</param>
        /// <param name="logger">Contain the Log File</param>
        public abstract void Execute(string settingsfile, string settingsId, string installation);

        /// <summary>
        /// Return the SettingsModel for the Job (Maybe null if there is no seetings file are set in configuration)
        /// </summary>
        /// <param name="settingsfile">the settings file.</param>
        /// <param name="throwEception">true: if SettingsModel==null then throw exception</param>
        /// <returns>SettingsModel or null</returns>
        public SettingsModel GetSettings(string settingsfile, bool throwEception=true)
        {
            try
            {
                if (settingsfile == null || settingsfile == "")
                {
                    if (!throwEception) return null;
                    throw new Exception("Invalid Settings file");
                }
                SettingsModel sm = ConfigHelper.ReadSettingsFile(settingsfile, HitServices_Core.Properties.Settings.Default.SettingsFilesAreEncrypted);
                sm.CsvDelimenter = SetDelimeter(sm.CsvDelimenter);
                sm.Encoding = SetEncoding(sm.CsvEncoding);
                return sm;
            }
            catch (Exception ex)
            {
                logger.Error("Invalid Settings : " + ex.ToString());
                if (throwEception) throw new Exception("Invalid Settings");
            }
            return null; 
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
                    throw new Exception("Invalid Script file '"+(scriptfile??"<null>") +"'");

                }
                string  script = ch.ReadScriptFile(scriptfile, HitServices_Core.Properties.Settings.Default.ScriptFilesAreEncrypted);

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
        /// Set delimeter (;,Space,Tab,-,@,#,$,^,~,_,+,:) for csv file
        /// </summary>
        /// <returns></returns>
        public static string SetDelimeter(string delimeter)
        {
            switch (delimeter)
            {
                case "Comma":
                    return ",";
                case "Space":
                    return " ";
                case "Tab":
                    return "\t";
                default:
                    return delimeter;
            }
        }

        /// <summary>
        /// set Encoding
        /// </summary>
        /// <param name="encoding">encoding as string : UTF8,ASCII,Unicode,UTF32,UTF7,BigEndianUnicode</param>
        /// <returns></returns>
        public static Encoding SetEncoding(string encoding="")
        {
            switch (encoding)
            {
                case "UTF8":
                    return Encoding.UTF8;
                case "ASCII":
                    return Encoding.ASCII;
                case "Unicode":
                    return Encoding.Unicode;
                case "UTF32":
                    return Encoding.UTF32;
                case "UTF7":
                    return Encoding.UTF7;
                case "BigEndianUnicode":
                    return Encoding.BigEndianUnicode;
               
                default:
                    return Encoding.Default;
            }
        }


        /// <summary>
        /// if smtp settings have been configured send an error email
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="settingsId"></param>
        /// <param name="installation"></param>
        /// <param name="errorMessage"></param>
        public void SendEmailOnError(SettingsModel settings, string settingsId,string installation,Exception ex)
        {
            if (string.IsNullOrEmpty(settings.ErrorEmailSmtp) ||
                settings.ErrorEmailPort == 0 ||
                string.IsNullOrEmpty(settings.ErrorEmailUsername) ||
                string.IsNullOrEmpty(settings.ErrorEmailPassword) 
                )
                return;

            try
            {
                EmailSendModel email = new EmailSendModel();

                email.From = settings.ErrorEmailFrom;
                email.To = settings.ErrorEmailTo;
                email.Subject = "Hit Services - Error Executing Job '" + settingsId + "'";
                email.Body = @"
                    <b>Installation : </b>" + installation + @"<br>
                    <b>Job  : </b>" + settingsId + @"<br>
                    <b>Date : </b>" + DateTime.Now.ToString("dd/MM/yyyy HH:mm") + @"
                    <br><br><b> Error Message:</b> " + ex.Message + @"
                    <br><br><br><b> Error Info:</b> 
                    <p style='background-color:#f7f7f9;'><font size='2'>" + ex.ToString().Replace(Environment.NewLine, "<br>") + "</font><br><br></p>";
                    
                    

                EmailHelper sendEmail = new EmailHelper();
                sendEmail.Init(smtp: settings.ErrorEmailSmtp,
                               port: settings.ErrorEmailPort,
                               ssl: settings.ErrorEmailSsl,
                               username: settings.ErrorEmailUsername,
                               password: settings.ErrorEmailPassword);

                sendEmail.Send(email);
            }catch(Exception ex2)
            {
                logger.Error("Error sending Error-Email: " + ex2.ToString());
            }
           
        }


    }

}
