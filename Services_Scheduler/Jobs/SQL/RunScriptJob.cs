using Hangfire;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hit.Services.MainLogic.Flows.SQLjobs;
using Hit.Services.Helpers.Classes.Classes;
using Hit.Services.Models.Models;
using System.ComponentModel.DataAnnotations;
using Hit.Services.MainLogic.Tasks.SQLJobs;
using Hit.Services.Models.CustomAnotations;
using Hit.Services.Core;

namespace Hit.Scheduler.Jobs.SQL
{
    /// <summary>
    /// Execute a script to DB
    /// </summary>
    [DisplayColumn("FullClassName,Custom1DB,SqlScript,SqlParameters,DBTimeout")]//Fields for settings to display on Encrypt Decrypt
    [Describe(Type = "Job", Description = @"

Jod that cals 'Run Script' Service. Service runsJod that runs a script (inserts/updates/deletes) to a DB.

The relaited SQL Script must perform an action to DB and optionally return a Data Set with the updated values for the sql parameters.


EXAMPLE (No sql parameters return): 
           update EndOfDay_Hist set Barcodes=0  where Barcodes is null 

EXAMPLE (1 Data Set return): 
        IF object_id('tempdb..#TempTable') is not null  drop table #TempTable
        select  id  INTO #TempTable FROM EndOfDay_Hist where Id>@Id 
        update EndOfDay_Hist set Barcodes=0 
                    where Barcodes is null and id>@Id  -- <-- action to perform
        SELECT max(Id) as Id from #TempTable       -- <-- 1st Data Set (Updated sql parameters)
        drop table #TempTable 
In the example above the Data Set contains the updated value 'Id' for the SQLParameter @Id (see SQL Script section)


")]
    public class RunScriptJob : ScheduledJob
    {
      

        public RunScriptJob()
        { }

        /// <summary>
        /// Entry Point
        /// </summary>
        /// <param name="settingsfile">settings file</param>
        [DisableConcurrentExecution(5)]
        [AutomaticRetry(Attempts = 0, LogEvents = true, OnAttemptsExceeded = AttemptsExceededAction.Fail)]
        public override void Execute(string settingsfile, string settingsId, string installation)
        {
            logger.Info("Running Job :" + settingsId);
            try
            {
                //1. get Settings and script
                SettingsModel settings = GetSettings(settingsfile);
                string script = GetScript(settings.SqlScript);

                //2. Call Main Logic
                SQLFlows sqlflows = new SQLFlows(settings );
                sqlflows.RunScript(script);

                logger.Info("Job " + settingsId + " Finished.");
            }
            catch(Exception ex)
            {
                logger.Error("Error job '" + settingsId + "' : " + ex.ToString());
                throw;   //>>> Throw the exception to be coutch by the hangfire
            }
          
        }
    }
}
