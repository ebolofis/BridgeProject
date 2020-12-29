using Hit.Services.Core;
using Hit.Services.DataAccess.DT.Sql;
using Hit.Services.DataAccess.DT.SQL;
using Hit.Services.Helpers.Classes;
using Hit.Services.Helpers.Classes.Classes;
using Hit.Services.MainLogic.Tasks.SQLJobs;
using Hit.Services.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hit.Services.MainLogic.Tasks.SQLJobs
{
    /// <summary>
    /// Tasks for managing SqlParameters
    /// </summary>
    public class SqlParametersTasks
    {

        SettingsModel settings;
        //    string sqlScript;
        ConfigHelper configHelper;
        GenericDT RunScriptDT;
        ConvertDynamicHelper castDynamic;

        /// <summary>
        /// Tasks for managing SqlParameters
        /// </summary>
        /// <param name="settings">Settings Model</param>
        /// <param name="script">sql script to execute</param>
        public SqlParametersTasks(SettingsModel settings)
        {
            this.settings = settings;
            configHelper = new ConfigHelper();
            RunScriptDT = new GenericDT();
            castDynamic = new ConvertDynamicHelper();
        }

        /// <summary>
        /// Get an sqlScript and replace parameters (ex:@Id) with values (sqlParameters).
        /// </summary>
        /// <param name="sqlScript"></param>
        /// <returns></returns>
        public string PrepareSqlScript(string sqlScript)
        {
            if (settings.SqlParameters == null || settings.SqlParameters.Count == 0) return sqlScript;

            HitServicesDT hitServicesDt = new HitServicesDT();

            foreach (string key in settings.SqlParameters.Keys)
            {

                //1. get parameter's value from HangFireDB
                string value = hitServicesDt.GetParameterValue(HangFireDB.ConString, settings.SettingsFile, key);

                //2. if parameter does not exist in HangFireDB then get the initial value from settingsfile
                if (value == null) value = settings.SqlParameters[key.Trim()].Replace("'", "''");

                //2. replace value to sql script
                sqlScript = sqlScript.Replace(key, "'" + value + "'");
            }
            return sqlScript;
        }

        /// <summary>
        /// Return an SQL parameter's  value 
        /// </summary>
        /// <param name="settingsfile">settingsfile</param>
        /// <param name="parameter">parameter</param>
        /// <returns></returns>
        public string GetSqlParameter(string settingsfile, string parameter)
        {
            HitServicesDT hitServicesDt = new HitServicesDT();
            return hitServicesDt.GetParameterValue(HangFireDB.ConString, settingsfile, parameter);
        }


        /// <summary>
        /// Return an SQL parameter's value for the current settings file
        /// </summary>
        /// <param name="parameter">parameter</param>
        /// <returns></returns>
        public string GetSqlParameter(string parameter)
        {
            HitServicesDT hitServicesDt = new HitServicesDT();
            return hitServicesDt.GetParameterValue(HangFireDB.ConString, settings.SettingsFile, parameter);
        }


        /// <summary>
        /// Update or Insert (one or more) SQL Parameters to HangFireDB
        /// </summary>
        /// <param name="newSqlParameters">the new SQL Parameters</param>
        public void UpdateSqlParams( IEnumerable<dynamic> newSqlParameters)
        {
            if (newSqlParameters == null) return;

            HitServicesDT hitServicesDt = new HitServicesDT();
            SQLTasks sqlTasks = new SQLTasks(settings);
            IDictionary<string, dynamic> sqlParamsDict = castDynamic.ToListDictionary(newSqlParameters).FirstOrDefault();
            if (sqlParamsDict == null) return;
            foreach (string key in sqlParamsDict.Keys)
            {
                if (settings.SqlParameters.ContainsKey("@" + key)|| settings.SqlParameters.ContainsKey( key))
                    hitServicesDt.UpdateParameter(HangFireDB.ConString, settings.SettingsFile, "@" + key, sqlParamsDict[key].ToString());
            }
        }

        /// <summary>
        /// Update or Insert one SQL Parameters (for the current settings file) to HangFireDB
        /// </summary>
        /// <param name="parameter">parameter</param>
        /// <param name="newValue">new value</param>
        public void UpdateSqlParam(string parameter,string newValue)
        {
            if (newValue == null || parameter==null) return;

            HitServicesDT hitServicesDt = new HitServicesDT();
            hitServicesDt.UpdateParameter(HangFireDB.ConString, settings.SettingsFile, parameter, newValue);
        }

    }
}
