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
using Hit.Services.MainLogic.Tasks.SQLJobs;
using Hit.Services.MainLogic.Flows.ProtelToPylonMappings;
using Hit.Services.Models.Models.ProtelMappingsToPylon;
using System.Net;
using Newtonsoft.Json;
using System.IO;

namespace Hit.Scheduler.Jobs.SQL //Hit.Scheduler.Jobs.
{
    [DisplayColumn("FullClassName,ProtelDB,PylonDB,SqlScript,SqlParameters,DBTimeout,SqlScriptPath,CultureInfo,Formater,TimeStamp,FixedLenghtFilePath,FixedLenghtFileHeader,FixedLenghtAlignRight,FixedLengths,RestServerUrl,RestServerAuthenticationHeader,RestServerHttpMethod,RestServerMediaType,RestServerCustomHeaders")]
    [Describe(Type = "Job", Description = @"")]
    public class ProtelEODJob : ScheduledJob
    {
        ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        [DisableConcurrentExecution(5)]
        [AutomaticRetry(Attempts = 0, LogEvents = true, OnAttemptsExceeded = AttemptsExceededAction.Fail)]
        public override void Execute(string settingsfile, string settingsId, string installation)
        {
            logger.Info("Running Job :" + settingsId);
            ExportDataTasks exportDataTask;

            SettingsModel settings = GetSettings(settingsfile);
            PostDataToPylonFlows flow = new PostDataToPylonFlows(settings);
            List<PostDataToPylonModel> results = new List<PostDataToPylonModel>();
            //results = flow.PostDataToPylon("");

            exportDataTask = new ExportDataTasks(settings);
            string PylonJson = JsonConvert.SerializeObject(results);
            string temp = exportDataTask.PostToRestServer<string>(PylonJson, "insertMessages");

            PostDataToPylonFlows innerflow = new PostDataToPylonFlows(settings);
           
           // innerflow.FlagSentRecords(listtobeflaged);


            
        }
    }
}
