using Hit.Services.Core;
using Hit.Services.DataAccess.DT.Sql;
using Hit.Services.Helpers.Classes;
using Hit.Services.Helpers.Classes.Classes;
using Hit.Services.MainLogic.Tasks.SQLJobs;
using Hit.Services.Models.Models;
using Hit.Services.Models.Models.SQL;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Hit.Services.MainLogic.Tasks.SQLJobs
{

    /// <summary>
    /// Class that exports data to a File or to a Rest Server (web api) or to a DB Table
    /// </summary>
   public class ExportDataTasks
    {
        SqlConstructorHelper sqlConstruct;
        ConvertDataHelper convertDataHelper;
        FileHelpers fileHelpers;
        SettingsModel settings;
        SQLTasks sqlTasks;
        GenericDT genericDT;
        ConvertDynamicHelper dynamicCast;

        /// <summary>
        ///Class that exports data to a File or to a Rest Server (web api) or to a DB Table
        /// </summary>
        /// <param name="settings"></param>
        public ExportDataTasks(SettingsModel settings)
        {
            sqlConstruct = new SqlConstructorHelper();
            fileHelpers = new FileHelpers();
            sqlTasks = new SQLTasks(settings);
            genericDT = new GenericDT();
            dynamicCast = new ConvertDynamicHelper();
            this.settings = settings;
        }

        /// <summary>
        /// Export Data to file (xml, csv, fixes length, json, html) or post data to rest server 
        /// </summary>
        /// <param name="rawData"></param>
        /// <param name="extension">the ending part of url (not included to settings.RestServerUrl)</param>
        /// <returns>for the RestServer call, return the Servers's response message</returns>
        public string ExportData(dynamic rawData,string extension="")
        {
            //1. if export to file is needed then Convert Data to List<IDictionary<string, dynamic>>
            List<IDictionary<string, dynamic>> dictionary = null;

            if (!String.IsNullOrEmpty(settings.CsvFilePath) ||
                !String.IsNullOrEmpty(settings.HtmlFilePath) ||
                !String.IsNullOrEmpty(settings.PdfFilePath) ||
                !String.IsNullOrEmpty(settings.JsonFilePath) ||
                !String.IsNullOrEmpty(settings.XmlFilePath) ||
                !String.IsNullOrEmpty(settings.FixedLenghtFilePath)
                )
            {
                dictionary = dynamicCast.ToListDictionary(rawData);
            }

            //2. export to file or to rest server
            if (!String.IsNullOrEmpty(settings.CsvFilePath))
            {
                ToCsv(dictionary);
            }
            if (!String.IsNullOrEmpty(settings.HtmlFilePath))
            {
                ToHtml(dictionary);
            }
            if (!String.IsNullOrEmpty(settings.PdfFilePath))
            {
                ToPdf(dictionary);
            }
            if (!String.IsNullOrEmpty(settings.JsonFilePath))
            {
                ToJson(dictionary);
            }
            if (!String.IsNullOrEmpty(settings.XmlFilePath))
            {
                ToXml(dictionary);
            }
            if (!String.IsNullOrEmpty(settings.FixedLenghtFilePath))
            {
                ToFixedLenght(dictionary);
            }
            if (!String.IsNullOrEmpty(settings.RestServerUrl))
            {
                //check if rawData are List or not
                if(isIEnumerable(rawData))
                    return  PostToRestServer<List<dynamic>>(ToList(rawData), extension); //rawData is a list
                else
                    return PostToRestServer<dynamic>(rawData, extension); //rawData is not a list
            }

            return "";
        }

        /// <summary>
        /// return true id rawData is a List
        /// </summary>
        /// <param name="rawData">Data (list or not list)</param>
        /// <returns></returns>
        public bool isIEnumerable(dynamic rawData)
        {
            var enumerable = rawData as System.Collections.IEnumerable;
            if (enumerable != null)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Convert dynamic rawData as List of dynamic. (rawData must be checked if they are list, see isIEnumerable)
        /// </summary>
        /// <param name="rawData">dynamic data</param>
        /// <returns></returns>
        public List<dynamic> ToList(dynamic rawData)
        {
            return (rawData as IEnumerable<dynamic>).ToList();
        }


        /// <summary>
        /// Write data to xml file
        /// </summary>
        /// <param name="data"></param>
        public void ToXml(List<IDictionary<string, dynamic>> data)
        {
            convertDataHelper = new ConvertDataHelper(CreateFormaters(settings));
            string xml =  convertDataHelper.ToXml(data, settings.XmlRootElement, settings.XmlElement);
            fileHelpers.WriteTextToFile(settings.XmlFilePath, xml, false, settings.TimeStamp);
            
        }


        /// <summary>
        /// Write data to json file
        /// </summary>
        /// <param name="data"></param>
        public void ToJson(List<IDictionary<string, dynamic>> data)
        {
            convertDataHelper = new ConvertDataHelper(CreateFormaters(settings));
            string json = convertDataHelper.ToJson(data);
            fileHelpers.WriteTextToFile(settings.JsonFilePath, json, false, settings.TimeStamp);
        }

        /// <summary>
        /// Write data to csv file
        /// </summary>
        /// <param name="data"></param>
        public void ToCsv(List<IDictionary<string, dynamic>> data)
        {
            convertDataHelper = new ConvertDataHelper(CreateFormaters(settings));
            string csv = convertDataHelper.ToCsv(data,settings.CsvFileHeader??false, settings.CsvDelimenter);
            fileHelpers.WriteTextToFile(settings.CsvFilePath, csv, false, settings.TimeStamp);
        }

        /// <summary>
        /// Write data to csv file
        /// </summary>
        /// <param name="data"></param>
        public void ToFixedLenght(List<IDictionary<string, dynamic>> data)
        {
            convertDataHelper = new ConvertDataHelper(CreateFormaters(settings));
            string fl = convertDataHelper.ToFixedLenght(data, settings.FixedLenghtFileHeader??false, settings.FixedLengths??null,settings.FixedLenghtAlignRight??false);
            fileHelpers.WriteTextToFile(settings.FixedLenghtFilePath, fl, false, settings.TimeStamp);
        }

        /// <summary>
        /// Write data to html file
        /// </summary>
        /// <param name="data"></param>
        public void ToHtml(List<IDictionary<string, dynamic>> data)
        {
            convertDataHelper = new ConvertDataHelper(CreateFormaters(settings));
            string fl = convertDataHelper.ToHtml(data, settings.HtmlHeader??false,settings.HtmlTitle, settings.HtmlSortRows, settings.Htmlcss);
            fileHelpers.WriteTextToFile(settings.HtmlFilePath, fl, false, settings.TimeStamp);
        }

        /// <summary>
        /// Write data to pdf file
        /// </summary>
        /// <param name="data"></param>
        public void ToPdf(List<IDictionary<string, dynamic>> data)
        {
            convertDataHelper = new ConvertDataHelper(CreateFormaters(settings));
            string html = convertDataHelper.ToHtml(data, settings.HtmlHeader ?? false, settings.PdfTitle,false, "");
            html = html.Replace("<style type=\"text/css\">", "");
            html = html.Replace("</style>", "");
            fileHelpers.WriteHtmlToPdf(settings.PdfFilePath, html, settings.Pdfcss, settings.TimeStamp);
        }


        /// <summary>
        /// POST Data to Rest Server
        /// </summary>
        /// <param name="data"></param>
        /// <param name="extension">the last part of url (not included to settings.RestServerUrl)</param>
        public string PostToRestServer<T>(T data, string extension )
        {
            string ErrorMsg = "";
            int returnCode = 0;
            string result = "" ;
            WebApiClientHelper webHelper = new WebApiClientHelper();

            if (settings.RestServerHttpMethod.ToUpper() == "POST")
            {
                result = webHelper.PostRequest<T>(data,
                                            constractUrl(settings.RestServerUrl, extension),
                                            settings.RestServerAuthenticationHeader,
                                            settings.RestServerCustomHeaders,
                                            out returnCode,
                                            out ErrorMsg,
                                            settings.RestServerMediaType
                                        );
            }
            else
            {
                throw new Exception("No Post Http Method has been set.");
            }
            if (returnCode != 200)
            {
                throw new Exception("Http Post Error: "+ ErrorMsg + " Code: "+ returnCode.ToString());
            }

            return result;
        }

        /// <summary>
        /// Save data to a Destination DB Table. Default Connecetion String: settings.DestinationDB
        /// </summary>
        /// <param name="rawData">data to save</param>
        /// <param name="tableinfo">destination table</param>
        /// <param name="conString">connection string. If conString=null then default Connecetion String: settings.DestinationDB</param>
        public void SaveDataToDB(dynamic rawData,DbTableModel tableinfo,string conString=null)
        {
            if (conString == null) conString = settings.DestinationDB;

            //1. convert data as List<IDictionary<string, dynamic>>
            if (rawData == null) return;
            List<IDictionary<string, dynamic>> dictionary = dynamicCast.ToListDictionary(rawData);
            if (dictionary == null) return;

            //2. Insert or Update data to a Data Table
            genericDT.SaveToTable(dictionary, conString, tableinfo, settings.DBOperation, settings.DBTransaction);
        }


        #region "Private Members"
        /// <summary>
        /// constract a final url 
        /// </summary>
        /// <param name="baseUrl"></param>
        /// <param name="extenstion"></param>
        /// <returns></returns>
        private string constractUrl(string baseUrl, string extenstion)
        {
            if (extenstion == "") return baseUrl;
            if (baseUrl.EndsWith("/") || extenstion.StartsWith("/"))
                return baseUrl + extenstion;
            else
                return baseUrl + "/"+extenstion;
        }


     

        /// <summary>
        /// Using a SettingsModel (CultureInfo, Formater) return a Dictionary(string, Formater)
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        private Dictionary<string, Formater> CreateFormaters(SettingsModel settings)
        {
            Dictionary<string, Formater> formaters = new Dictionary<string, Formater>();
            if (settings == null || settings.Formater == null) return formaters;

            foreach (string key in settings.Formater.Keys)
            {
                Formater f = new Formater();
                f.CultureInfoDescription = settings.CultureInfo;
                f.Format = settings.Formater[key];
                formaters.Add(key, f);
            }
            return formaters;
        }

        #endregion

    }
}
