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

namespace Hit.Services.Scheduler.Jobs.SQL
{
    /// <summary>
    /// Read a CSV file and then save the selected data (insert or/and update) to a destination DB Table
    /// </summary>
    [DisplayColumn("FullClassName,DestinationDB,DestinationDBTableName,DBOperation,DBTransaction,SqlDestPreScript,DBTimeout,CsvFilePath,CsvFileHeader,CsvDelimenter,CsvFileHeaders,CsvEncoding")]
    [Describe(Type = "Job", Description = @"

Jod that reads a CSV file and then saves the selected data (insert or/and update) to a destination DB Table. 

")]
    public  class ReadFromCsvJob : ScheduledJob
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

                //2. get Scripts
               string sqlpreScript = GetScript(settings.SqlDestPreScript,false);
               

                //3. Call Main Logic
                SQLFlows sqlflows = new SQLFlows(settings);
                sqlflows.ReadFromCsv(sqlpreScript);

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
