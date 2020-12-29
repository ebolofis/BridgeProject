using Hangfire;
using log4net;
using Hit.Scheduler.Jobs;
using Hit.Services.Core;
using Hit.Services.MainLogic.Flows.SQLjobs;
using Hit.Services.Models.CustomAnotations;
using Hit.Services.Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hit.Scheduler.Jobs.SQL
{
    /// <summary>
    /// Run a select query to DB and export data to a file or to a Rest Server. (see also SqlController.ExportData)
    /// </summary>
    [DisplayColumn("FullClassName,SourceDB,DestinationDB,DestinationDBTableName,DBOperation,DBTransaction,SqlScript,SqlParameters,SqlDestPreScript,DBTimeout")]
    [Describe(Type = "Job", Description = @"

Jod that gets data running an sql script and then saves the selected data (insert or/and update) to a destination DB Table. 

The relaited SQL Script must return one or two Data Sets (select statements):
   1st Data Set: Data to save to destination table.
   2nd Data Set: Updated values for the sql parameters (optional).

It also returns the data to caller.

EXAMPLE (1 Data Set): 
           select  TOP (10)  [kdnr],[name1],[name2],[outldate],[prof],[landkz]as lkz INTO #TempTable FROM [protel].[proteluser].[kunden]

EXAMPLE (2 Data Sets): 
        IF object_id('tempdb..#TempTable') is not null  drop table #TempTable
        select  TOP (10)  [kdnr],[name1],[name2],[outldate],[prof],[landkz]as lkz INTO #TempTable FROM [protel].[proteluser].[kunden] where kdnr>@Id 
        SELECT * from #TempTable                    -- <-- 1st Data Set
        SELECT max(kdnr) as Id from #TempTable      -- <-- 2nd Data Set
        drop table #TempTable 
In the example above the 2nd Data Set contains the updated value 'Id' for the SQLParameter @Id (see SQL Script section)")]
    public class SaveToTableJob : ScheduledJob
    {
        [DisableConcurrentExecution(5)]
        [AutomaticRetry(Attempts = 0, LogEvents = true, OnAttemptsExceeded = AttemptsExceededAction.Fail)]
        public override void Execute(string settingsfile, string settingsId, string installation)
        {
            logger.Info("Running Job :" + settingsId);

            try
            {
                //1. get Settings
                SettingsModel settings = GetSettings(settingsfile);

                //2. get Scripts. Dictionary must contain two keys : 
                //                 'MAIN' for the main script running to source DB and 
                //                 'PRE'  for the pre-insert/update script running to destination DB
                Dictionary<string, string> sqlScripts = new Dictionary<string, string>();
                sqlScripts.Add("MAIN", GetScript(settings.SqlScript));
               if(!string.IsNullOrEmpty(settings.SqlDestPreScript))
                    sqlScripts.Add("PRE", GetScript(settings.SqlDestPreScript));
               else
                    sqlScripts.Add("PRE", null);

                //3. Call Main Logic
                SQLFlows sqlflows = new SQLFlows(settings);
                sqlflows.SaveDataToDB(sqlScripts);

                logger.Info("Job " + settingsId + " Finished.");

            }
            catch (Exception ex)
            {
                logger.Error("Error job '" + settingsId + "' : " + ex.ToString());
                throw;   //>>> Throw the exception to be coutch by the hangfire
            }
        }
    }
}
