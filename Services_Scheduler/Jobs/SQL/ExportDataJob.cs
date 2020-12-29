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
//using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hit.Scheduler.Jobs.SQL
{
    /// <summary>
    /// Run a select query to DB and export data to a file or to a Rest Server. (see also SqlController.ExportData)
    /// </summary>
    [DisplayColumn("FullClassName,Custom1DB,CustomDB1,SqlScript,SqlParameters,SqlScriptPath,DBTimeout,CultureInfo,Formater,TimeStamp,XmlFilePath,XmlRootElement,XmlElement,JsonFilePath,CsvFilePath,CsvFileHeader,CsvDelimenter,FixedLenghtFilePath,FixedLenghtFileHeader,FixedLenghtAlignRight,FixedLengths,HtmlFilePath,HtmlHeader,HtmlTitle,Htmlcss,RestServerUrl,RestServerAuthenticationHeader,RestServerAuthenticationType,RestServerHttpMethod,RestServerMediaType,RestServerCustomHeaders,PdfFilePath,Pdfcss,PdfTitle")]
    [Describe(Type = "Job", Description = @"

Jod that call 'Export Data' Service: Service gets data running an sql script and then exports the selected data to at least one File or/and to a Rest Server. 

The relaited SQL Script must return one or two Data Sets (select statements):
   1st Data Set: Data to export to file or/and to Rest Server.
   2nd Data Set: Updated values for the sql parameters (optional).

It also returns the data to caller (see also SqlController.ExportData).

EXAMPLE (1 Data Set): 
           select  TOP (10)  [kdnr],[name1],[name2],[outldate],[prof],[landkz]as lkz INTO #TempTable FROM [protel].[proteluser].[kunden]

EXAMPLE (2 Data Sets): 
        IF object_id('tempdb..#TempTable') is not null  drop table #TempTable
        select  TOP (10)  [kdnr],[name1],[name2],[outldate],[prof],[landkz]as lkz INTO #TempTable FROM [protel].[proteluser].[kunden] where kdnr>@Id 
        SELECT * from #TempTable                    -- <-- 1st Data Set
        SELECT max(kdnr) as Id from #TempTable      -- <-- 2nd Data Set
        drop table #TempTable 
In the example above the 2nd Data Set contains the updated value 'Id' for the SQLParameter @Id (see SQL Script section)")]
    public class ExportDataJob : ScheduledJob
    {
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
                SQLFlows sqlflows = new SQLFlows(settings);
                sqlflows.ExportData(script);

                logger.Info("Job " + settingsId + " Finished.");
            }
            catch (Exception ex)
            {
                logger.Error("Error job '"+ settingsId + "' : "+ex.ToString());
                throw;
            }
        }
    }
}
