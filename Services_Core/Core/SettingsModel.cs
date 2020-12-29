using Hit.Services.Models.CustomAnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hit.Services.Core
{
    public class SettingsModel
    {
        #region "General"
        /// <summary>
        /// Full class name to get the annotations for active fields.
        /// 
        /// </summary>
        [Setting(Region = "General", Lebel = "Class Name", Help = "Job's or Controller's full class name", Order = 1, Width = 650)]
        public string FullClassName { get; set; }

        [Setting(Region = "General", Lebel = "Type          ", Help = "Type: 'Job' or 'Controller'", Order = 2, Width = 650)]
        public string ClassType { get; set; }

        /// <summary>
        /// Contain the JobId or the Controller full class name (hidden)
        /// </summary>
        [Setting(Region = "General", Lebel = "Settings    ", Help = "SettingsFile", Width = 650, Order = 3)]
        public string SettingsFile { get; set; }

        [Setting(Region = "General", Lebel = "Description", Help = "Class Description", Order = 4, Width = 650,Height =420)]
        public string ClassDescription { get; set; }

       
        

        #endregion


        #region "Connection strings"
        /// <summary>
        /// ProtelDB connection string 
        /// </summary>
        [Setting(Region = "DBs", Lebel = "Protel DB", Help = "Connection string for Protel DB", Order = 1, Width = 500)]
        public string ProtelDB { get; set; } = "server = <SERVER>; user id = <USER>; password = <PASSWORD>; database = protel;";

        /// <summary>
        /// WebPos DB connection string 
        /// </summary>
        [Setting(Region = "DBs", Lebel = "WebPos DB", Help = "Connection string for WebPos DB", Order = 2, Width = 500)]
        public string WebPosDB { get; set; } = "server = <SERVER>; user id = <USER>; password = <PASSWORD>; database = <DB>;";

        /// <summary>
        /// HitPos DB connection string 
        /// </summary>
        [Setting(Region = "DBs", Lebel = "HitPos DB", Help = "Connection string for HitPos DB", Order = 3, Width = 500)]
        public string HitPosDB { get; set; } = "server = <SERVER>; user id = <USER>; password = <PASSWORD>; database = <DB>;";

        /// <summary>
        /// CustomDB1 connection string 
        /// </summary>
        [Setting(Region = "DBs", Lebel = "Custom DB 1", Help = "Connection string for a DB", Order = 4, Width = 500)]
        public string Custom1DB { get; set; } = "server = <SERVER>; user id = <USER>; password = <PASSWORD>; database = <DB>;";

        /// <summary>
        /// CustomDB2 connection string 
        /// </summary>
        [Setting(Region = "DBs", Lebel = "Custom DB 2", Help = "Connection string for a DB", Order = 5, Width = 500)]
       public string Custom2DB { get; set; } = "server = <SERVER>; user id = <USER>; password = <PASSWORD>; database = <DB>;";

        /// <summary>
        /// CustomDB2 connection string 
        /// </summary>
        [Setting(Region = "DBs", Lebel = "Source DB      ", Help = "Connection string for the Source DB", Order = 6, Width = 500)]
        public string SourceDB { get; set; } = "server = <SERVER>; user id = <USER>; password = <PASSWORD>; database = <DB>;";

        /// <summary>
        /// CustomDB2 connection string 
        /// </summary>
        [Setting(Region = "DBs", Lebel = "Destination DB", Help = "Connection string for the Destination DB", Order = 7, Width = 500)]
        public string DestinationDB { get; set; } = "server = <SERVER>; user id = <USER>; password = <PASSWORD>; database = <DB>;";


        /// <summary>
        /// Smart In DB connection string 
        /// </summary>
        [Setting(Region = "DBs", Lebel = "SmartInElock DB", Help = "Connection string for SmartInElock DB", Order = 8, Width = 500)]
        public string SmartInElockDB { get; set; } = "server = <SERVER>; user id = <USER>; password = <PASSWORD>; database = <DB>;";


        /// <summary>
        /// Table name
        /// </summary>
        [Setting(Region = "DBs", Lebel = "Source Table Name      ", Help = "Source Table Name", Order = 80, Width = 150)]
        public string SourceDBTableName { get; set; }

        [Setting(Region = "DBs", Lebel = "Destination Table Name", Help = "Destination Table Name", Order = 81, Width = 150)]
        public string DestinationDBTableName { get; set; }

        /// <summary>
        /// Type of operation: 0: Insert & Update data, 1: Insert data only, 2: Update data only
        /// </summary>
        [Setting(Region = "DBs", Lebel = "Operation                  ", Help = "Type of operation: Insert data only, Update data only, Insert & Update data",Values ="Inserts & Updates,Inserts only ,Updates only", Order = 85, Width = 150)]
        public int DBOperation { get; set; }

        /// <summary>
        /// Transaction
        /// </summary>
        [Setting(Region = "DBs", Lebel = "Transaction                ", Help = "Check to use Transaction", Order = 86)]
        public bool DBTransaction { get; set; }


        /// <summary>
        /// Transaction
        /// </summary>
        [Setting(Region = "DBs", Lebel = "Timeout in sec             ", Help = "DB connection timeout in sec", Order = 87, Values = "10,20,30,40,50,60,90,120,150,180,250,300,420,600,1200,2000,5000")]
        public string DBTimeout { get; set; } = "60";
        #endregion


        #region "Protel"
        /// <summary>
        /// Protel's hotel's Id
        /// </summary>
        [Setting(Region = "Protel", Lebel = "Hotel Id", Help = "The hotel id for multiproperty", Order = 1, Width = 120)]
        public string MpeHotel { get; set; }

        /// <summary>
        /// Protel User Name
        /// </summary>
        [Setting(Region = "Protel", Lebel = "Protel User", Help = "Name for proteluser", Order = 0, Width = 120)]
        public string ProtelUser { get; set; }

        /// <summary>
        /// List Of Travel Agents to get Rate Codes
        /// </summary>
        [Setting(Region = "Protel", Lebel = "Travel Agents (IDs)", Help = "List of travel agents ids", Order = 2, Width = 200, Height = 200)]
        public List<long?> RateCodeAgentIds { get; set; }

        /// <summary>
        /// 1=> Only Web Rate Codes
        /// 0=> Not Web Rate Codes
        /// 2=> All Rate Codes
        /// </summary>
        [Setting(Region = "Protel", Lebel = "Visible on Web", Help = "If selected data are visible on WEB", Values = "No,Yes,All", Order = 3, Width = 120)]
        public Nullable<int> WebRateCodeType { get; set; }

        /// <summary>
        /// Name for Caller to get availability (ex. WebHotelier, Mouzenidis, Availabilities.com ....)
        /// </summary>
        [Setting(Region = "Protel", Lebel = "Customer Name", Help = "The name who call to get the availability", Order = 4, Width = 150)]
        public string CustomerName { get; set; }


        [Setting(Region = "Protel", Lebel = "Match Type to Room        ", Help = "Match Types (1st column) to Rooms (2nd column)", Order = 5, Width = 350, Height = 200)]
        public Dictionary<string, string> ProtelTypeToRoom { get; set; }

        [Setting(Region = "Protel", Lebel = "Match Type to Department", Help = "Match Types (1st column) to Departments (2nd column)", Order = 6, Width = 350, Height = 200)]
        public Dictionary<string, string> ProtelTypeToDepartment { get; set; }

        /// <summary>
        /// Delete old records
        /// </summary>
        [Setting(Region = "Protel", Lebel = "Days To Delete", Help = "Delete old records ", Order = 7, Width = 80)]
        public int ProtelDaysToDelete { get; set; } = 730;

        /// <summary>
        /// Virual Rooms
        /// </summary>
        [Setting(Region = "Protel", Lebel = "Virual Rooms", Help = "List of virtual Room Numbers ", Order = 8, Width = 100)]
        public List<string> ProtelVirualRooms { get; set; }

        /// <summary>
        /// Protel's male description (restyp.bezeich)
        /// </summary>
        [Setting(Region = "Protel", Lebel = "Male Descr", Help = "Protel's male description (restyp.bezeich)", Order = 9, Width = 120)]
        public string ProtelMaleDesc { get; set; }
        /// <summary>
        /// Protel's female description (restyp.bezeich)
        /// </summary>
        [Setting(Region = "Protel", Lebel = "Female Descr", Help = "Protel's female description (restyp.bezeich)", Order = 10, Width = 120)]
        public string ProtelFemaleDesc { get; set; }

        #endregion


        #region "Api client"

        /// <summary>
        /// api url to call
        /// </summary>
        [Setting(Region = "Api client", Lebel = "Api Url                   ", Help = "Api Url (Full or Basic) to call", Width = 400)]
        public string RestServerUrl { get; set; }

        /// <summary>
        /// Authentication Header . Format: Username:Password  
        /// </summary>
        [Setting(Region = "Api client", Lebel = "Authentication        ", Help = "Authentication Header.  Format for Basic: 'Username:Password',  Format for OAuth2: 'Bearer  ZTdmZmY1Zjc5MTQ4NDQ5ZTEzMzIyZTOQ'", Width = 250)]
        public string RestServerAuthenticationHeader { get; set; }

        /// <summary>
        /// Authentication Type  (Basic or OAuth2)
        /// </summary>
        [Setting(Region = "Api client", Lebel = "Authentication Type", Help = "If Authentication is filled then 'Authentication Type' is required, otherwise the value of 'Authentication Type' is ignored", Values = " ,Basic,OAuth2", Width = 100)]
        public string RestServerAuthenticationType { get; set; } = "Basic";

        /// <summary>
        /// HttpMethod: Get,Post  
        /// </summary>
        [Setting(Region = "Api client", Lebel = "Http Method           ", Help = "Http Method : Get or Post",Values ="GET,POST", Width = 100)]
        public string RestServerHttpMethod { get; set; }

        /// <summary>
        /// mediaType: application/json or application/xml 
        /// </summary>
        [Setting(Region = "Api client", Lebel = "Media Type            ", Help = "Media Type", Values = "application/json,application/xml ", Width = 150)]
        public string RestServerMediaType { get; set; }

        /// <summary>
        /// CustomHeaders the Rest Server may require
        /// </summary>
        [Setting(Region = "Api client", Lebel = "Custom Headers      ", Help = "Custom Headers for the Rest Call", Width = 250, Height = 200)]
        public Dictionary<string,string> RestServerCustomHeaders { get; set; }

        /// <summary>
        /// URL Parameters
        /// </summary>
        [Setting(Region = "Api client", Lebel = "URL Parameters       ", Help = "URL Parameters", Width = 250, Height = 200)]
        public Dictionary<string, string> RestServerURLParameters { get; set; }
        #endregion


        #region "Dates - Durations"
        /// <summary>
        /// Date From
        /// </summary>
        [Setting(Region = "Dates - Durations", Lebel = "From Date", Help = "From Date if empty then current", Width = 80)]
        public Nullable<DateTime> DateFrom { get; set; }

        /// <summary>
        /// Date To
        /// </summary>
        [Setting(Region = "Dates - Durations", Lebel = "To Date", Help = "To Date", Width = 80)]
        public Nullable<DateTime> DateTo { get; set; }


        /// <summary>
        /// Duration
        /// </summary>
        [Setting(Region = "Dates - Durations", Lebel = "Duration", Help = "Days after the From Date", Width = 60)]
        public Nullable<int> Duration { get; set; }
        #endregion


        #region "SQL Script"
        /// <summary>
        /// Sql Script Path to execute
        /// </summary>
        [Setting(Region = "SQL Script", Lebel = "SQL Script Name                 ", Help = "Sql Script  to execute", Width = 400)]
        public string SqlScript { get; set; }

        [Setting(Region = "SQL Script", Lebel = "Sql Params and initial Values", Help = "Parameters and their inital values required by the SqlScript. ex: @Id=50, @StartDate=2018-03-21 . (Keys must start with @ and have to accord with sql script's parameters)", Width = 400, Height = 200)]
        public Dictionary<string, string> SqlParameters { get; set; } = new Dictionary<string, string>();


        [Setting(Region = "SQL Script", Lebel = "Destination SQL Pre-Insert/Update Script Name", Help = "Sql Script to Run into destination DB before the inserts/updates.", Width = 400)]
        public string SqlDestPreScript { get; set; }
        #endregion


        #region "Various Parameters"

        [Setting(Region = "Various Parameters", Lebel = "Parameters", Help = "Parameters for custom usage. See Job's description", Width = 500, Height = 400)]
        public Dictionary<string, string> Parameters { get; set; }

        #endregion


        #region "Export To File"

        /// <summary>
        /// CultureInfo: ex: el-GR, en-us or null
        /// </summary>
        [Setting(Region = "Export To File", Lebel = "Culture Info", Help = "Culture Info: Choose a Culture or keep it empty for invariant culture", Values = ",en-US,en-GB,fr-FR,de-DE,el-GR,es-ES,ru-RU,mk-MK,sq-AL", Width = 100)]
        public string CultureInfo { get; set; } = "en-us";

        /// <summary>
        /// Formatter for every column. Key: the name of column, Value: format ex: yyyy-MM-dd HH:mm:ss (2018-12-31 23:10:20), F2 (1230.12), N2 (1,230.12) 
        /// </summary>
        [Setting(Region = "Export To File", Lebel = "Formaters", Help = "Formatter for every column. Key: the name of column, Value: format ex: yyyy-MM-dd HH:mm:ss (2018-12-31 23:10:20), F2 (1230.12), N2 (1,230.12)",  Width = 300, Height = 200)]
        public Dictionary<string,string>  Formater { get; set; }

        /// <summary>
        /// TimeStampt's format (ex:yyyyMMddHHmm). Is replaces the &lt;TIMESTAMP&#62; into files paths
        /// </summary>
        [Setting(Region = "Export To File", Lebel = "TimeStamp ", Help = "TimeStamp replaces the <TIMESTAMP> into file paths. TimeStamp must have DateTime format ex : yyyyMMddHHmm", Width = 150)]
        public string TimeStamp { get; set; } = "yyyyMMddHHmmss";

        /// <summary>
        /// Path for the xml path. (if path contains &lt;TIMESTAMP&#62; then this is replased by current timespan )
        /// </summary>
        [Setting(Region = "Xml", Lebel = "Xml File Path", Help = "Path for the exported xml file.  If path contains <TIMESTAMP> then this is replased by current timespan (to export only)", Order = 1, Width = 600)]
        public string XmlFilePath { get; set; }//(if path contains <TIMESTAMP> then this is replased by current timespan )

        /// <summary>
        /// RootElement for the xml  (ex: Receipts)
        /// </summary>
        [Setting(Region = "Xml", Lebel = "Root Element", Help = "RootElement for the xml  (ex: Receipts)", Order = 2, Width = 80)]
        public string XmlRootElement { get; set; }


        /// <summary>
        /// Every-Line-Element for the xml(ex: Receipt)
        /// </summary>
        [Setting(Region = "Xml", Lebel = "Xml Element", Help = "Every-Line-Element for the xml(ex: Receipt)",Order =3, Width = 80)]
        public string XmlElement { get; set; }


        /// <summary>
        /// Path for the json path. (if path contains &lt;TIMESTAMP&#62; then this is replased by current timespan )
        /// </summary>
        [Setting(Region = "Json", Lebel = "Json Path", Help = "Path for the json path. If path contains <TIMESTAMP> then this is replased by current timespan (to export only)", Width = 600)]
        public string JsonFilePath { get; set; }//(if path contains <TIMESTAMP> then this is replased by current timespan )


        /// <summary>
        /// Path for the csv path. (if path contains &lt;TIMESTAMP&#62; then this is replased by current timespan )
        /// </summary>
        [Setting(Region = "Csv", Lebel = "Csv Path", Help = "Path for the csv path.  If path contains <TIMESTAMP> then this is replased by current timespan (to export only)", Order = 1, Width = 600)]
        public string CsvFilePath { get; set; }//(if path contains <TIMESTAMP> then this is replased by current timespan )


        /// <summary>
        /// If checked then the 1st line of the file will be header.
        /// </summary>
        [Setting(Region = "Csv", Lebel = "File Header", Help = "If checked then the 1st line of the file will be header.", Order = 2, Width = 40)]
        public bool? CsvFileHeader { get; set; } = true;

        /// <summary>
        /// List of header's names. If CsvFileHeader is unchecked then headers MUST be declered manually in CsvFileHeaders.
        /// </summary>
        [Setting(Region = "Csv", Lebel = "File Headers", Help = "If 'File Header' is unchecked then header MUST be declared here.", Order = 3, Width = 160,Height =250)]
        public List<string> CsvFileHeaders { get; set; }

        /// <summary>
        /// Delimeter (ex: ;,- or tab)
        /// </summary>
        [Setting(Region = "Csv", Lebel = "Csv Delimeter", Help = "Delimeter (ex: ;,- or tab)",Values = ";,Comma,Space,Tab,-,@,#,$,^,~", Order = 4, Width = 70)]
        public string CsvDelimenter { get; set; } = ";";

        [Setting(Region = "Csv", Lebel = "Csv Encoding", Help = "File's Encoding", Values = "UTF8,ASCII,Unicode,UTF32,UTF7,BigEndianUnicode", Order = 5, Width = 140)]
        public string CsvEncoding { get; set; } = ";";


        /// <summary>
        /// Path for the FixedLenght path. (if path contains &lt;TIMESTAMP&#62; then this is replased by current timespan )
        /// </summary>
        [Setting(Region = "Fixed lenght", Lebel = "Fixed Lenght Path", Help = "Path for the FixedLenght path.  If path contains <TIMESTAMP> then this is replased by current timespan (to export only)", Order = 1, Width = 600)]
        public string FixedLenghtFilePath { get; set; }//(if path contains <TIMESTAMP> then this is replased by current timespan )


        /// <summary>
        /// If checked then the 1st line of the file will be header.
        /// </summary>
        [Setting(Region = "Fixed lenght", Lebel = "File Header", Help = "If checked then the 1st line of the file will be header.", Width = 40)]
        public bool? FixedLenghtFileHeader { get; set; } = true;

        /// <summary>
        /// If checked then columns are aligned to the right.
        /// </summary>
        [Setting(Region = "Fixed lenght", Lebel = "Align Right", Help = "If checked then columns are aligned to the right.", Width = 40)]
        public bool? FixedLenghtAlignRight { get; set; } = true;

        /// <summary>
        /// Lengths for every column (ex: 10,20,8,23)
        /// </summary>
        [Setting(Region = "Fixed lenght", Lebel = "Columns Length", Help = "Lengths for every column (ex: 10,20,8,23)", Width = 300, Height = 200)]
        public List<int?> FixedLengths { get; set; }



        /// <summary>
        /// Path for the Html path. (if path contains &lt;TIMESTAMP&#62; then this is replased by current timespan )
        /// </summary>
        [Setting(Region = "Html", Lebel = "Html Path", Help = "Path for the Html path.  If path contains <TIMESTAMP> then this is replased by current timespan (to export only)", Order = 1, Width = 600)]
        public string HtmlFilePath { get; set; }//(if path contains <TIMESTAMP> then this is replased by current timespan )


        /// <summary>
        /// If checked then the table will contain header.
        /// </summary>
        [Setting(Region = "Html", Lebel = "Html Header", Help = "If checked then the table will contain header.", Width = 40)]
        public bool? HtmlHeader { get; set; } = true;

        /// <summary>
        /// Html's title
        /// </summary>
        [Setting(Region = "Html", Lebel = "Html Title", Help = "Html's title", Width = 150)]
        public string HtmlTitle { get; set; }

        /// <summary>
        /// css for html
        /// </summary>
        [Setting(Region = "Html", Lebel = "Sort Rows", Help = "Allow user to sort columns", Width = 120)]
        public bool HtmlSortRows { get; set; } = true;

        /// <summary>
        /// css for html
        /// </summary>
        [Setting(Region = "Html", Lebel = "CSS", Help = "css", Width = 500,Height =500)]
        public string Htmlcss { get; set; }= @"
body{
    padding: 0; 
    border: 0; 
    margin: 0;
}
#hittable {
    font-family:  ""Trebuchet MS"",Arial, Helvetica, sans-serif;
    border - collapse: collapse;
    width: 100 %;
    padding: 0; 
    margin: 0;
   }

#hittable td, #hittable th {
        border: 1px solid #ddd;
        padding: 8px;
  }

