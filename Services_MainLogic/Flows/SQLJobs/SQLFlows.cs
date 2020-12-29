using System.Collections.Generic;
using System.Linq;
using Hit.Services.Core;
using Hit.Services.DataAccess.DT.Sql;
using Hit.Services.Helpers.Classes.Classes;
using Hit.Services.MainLogic.Tasks.SQLJobs;
using Hit.Services.Models.Models;
using Hit.Services.Models.Models.SQL;

namespace Hit.Services.MainLogic.Flows.SQLjobs
{
    /// <summary>
    /// Class containing Tasks to run select/insert/update/delete generic queres
    /// </summary>
    public class SQLFlows
    {

        SQLTasks sqlTasks;
        SettingsModel settings;
        FileHelpers fh = new FileHelpers();
        ExportDataTasks exportDataTask;
        SqlParametersTasks sqlParametersTasks;
        GenericDT sqlScriptsDT;
        Dictionary<string, string> sqlScripts;

        /// <summary>
        /// constractor
        /// </summary>
        /// <param name="settings">Settings Model</param>
        public SQLFlows(SettingsModel settings)
        {
            this.settings = settings;
           
        }

        /// <summary>
        /// Run sql script (insert/updates/deletes)
        /// </summary>
        public void RunScript(string sqlScript)
        {
            sqlTasks = new SQLTasks(settings);
            sqlParametersTasks = new SqlParametersTasks(settings);

            //1. Replace sqlScript's parameters (ex:@Id) with values from HangFireDB.
            sqlScript = sqlParametersTasks.PrepareSqlScript(sqlScript);

            //2. Run sql script (insert/updates/deletes) and return updated sql parameters
            IEnumerable<dynamic> newSqlParameters = sqlTasks.RunSelect(sqlScript);
            //sqlTasks.RunScript(sqlScript);

            //3. Update SQL Parameters to HangFireDB
            sqlParametersTasks.UpdateSqlParams(newSqlParameters);
        }

        /// <summary>
        ///  Run a select query and then save as file OR/AND return to local api OR/AND post to an external Rest Server. 
        ///  Return data as dynamic list.
        /// </summary>
        /// <param name="sqlScript">the sql script to produce data</param>
        /// <returns></returns>
        public IEnumerable<dynamic> ExportData(string sqlScript)
        {
            sqlTasks = new SQLTasks(settings);
            sqlParametersTasks = new SqlParametersTasks(settings);
            exportDataTask = new ExportDataTasks(settings);

            //1. Replace sqlScript's parameters (ex:@Id) with values from HangFireDB.
            sqlScript = sqlParametersTasks.PrepareSqlScript(sqlScript);

            //2. select data from DB
            IEnumerable<dynamic> newSqlParameters = null;
            List<IEnumerable<dynamic>> rawDataList = sqlTasks.RunMultySelect(sqlScript);

            //3. Manipulate data:  If rawDataList contains 2 items then the 1st item contains the Data and  the 2nd contains  new values for sqlScript's parameters.
            IEnumerable<dynamic> rawData = null;

            //    3a. sql script does not return any datasets
            if (rawDataList == null || rawDataList.Count() == 0)
                rawData = new List<dynamic>(); // <--- Selected Data
            else
            {
                //3b. sql script  return (at least) one dataset
                rawData = rawDataList[0];

                //3c. sql script return two datasets
                if (rawDataList.Count() >= 2)
                    newSqlParameters = rawDataList[1];
            }

            //4. Export Data to FILE (xml, csv, fixes length, json, html, pdf) or post data to rest server
            exportDataTask.ExportData(rawData);

            //5. Update SQL Parameters to HangFireDB
            sqlParametersTasks.UpdateSqlParams(newSqlParameters); 

            //6. return data
            return rawData;
        }

        /// <summary>
        /// Get Data from a select statment and insert/update/upsert in DB Table. 
        /// </summary>
        /// <param name="sqlScripts">Dictionary with the required scripts. 
        ///   Two keys are required: 
        ///    'MAIN' for the main script running to source DB and 
        ///    'PRE'  for the pre-insert/update script running to destination DB</param>
        /// <returns>return selected data</returns>
        public IEnumerable<dynamic> SaveDataToDB(Dictionary<string, string> sqlScripts)
        {
            sqlTasks = new SQLTasks(settings);
            sqlParametersTasks = new SqlParametersTasks(settings);
            exportDataTask = new ExportDataTasks(settings);
            sqlScriptsDT = new GenericDT();

            string sqlScript = sqlScripts["MAIN"];
            string preSqlScript = sqlScripts["PRE"];

            //1. Replace sqlScript's parameters (ex:@Id) with values from  HangFireDB.
            sqlScript = sqlParametersTasks.PrepareSqlScript(sqlScript);

            //2. select data from DB
            IEnumerable<dynamic> newSqlParameters = null;
            List<IEnumerable<dynamic>> rawDataList = sqlTasks.RunMultySelect(sqlScript,settings.SourceDB);

            //3. Manipulate data:  If rawDataList contains 2 items then the 1st item contains the Data and  the 2nd contains new values for sqlScript's parameters.
            IEnumerable<dynamic> rawData = null;

            //    3a. sql script does not return any datasets
            if (rawDataList == null || rawDataList.Count() == 0)
                   rawData = new List<dynamic>(); // <--- Selected Data
            else
            {
                //3b. sql script  return (at least) one dataset
                rawData = rawDataList[0];

                //3c. sql script return two datasets
                if (rawDataList.Count() >= 2)
                    newSqlParameters = rawDataList[1];
            }
            //4. Run Destination SQL Pre-Insert/Update Script
            if (!string.IsNullOrEmpty(settings.SqlDestPreScript)) sqlTasks.RunScript(preSqlScript, settings.DestinationDB);

            //5. Read Destination's Table info
            DbTableModel tableInfo = sqlScriptsDT.GetTableInfo(settings.DestinationDB, settings.DestinationDBTableName);

            //6. Save Data to destination Table
            exportDataTask.SaveDataToDB(rawData, tableInfo);
            //7. Update SQL Parameters to HangFireDB
            sqlParametersTasks.UpdateSqlParams(newSqlParameters); 

            //8. return data
            return rawData;

        }

        /// <summary>
        /// Read a CSV file and then save the selected data (insert or/and update) to a destination DB Table.
        /// </summary>
        /// <param name="preSqlScript"> pre-insert/update script running to destination DB</param>
        ///
        public void ReadFromCsv(string preSqlScript)
        {
            sqlTasks = new SQLTasks(settings);
            sqlParametersTasks = new SqlParametersTasks(settings);
            exportDataTask = new ExportDataTasks(settings);
            sqlScriptsDT = new GenericDT();

          
            //1. read data from csv file
            List<IDictionary<string, dynamic>> rawData = fh.ReadCsvFile(settings.CsvFilePath, settings.CsvDelimenter,settings.CsvFileHeader.Value, settings.CsvFileHeaders, settings.Encoding).ToList();

            //2. Run Destination SQL Pre-Insert/Update Script
            if (!string.IsNullOrEmpty(settings.SqlDestPreScript)) sqlTasks.RunScript(preSqlScript, settings.DestinationDB);

            //3. Read Destination's Table info
            DbTableModel tableInfo = sqlScriptsDT.GetTableInfo(settings.DestinationDB, settings.DestinationDBTableName);

            //4. Save Data to destination Table
            exportDataTask.SaveDataToDB(rawData, tableInfo);
           

        }
    }

}
