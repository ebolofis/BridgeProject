using Hit.Services.Core;
using Hit.Services.Models.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Hit.Services.Helpers.Classes.Classes
{

    /// <summary>
    /// Helper Class for configuration
    /// </summary>
   public class ConfigHelper
    {

        FileHelpers fh;

        public ConfigHelper()
        {
            fh = new FileHelpers();
        }


        #region "Script files"


        /// <summary>
        /// Return the list of script files
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetScriptFiles()
        {

            List<string> files = new List<string>();
            foreach (string s in Directory.GetFiles(fh.GetScriptsPath()).Select(Path.GetFileName))
                yield return s;
        }


        /// <summary>
        /// Read a Script File. Script files may be encrypted or unencrypted
        /// </summary>
        /// <param name="path">settings filename or full path</param>
        /// <param name="isEncrypted">true if the file is encrypted</param>
        /// <returns>text</returns>
        public string ReadScriptFile(string path, bool isEncrypted)
        {
            string text = null;
            path = fh.GetScriptsPath(path);
            if (isEncrypted)
                text = fh.ReadEncryptedFile(path);
            else
                text = fh.ReadFile(path);
            return text;
        }


        /// <summary>
        /// Write a Script File. Script Files may be encrypted or unencrypted
        /// </summary>
        /// <param name="filename">filename (no path is included)</param>
        /// <param name="script">script</param>
        /// <param name="isEncrypted"></param>
        public void WriteScriptFile(string filename, string script, bool isEncrypted)
        {
            string path = fh.GetScriptsPath();
            fh.WriteTextToFile(path + "/" + filename, script, isEncrypted);
        }

        /// <summary>
        ///  Delete a Script File.
        /// </summary>
        /// <param name="filename"></param>
        public void DeleteScriptFile(string filename)
        {
            string path = fh.GetScriptsPath();
            File.Delete(path + "\\" + filename);
        }

        /// <summary>
        /// Return true if a Scripts file exists
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public bool ScriptFileExist(string filename)
        {
            string path = fh.GetScriptsPath();
            return File.Exists(path + "\\" + filename);
        }

        #endregion

        #region "Settings files"

        /// <summary>
        /// Return the list of Settings files
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetSettingsFiles()
        {
            List<string> files = new List<string>();
            foreach (string s in Directory.GetFiles(fh.GetSettingsPath()).Select(Path.GetFileName))
                yield return s;
        }

        /// <summary>
        /// Convert a Settings File to SettingsModel model. Settings file may be encrypted or unencrypted
        /// </summary>
        /// <param name="path">settings filename or full path</param>
        /// <param name="isEncrypted">true if the file is encrypted</param>
        /// <returns>SettingsModel</returns>
        public SettingsModel ReadSettingsFile(string path,bool isEncrypted)
        {
            string settings = null;
            path= fh.GetSettingsPath(path);
            if (isEncrypted)
                settings = fh.ReadEncryptedFile(path);
            else
                settings = fh.ReadFile(path);
            return JsonConvert.DeserializeObject<SettingsModel>(settings);
        }

        /// <summary>
        /// Write a Settings File. Settings Files may be encrypted or unencrypted
        /// </summary>
        /// <param name="filename">filename (no path is included)</param>
        /// <param name="model">SettingsModel</param>
        /// <param name="isEncrypted"></param>
        public void WriteSettingsFile(string filename, SettingsModel model, bool isEncrypted)
        {
            string oldfile = "";
            bool oldfileExist = false;
            //1. check if file already exist and get contents
            string path = fh.GetSettingsPath();
            try
            {
                oldfile = fh.ReadFile(path + filename);
                oldfileExist = true;
            }
            catch { }
           
            //2. if contents have changed then write to disk
            string newFile = fh.SimulateWriteToFile<SettingsModel>(path + filename, model, isEncrypted);
            if (oldfile != newFile)
            {
                //3. if old file already exist then copy toy Backup folder
                if (oldfileExist)File.Copy(path + filename, path +"Backup\\"+ filename, true);
                //4. write file
                fh.WriteToFile<SettingsModel>(path + filename, model, isEncrypted);
            }
        }

        /// <summary>
        /// Simulate the process of Writing a Settings File to a file. Return the content of the file but DO NOT save to file.
        /// </summary>
        /// <param name="filename">filename (no path is included)</param>
        /// <param name="model">SettingsModel</param>
        /// <param name="isEncrypted"></param>
        public string SimulateWriteSettingsFile(string filename, SettingsModel model, bool isEncrypted)
        {
            string path = fh.GetSettingsPath();
           return fh.SimulateWriteToFile<SettingsModel>(path + filename, model, isEncrypted);
        }

        /// <summary>
        ///  Delete a Settings File.
        /// </summary>
        /// <param name="filename"></param>
        public void DeleteSettingsFile(string filename)
        {
            string path = fh.GetSettingsPath();
            File.Delete(path + "\\" + filename);
        }

        /// <summary>
        /// Return true if a Settings file exists
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public bool SettingsFileExist(string filename)
        {
            string path = fh.GetSettingsPath();
            return  File.Exists(path + "\\" + filename);
        }

        #endregion

        #region "ScheduledJobs"

        /// <summary>
        /// Read file ScheduledJobs.json. ScheduledJobs file may be encrypted or unencrypted
        /// </summary>
        /// <returns>SchedulerModel</returns>
        public SchedulerModel ReadScheduledJobsFile(bool isEncrypted)
        {
            SchedulerModel sch;
            string json = null;
            //1. Read file /Json/ScheduledJobs.json
            string path = GetScheduledJobFile(isEncrypted);
           
            if (isEncrypted)
                json = fh.ReadEncryptedFile(path);
            else
                json = fh.ReadFile(path);
            sch = Newtonsoft.Json.JsonConvert.DeserializeObject<SchedulerModel>(json);

            foreach (var job in sch.Jobs)
            {
                if (job.ID == null || job.ID == "")
                {
                    job.ID = job.ClassName;
                }
            }
            return sch;
        }

        /// <summary>
        /// Return full path for ScheduledJobs.json.
        ///  If path /json does not exist then return "JSON_PATH_NOT_FOUND". 
        ///  If file ScheduledJobs.json does not exist then create it.
        /// </summary>
        /// <param name="path"></param>
        public string GetScheduledJobFile(bool isEncrypted)
        {
            string path = fh.GetJsonPath();
            if (path == null) return "JSON_PATH_NOT_FOUND";
            path = path + "ScheduledJobs.json";
            if(!File.Exists(path))
            {
                SchedulerModel model = new SchedulerModel() { Jobs = new List<SchedulerJob>() };
                fh.WriteToFile<SchedulerModel>(path, model, isEncrypted);
            }
            return path;
        }

        /// <summary>
        /// Write file ScheduledJobs.json. ScheduledJobs file may be encrypted or unencrypted
        /// </summary>
        /// <param name="model">SchedulerModel</param>
        /// <param name="isEncrypted"></param>
        public void WriteScheduledJobsFile(SchedulerModel model, bool isEncrypted)
        {
            //1.Get ScheduledJobs Path
            string path = GetScheduledJobFile(isEncrypted);
            fh.WriteToFile<SchedulerModel>(path, model, isEncrypted);
        }

        #endregion

        #region "Users"

        /// <summary>
        /// Return full path for Users.json.
        ///  If path /json does not exist then return "USERS_PATH_NOT_FOUND". 
        ///  If file Users.json does not exist then create it.
        /// </summary>
        /// <param name="path"></param>
        public string GetUsers()
        {
            string path = fh.GetJsonPath();
            if (path == null) return "JSON_PATH_NOT_FOUND";
            path = path + "Users.json";
            if (!File.Exists(path))
            {
                List<LoginModel> model = new List<LoginModel>();
                fh.WriteToFile<List<LoginModel>>(path, model, true);
            }
            return path;//Users.json
        }

        /// <summary>
        /// Read file Users.json. Users file is always encrypted.
        /// </summary>
        /// <returns>SchedulerModel</returns>
        public List<LoginModel> ReadUsersFile()
        {
            List<LoginModel> users;
            string json = null;
            //1. Read file /Json/Users.json
            string path = GetUsers();
                json = fh.ReadEncryptedFile(path);

            users = Newtonsoft.Json.JsonConvert.DeserializeObject<List<LoginModel>>(json);
            return users;
        }

        /// <summary>
        /// Write to file Users.json. Users file is always encrypted.
        /// </summary>
        /// <param name="model">List<LoginModel></param>
        public void WriteUsersFile(List<LoginModel> model)
        {
            //1.Get Users.json Path
            string path = GetUsers();
            fh.WriteToFile<List<LoginModel>>(path, model,true);
        }

        #endregion


        #region "WebApiSettings"

        /// <summary>
        /// Return full path for WebApiSettings.json.
        ///  If path /json does not exist then return "USERS_PATH_NOT_FOUND". 
        ///  If file WebApiSettings.json does not exist then create it.
        /// </summary>
        /// <param name="path"></param>
        public string GetWebApiSettingsPath()
        {
            string path = fh.GetJsonPath();
            if (path == null) return "JSON_PATH_NOT_FOUND";
            path = path + "WebApiSettings.json";
            if (!File.Exists(path))
            {
                List<WebApiSettingsModel> model = new List<WebApiSettingsModel>();
                fh.WriteToFile<List<WebApiSettingsModel>>(path, model, true);
            }
            return path;//Users.json
        }

        /// <summary>
        /// Read file WebApiSettings.json.  return a list of WebApiSettings
        /// </summary>
        /// <returns>WebApiSettings</returns>
        public List<WebApiSettingsModel> ReadWebApiSettings(bool isEncrypted)
        {
            string settings;
            List<WebApiSettingsModel> model;
            //1. Read file /Json/Users.json
            string path = GetWebApiSettingsPath();
            if (isEncrypted)
                settings = fh.ReadEncryptedFile(path);
            else
                settings = fh.ReadFile(path);

            model = Newtonsoft.Json.JsonConvert.DeserializeObject<List<WebApiSettingsModel>>(settings);
           // if (model.Count == 0) model.Add(new WebApiSettingsModel() { Controller = "", Settings = "" });
            return model;
        }

        /// <summary>
        /// Write to file WebApiSettings.json. 
        /// </summary>
        /// <param name="model">List<WebApiSettings></param>
        public void WriteWebApiSettings(List<WebApiSettingsModel> model, bool isEncrypted)
        {
            //1.Get Users.json Path
            string path = GetWebApiSettingsPath();
            fh.WriteToFile<List<WebApiSettingsModel>>(path, model, isEncrypted);
        }

        #endregion



        /// <summary>
        /// Constructs a connection string
        /// </summary>
        /// <param name="Server"></param>
        /// <param name="DB"></param>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public string ConnectionString(string Server,string DB, string user,string password)
        {
            return "server = "+Server+ "; user id = " + user + "; password = " + password + "; database = " + DB + "; ";
        }

      
        
    }
}
