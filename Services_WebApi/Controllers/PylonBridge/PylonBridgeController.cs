using Hit.Services.Core;
using Hit.Services.MainLogic.Flows.ProtelToPylonMappings;
using Hit.Services.MainLogic.Flows.SQLJobs;
using Hit.Services.Models.CustomAnotations;
using Hit.Services.Models.Models.ProtelMappingsToPylon;
using Hit.Services.Models.Models.QueriesLogistics;
using Hit.Services.WebApi.Filters;
using Hit.WebApi.Controllers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;

using System.Web.Mvc;

namespace Hit.Services.WebApi.Controllers.PylonBridge
{
    [RoutePrefix("PylonBridge")]
    [DisplayColumn("FullClassName,ProtelDB,PylonDB,SqlScript,SqlParameters,DBTimeout,SqlScriptPath,CultureInfo,Formater,TimeStamp,FixedLenghtFilePath,FixedLenghtFileHeader,FixedLenghtAlignRight,FixedLengths,RestServerUrl,RestServerAuthenticationHeader,RestServerHttpMethod,RestServerMediaType,RestServerCustomHeaders")]
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
    public class PylonBridgeController : HitController
    {

        [HttpGet, Route("GetProtelVat/{settingsfile?}")]
        //[Authorization]
        public HttpResponseMessage GetVats(string settingsfile = "")
        {
            //1. Get settings
            SettingsModel settings = GetSettings();

            //2. Get script to run
            //string sqlScript = GetScript(settings.SqlScript);

            //3. Call Main Logic
            VatsFlows flow = new VatsFlows(settings);
            List<MwstModel> results = new List<MwstModel>();
            results = flow.GetVats();

            Dictionary<string, string> resultsDictionary =
                 results.ToDictionary(k => k.satznr.ToString(), v => v.satz.ToString());


            return Request.CreateResponse(HttpStatusCode.OK, resultsDictionary);
        }

        [HttpGet, Route("GetPaymentMethods/{settingsfile?}")]
        public HttpResponseMessage GetPaymentMethods(string settingsfile = "")
        {
            //1. Get settings
            SettingsModel settings = GetSettings();
            PaymentMethodsFlows flow = new PaymentMethodsFlows(settings);
            List<ZahlartModel> results = new List<ZahlartModel>();
            results = flow.GetPaymentMethods();

            Dictionary<string, string> resultsDictionary =
                results.ToDictionary(k => k.zanr.ToString(), v => v.bez.ToString());

            return Request.CreateResponse(HttpStatusCode.OK, resultsDictionary);


        }

        [HttpGet, Route("GetMatchComDocs/{mpehotel}")]
        public HttpResponseMessage GetDocs(string mpehotel = "0")
        {
            //1. Get settings
            SettingsModel settings = GetSettings();
            GetDocsFlows flow = new GetDocsFlows(settings);
            List<FiscalcdModel> results = new List<FiscalcdModel>();
            results = flow.GetDocs(mpehotel);

            Dictionary<string, string> resultsDictionary =
               results.ToDictionary(k => k.Ref.ToString(), v => v.text.ToString());

            return Request.CreateResponse(HttpStatusCode.OK, resultsDictionary);
        }


        [HttpGet, Route("GetServices/{mpehotel}")]
        public HttpResponseMessage GetServices(string mpehotel = "0")
        {
            //1. Get settings
            SettingsModel settings = GetSettings();
            ServicesFlows flow = new ServicesFlows(settings);
            List<UktoModel> results = new List<UktoModel>();
            results = flow.GetServices(mpehotel);

            Dictionary<string, string> resultsDictionary =
               results.ToDictionary(k => k.ktonr.ToString(), v => v.bez.ToString());

            return Request.CreateResponse(HttpStatusCode.OK, resultsDictionary);
        }