#hittable tr:nth-child(even){background-color: #f2f2f2;}

#hittable tr:hover {background-color: #ddd;}

#hittable th {
    padding-top: 12px;
    padding-bottom: 12px;
    text-align: left;
    background-color: #4CAF50;
    color: white;
}";

        /// <summary>
        /// Path for the Pdf path. (if path contains &lt;TIMESTAMP&#62; then this is replased by current timespan )
        /// </summary>
        [Setting(Region = "Pdf", Lebel = "Pdf Path", Help = "Path for the Pdf path.  If path contains <TIMESTAMP> then this is replased by current timespan (to export only)", Order = 1, Width = 600)]
        public string PdfFilePath { get; set; }//(if path contains <TIMESTAMP> then this is replased by current timespan )

        /// <summary>
        /// Html's title
        /// </summary>
        [Setting(Region = "Pdf", Lebel = "Pdf Title", Help = "Pdf's title", Width = 150)]
        public string PdfTitle { get; set; }

        /// <summary>
        /// css for html
        /// </summary>
        [Setting(Region = "Pdf", Lebel = "CSS", Help = "css", Width = 500, Height = 500)]
        public string Pdfcss { get; set; }

        #endregion



        #region "File Info"
        /// <summary>
        /// (Source/Target) Directory
        /// </summary>
        [Setting(Region = "File Info", Lebel = "Directory", Help = "File's path", Width = 500)]
        public string FileInfoDirectory { get; set; }

        /// <summary>
        /// (Source/Target) File(s). Optionaly may contain wild card characters, ex: *.txt
        /// </summary>
        [Setting(Region = "File Info", Lebel = "FileName", Help = "FileName. Optionaly may contain wild card characters, ex: *.txt", Width = 500)]
        public string FileInfoFileName { get; set; }

        /// <summary>
        /// If in the file a line starts with this character then this line is considered as comments and it will NOT be included into the returned list of lines
        /// </summary>
        [Setting(Region = "File Info", Lebel = "Comments Char", Help = "If a line starts with this character then this line is considered as comments and it will NOT be included into the returned list of lines", Width = 15)]
        public string FileInfoCommentsChar { get; set; }

        #endregion


        #region "Forex - fixer.io"
        /// <summary>
        /// Currency From
        /// </summary>
        [Setting(Region = "Forex - fixer.io", Lebel = "Currency From", Help = "Το βασικό νόμισμα για το οποίο λαμβάνονται τα rates (EUR)", Order = 1, Width = 80)]
        public string ForexFixerFrom { get; set; } = "EUR";
        /// <summary>
        /// List of Currencies (ex: USD,AUD,CAD,PLN,MXN
        /// </summary>
        [Setting(Region = "Forex - fixer.io", Lebel = "Currencies To", Help = "Λίστα με τα νομίσματα των rates. Πχ: USD,AUD,CAD,PLN,MXN", Order = 2, Width = 400)]
        public string ForexFixerTo { get; set; }
        /// <summary>
        /// Every time Service runs delete old rates first
        /// </summary>
        [Setting(Region = "Forex - fixer.io", Lebel = "Delete Old Rates", Help = "Διαγραφή των υπαρχόντων rates στην DB πριν ληφθούν τα νέα rates", Order = 3, Width = 100)]
        public bool ForexFixerDeleteOld { get; set; } = true;
        /// <summary>
        /// Api Key given by fixer.io
        /// </summary>
        [Setting(Region = "Forex - fixer.io", Lebel = "Api Key", Help = "Api Key given by fixer.io", Order = 4, Width = 100)]
        public string ForexFixerApiKey { get; set; }
        /// <summary>
        /// Base Url
        /// </summary>
        [Setting(Region = "Forex - fixer.io", Lebel = "Base Url", Help = "Base Url. ex: http://data.fixer.io/api/latest", Order = 4, Width = 200)]
        public string ForexFixerBaseUrl { get; set; } = "http://data.fixer.io/api/latest";

        #endregion


        #region "Email Settings"

        /// <summary>
        /// EmailFrom
        /// </summary>
        [Setting(Region = "Email Settings", Lebel = "From", Help = "From email address", Order = 1, Width = 200)]
        public string EmailFrom { get; set; }


        /// <summary>
        /// EmailTo
        /// </summary>
        [Setting(Region = "Email Settings", Lebel = "To", Help = "To email list of addresses", Order = 2,  Width = 200, Height = 100)]
        public List<string> EmailTo { get; set; }

        /// <summary>
        /// Email subject
        /// </summary>
        [Setting(Region = "Email Settings", Lebel = "Subject", Help = "Email Subject", Order = 3, Width = 500)]
        public string EmailSubject { get; set; }

        /// <summary>
        /// Email body
        /// </summary>
        [Setting(Region = "Email Settings", Lebel = "Body", Help = "Email Body", Order = 4, Width = 500, Height = 200)]
        public string EmailBody { get; set; }

        /// <summary>
        /// true: on email failure throw exception
        /// </summary>
        [Setting(Region = "Email Settings", Lebel = "Failure", Help = "If sending email process failed, mark job also failed", Order = 5)]
        public bool EmailThrowException { get; set; } = true;

        /// <summary>
        /// smtp
        /// </summary>
        [Setting(Region = "Email Settings", Lebel = "SMTP", Help = "SMTP", Order = 5, Width = 400)]
        public string EmailSmtp { get; set; }

        [Setting(Region = "Email Settings", Lebel = "Port", Help = "SMTP Port", Order = 6, Width = 400)]
        public int EmailPort { get; set; }

        [Setting(Region = "Email Settings", Lebel = "Username", Help = "SMTP username", Order = 7, Width = 90)]
        public string EmailUsername { get; set; }

        [Setting(Region = "Email Settings", Lebel = "Password", Help = "SMTP password", Order = 8, Width = 400)]
        public string EmailPassword { get; set; }

        [Setting(Region = "Email Settings", Lebel = "SSL", Help = "SMTP support ssl", Order = 9, Width = 400)]
        public bool EmailSsl { get; set; }

        #endregion


        #region "Error Email Settings"

        /// <summary>
        /// Email From
        /// </summary>
        [Setting(Region = "Error Email Settings", Lebel = "From  ", Help = "From email address", Order = 1, Width = 200)]
        public string ErrorEmailFrom { get; set; }
        /// <summary>
        /// EmailTo
        /// </summary>
        [Setting(Region = "Error Email Settings", Lebel = "To     ", Help = "To email list of addresses", Order = 2, Width = 200, Height = 120)]
        public List<string> ErrorEmailTo { get; set; }
        /// <summary>
        /// smtp
        /// </summary>
        [Setting(Region = "Error Email Settings", Lebel = "SMTP", Help = "SMTP", Order = 5, Width = 100)]
        public string ErrorEmailSmtp { get; set; }

        [Setting(Region = "Error Email Settings", Lebel = "Port    ", Help = "SMTP Port", Order = 6, Width = 100)]
        public int ErrorEmailPort { get; set; }

        [Setting(Region = "Error Email Settings", Lebel = "Username", Help = "SMTP username", Order = 7, Width = 120)]
        public string ErrorEmailUsername { get; set; }

        [Setting(Region = "Error Email Settings", Lebel = "Password ", Help = "SMTP password", Order = 8, Width = 120)]
        public string ErrorEmailPassword { get; set; }

        [Setting(Region = "Error Email Settings", Lebel = "SSL", Help = "SMTP support ssl", Order = 9)]
        public bool ErrorEmailSsl { get; set; }

        #endregion



        #region "Helper Methods - Never use in win form"

        /// <summary>
        /// Encoding (Helper Method)
        /// </summary>
        public Encoding Encoding { get; set; }

        #endregion
    }
}
