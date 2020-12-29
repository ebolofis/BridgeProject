using AutoMapper;
using Hit.Services.Core;
using Hit.Services.DataAccess.DT.Sql;
using Hit.Services.DataAccess.DT.SQL;
using Hit.Services.Helpers.Classes.Classes;
using Hit.Services.Models.Models;
using Hit.Services.Models.Models.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Hit.Services.MainLogic.Tasks.SQLJobs
{
    /// <summary>
    /// Task to execute generic queries and castings
    /// </summary>
    public class SQLTasks
    {

        SettingsModel settings;
    //    string sqlScript;
        ConfigHelper configHelper;
        GenericDT RunScriptDT;

        /// <summary>
        /// Task to execute generic queries and castings
        /// </summary>
        /// <param name="settings">Settings Model</param>
        /// <param name="script">sql script to execute</param>
        public SQLTasks(SettingsModel settings)
        {
            this.settings = settings;
            configHelper = new ConfigHelper();
            RunScriptDT = new GenericDT();
        }

        /// <summary>
        ///  Run sql script (insert/updates/deletes/creates etc)
        /// </summary>
        /// <param name="sqlScript">sql script to run</param>
        /// <param name="conString">connection string. If null then pick settings.Custom1DB</param>
        public void RunScript(string sqlScript, string conString = null)
        {
            if (conString == null) conString = settings.Custom1DB;
            int timeout = Int32.Parse(settings.DBTimeout);
            //Exec Script to DB
            RunScriptDT.RunScript(conString, sqlScript, timeout);
        }



        /// <summary>
        /// Return the results of a select query as IEnumerable(dynamic). (Use AutoMapper to convert dynamic to specific model)
        /// </summary>
        /// <param name="sqlScript">sql script to run</param>
        /// <param name="conString">connection string. If null then pick  settings.Custom1DB </param>
        /// <returns>the reusult of the script</returns>
        public IEnumerable<dynamic> RunSelect(string sqlScript, string conString = null)
        {
            if (conString == null) conString = settings.Custom1DB;
            int timeout = Int32.Parse(settings.DBTimeout);
            //Exec Script to DB
            return RunScriptDT.RunSelect(conString, sqlScript, timeout);
        }

        /// <summary>
        /// Return the results of a select query as IEnumerable(dynamic). (Use AutoMapper to convert dynamic to specific model)
        /// </summary>
        /// <param name="sqlScript">sql script to run</param>
        /// <param name="conString">connection string. If null then pick  settings.Custom1DB </param>
        /// <returns>the reusult of the script</returns>
        public List<IEnumerable<dynamic>> RunMultySelect(string sqlScript,string conString=null)
        {
            if (conString == null) conString = settings.Custom1DB;
            int timeout =Int32.Parse(settings.DBTimeout);
            //Exec Script to DB
            return RunScriptDT.RunMultipleSelect(conString, sqlScript, timeout);
        }

       

   

    }
}