        [HttpGet, Route("GetMatchingBranch/{settingsfile?}")]
        public HttpResponseMessage GetMatchingBranch(string settingsfile = "")
        {
            //1. Get settings
            SettingsModel settings = GetSettings();
            BranchFlows flow = new BranchFlows(settings);
            List<LizenzModel> results = new List<LizenzModel>();
            results = flow.GetMatchingBranch();

            Dictionary<string, string> resultsDictionary =
               results.ToDictionary(k => k.mpehotel.ToString(), v => v.hotel.ToString());

            return Request.CreateResponse(HttpStatusCode.OK, resultsDictionary);
        }

        [HttpGet, Route("GetMatchingCurrency/{settingsfile?}")]
        public HttpResponseMessage GetMatchingCurrency(string settingsfile = "")
        {
            //1. Get settings
            SettingsModel settings = GetSettings();
            CurrencyFlows flow = new CurrencyFlows(settings);
            List<CurrencyModel> results = new List<CurrencyModel>();
            results = flow.GetCurrency();

            Dictionary<string, string> resultsDictionary =
               results.ToDictionary(k => k.Ref.ToString(), v => v.Name.ToString());

            return Request.CreateResponse(HttpStatusCode.OK, resultsDictionary);
        }

        [HttpPost, Route("PostDataToPylon")]
        public HttpResponseMessage PostDataToPylon(eaDateModel model)
        {
            SettingsModel settings = GetSettings();

            CreateTableFlows createTableFlows = new CreateTableFlows(settings);

            createTableFlows.CreateTable();

            PostDataToPylonFlows flow = new PostDataToPylonFlows(settings);

            List<PostDataToPylonModel> results = new List<PostDataToPylonModel>();

            results = flow.PostDataToPylon(model);

            return Request.CreateResponse(HttpStatusCode.OK, results);
        }

        [HttpPost, Route("PostMCDataToPylon")]
        public HttpResponseMessage PostMCDataToPylon(eaDateModel model)
        {
            SettingsModel settings = GetSettings();

            CreateTableFlows createTableFlows = new CreateTableFlows(settings);

            createTableFlows.CreateMCTable();

            PostDataToPylonFlows flow = new PostDataToPylonFlows(settings);

            List<MCDataModel> results = new List<MCDataModel>();

            results = flow.PostMCDataToPylon(model);

            return Request.CreateResponse(HttpStatusCode.OK, results);
        }

        [HttpPost, Route("UpdateFibudebToPylonCustomerId")]
        public HttpResponseMessage UpdateFibudebToPylonCustomerId(List<string> args)
        {
            //1. Get settings
            SettingsModel settings = GetSettings();
            KundenFlows flow = new KundenFlows(settings);
            bool results;
            results = flow.UpdateFibudebToPylonCustomerId(args[0], args[1]);


            return Request.CreateResponse(HttpStatusCode.OK, results);
        }

        [HttpGet, Route("UpdateFibudebToPylonCustomerId/{ProtelId}/ProtelId/{PylonId}/PylonId")]
        public HttpResponseMessage UpdateFibudebToPylonCustomerId(string ProtelId, string PylonId)
        {
            //1. Get settings
            SettingsModel settings = GetSettings();
            KundenFlows flow = new KundenFlows(settings);
            bool results;
            results = flow.UpdateFibudebToPylonCustomerId(ProtelId, PylonId);


            return Request.CreateResponse(HttpStatusCode.OK, results);
        }


