using Hit.Services.Core;
using Hit.Services.MainLogic.Flows.SQLjobs;
using Hit.Services.Models.CustomAnotations;
using Hit.Services.Models.Models;
using Hit.Services.WebApi.Filters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Hit.WebApi.Controllers.SQL
{
    [RoutePrefix("api/data")]
    [DisplayColumn("FullClassName,Custom1DB,CustomDB1,SqlScript,SqlParameters,DBTimeout,SqlScriptPath,CultureInfo,Formater,TimeStamp,XmlFilePath,XmlRootElement,XmlElement,JsonFilePath,CsvFilePath,CsvFileHeader,CsvDelimenter,FixedLenghtFilePath,FixedLenghtFileHeader,FixedLenghtAlignRight,FixedLengths,HtmlFilePath,HtmlHeader,HtmlTitle,Htmlcss,RestServerUrl,RestServerAuthenticationHeader,RestServerHttpMethod,RestServerMediaType,RestServerCustomHeaders,PdfFilePath,Pdfcss,PdfTitle")]
    [Describe(Type = "Controller", Description = @"

Web Api Controller that serves the following actions: 

   1. Call 'Export Data' Service: 
            Export data to file and/or return them to caller. 
            URL: <basic url>/api/data/getdata/<settings>

   2. Call 'Run Script' Service.
      Run an Sql Script. 
      URL: <basic url>/api/data/run/<settings>

Where <settings> is the name of an optional settings file. If no settings file is provided to url, the settings file must be pre-assosiated with the SqlController in config application

Every setting file must be related with a sql script.

SqlController requires a user defined to config application
")]
    public class SqlController : HitController
    {

        /// <summary>
        /// Run a query to DB (based on settingsfile) and return data. (see ExportDataJob)
        /// </summary>
        /// <param name="resno">reservation number</param>
        /// <returns></returns>
        [HttpGet, Route("getdata/{settingsfile?}")]
        [Authorization]
        public HttpResponseMessage ExportData(string settingsfile="")
        {
            //1. Get settings
            SettingsModel settings = GetSettings(settingsfile);

            //2. Get script to run
            string sqlScript = GetScript(settings.SqlScript);

            //3. Call Main Logic
            SQLFlows flow = new SQLFlows(settings);
            IEnumerable<dynamic> results = flow.ExportData(sqlScript);

            //4. Return results
            return Request.CreateResponse(HttpStatusCode.OK, results);
        }


        /// <summary>
        /// Execute a script to DB (based on settingsfile). (see RunScriptJob)
        /// </summary>
        /// <param name="resno">reservation number</param>
        /// <returns></returns>
        [HttpGet, Route("run/{settingsfile?}")]
        [Authorization]
        public HttpResponseMessage RunScript(string settingsfile = "")
        {
            //1. Get settings
            SettingsModel settings = GetSettings(settingsfile);

            //2. Get script to run
            string sqlScript = GetScript(settings.SqlScript);

            //3. Call Main Logic
            SQLFlows flow = new SQLFlows(settings);
            flow.RunScript(sqlScript);

            //4. Return results
            return Request.CreateResponse(HttpStatusCode.OK);
        }

    }
}