        [HttpGet, Route("DeleteSentRecords/{deletesentrecords}")]
        public HttpResponseMessage DeleteSentRecords(string deletesentrecords)
        {
            //1. Get settings
            SettingsModel settings = GetSettings();
            HitToPylonDocsFlows flow = new HitToPylonDocsFlows(settings);
            List<string> listtobedeleted = JsonConvert.DeserializeObject<List<string>>(deletesentrecords);
            flow.DeleteSentRecords(listtobedeleted);


            return Request.CreateResponse(HttpStatusCode.OK);
        }
        [HttpPost, Route("GetDocsForSpecifiedRange")]
        public HttpResponseMessage GetDocsForSpecifiedRange(eaDateModel model)
        {
            SettingsModel settings = GetSettings();
            PostDataToPylonFlows flow = new PostDataToPylonFlows(settings);
            flow.GetDocsForSpecifiedRange(model);

            return Request.CreateResponse(HttpStatusCode.OK);
        }
        [HttpGet, Route("GetSalesJournalAA")]
        public HttpResponseMessage GetSalesJournalAA()
        {
            SettingsModel settings = GetSettings();
            PostDataToPylonFlows flow = new PostDataToPylonFlows(settings);
            List<SalesJournalAAModel> res = new List<SalesJournalAAModel>();
            res = flow.GetsalesJournalAA();
            return Request.CreateResponse(HttpStatusCode.OK, res);
        }
        [HttpGet, Route("GetsalesJournalAR")]
        public HttpResponseMessage  GetsalesJournalAR()
        {
            SettingsModel settings = GetSettings();
            PostDataToPylonFlows flow = new PostDataToPylonFlows(settings);
            List<eaSalesJournalARModel> res = new List<eaSalesJournalARModel>();
            res = flow.GetsalesJournalAR();
            return Request.CreateResponse(HttpStatusCode.OK, res);
        }
      

        [HttpPost, Route("FlagSentRecords/")]
        public HttpResponseMessage FlagSentRecords(List<eaMessagesStatusModel> json)
        {
            //1. Get settings

            SettingsModel settings = GetSettings();
            PostDataToPylonFlows flow = new PostDataToPylonFlows(settings);
            flow.FlagSentRecords(json);

            return Request.CreateResponse(HttpStatusCode.OK);
        }


        [HttpPost, Route("SaveSalesJournalToTemporaryTables/")]
        public HttpResponseMessage Save_SalesJournalDepositOut_To_hit_estia_sales_journal_deposit_out(eaDateModel model)
        {
            //1. Get settings

            SettingsModel settings = GetSettings();
            PostDataToPylonFlows flow = new PostDataToPylonFlows(settings);
            flow.Save_SalesJournalDepositOut_To_hit_estia_sales_journal_deposit_out(model);

            return Request.CreateResponse(HttpStatusCode.OK);
        }


        [HttpGet, Route("GetMainCourante/{Size}")]
        public HttpResponseMessage GetMainCourante(string Size)
        {
            //1. Get settings

            SettingsModel settings = GetSettings();
            PostDataToPylonFlows flow = new PostDataToPylonFlows(settings);
            flow.GetMainCourante();

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        
        [HttpPost, Route("GetSalesJournalAADateRange")]
        public HttpResponseMessage GetSalesJournalAADateRange(eaDateModel model)
        {
            SettingsModel settings = GetSettings();
            PostDataToPylonFlows flow = new PostDataToPylonFlows(settings);
            List<SalesJournalAAMain> res = new List<SalesJournalAAMain>();
            res = flow.GetSalesJournalAADateRange( model);
            return Request.CreateResponse(HttpStatusCode.OK, res);
        }

        [HttpPost, Route("GetDebtorsSalesJournalAR")]
        public HttpResponseMessage GetDebtorsSalesJournalAR(eaDateModel model)
        {
            SettingsModel settings = GetSettings();
            PostDataToPylonFlows flow = new PostDataToPylonFlows(settings);
            List<eaSalesJournalARMain> res = new List<eaSalesJournalARMain>();
            res = flow.GetDebtorsSalesJournalAR(model);
            return Request.CreateResponse(HttpStatusCode.OK, res);
        }
        
        [HttpPost, Route("GetCustomers")]
        public HttpResponseMessage GetCustomers(eaDateModel model)
        {
            SettingsModel settings = GetSettings();

            PostDataToPylonFlows flow = new PostDataToPylonFlows(settings);

            List<KundenModel> res = new List<KundenModel>();
            
            res = flow.GetCustomers(model);

            return Request.CreateResponse(HttpStatusCode.OK, res);
        }

    }
}