using Hit.Services.Helpers.Classes.Classes;
using Hit.Services.MainLogic.Tasks.SQLJobs;
using Hit.Scheduler.Jobs.SQL;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hit.Services.Models.Models;
using Hit.Services.Core;

namespace HitServicesTest //  <----<<< IMPORTANT: All tests must have that namespace
{
    [TestFixture]
   public class ExportDataJobTest
    {

        ConfigHelper ch;
        string basePath = @"c:\logs\";

        [OneTimeSetUp]
        public void Init()
        {
            ch = new ConfigHelper();
            File.Delete(basePath + @"ExportData.xml");
            File.Delete(basePath + @"ExportData.csv");
            File.Delete(basePath + @"ExportData.json");
            File.Delete(basePath + @"ExportData.txt");
            File.Delete(basePath + @"ExportData.html");
            File.Delete(basePath + @"ExportData.pdf");
        }


        [Test, Order(1)]
        public void ExportDataJob_toXML()
        {
            //1. create settinga and script files
            string Settingsfile = "Settings.ExportData.ToXML";
            string Scriptfile = "Script.ExportData.Test1";
            createScriptFiles(Scriptfile);
            createSettingsFileForXML(Settingsfile, Scriptfile);

            //2. run job
            ExportDataJob sg = new ExportDataJob();
            Assert.DoesNotThrow(() => sg.Execute(Settingsfile, "ExportData", "TestInstallation"));

            //3. clean
            ch.DeleteScriptFile(Scriptfile);
            ch.DeleteSettingsFile(Settingsfile);
        }

        [Test, Order(2)]
        public void ExportDataJob_toJson()
        {
            //1. create settinga and script files
            string Settingsfile = "Settings.ExportData.ToJson";
            string Scriptfile = "Script.ExportData.Test1";
            createScriptFiles(Scriptfile);
            createSettingsFileForJson(Settingsfile, Scriptfile);

            //2. run job
            ExportDataJob sg = new ExportDataJob();
            Assert.DoesNotThrow(() => sg.Execute(Settingsfile, "ExportData","TestInstallation"));

            //3. clean
            ch.DeleteScriptFile(Scriptfile);
            ch.DeleteSettingsFile(Settingsfile);
        }

        [Test, Order(3)]
        public void ExportDataJob_toCsv()
        {
            //1. create settinga and script files
            string Settingsfile = "Settings.ExportData.ToCsv";
            string Scriptfile = "Script.ExportData.Test1";
            createScriptFiles(Scriptfile);
            createSettingsFileForCsv(Settingsfile, Scriptfile);

            //2. run job
            ExportDataJob sg = new ExportDataJob();
            Assert.DoesNotThrow(() => sg.Execute(Settingsfile, "ExportData", "TestInstallation"));

            //3. clean
            ch.DeleteScriptFile(Scriptfile);
            ch.DeleteSettingsFile(Settingsfile);
        }

        [Test, Order(4)]
        public void ExportDataJob_toFix()
        {
            //1. create settinga and script files
            string Settingsfile = "Settings.ExportData.ToFix";
            string Scriptfile = "Script.ExportData.Test1";
            createScriptFiles(Scriptfile);
            createSettingsFileForFix(Settingsfile, Scriptfile);

            //2. run job
            ExportDataJob sg = new ExportDataJob();
            Assert.DoesNotThrow(() => sg.Execute(Settingsfile, "ExportData", "TestInstallation"));

            //3. clean
            ch.DeleteScriptFile(Scriptfile);
            ch.DeleteSettingsFile(Settingsfile);
        }

        [Test, Order(5)]
        public void ExportDataJob_toHtml()
        {
            //1. create settinga and script files
            string Settingsfile = "Settings.ExportData.ToHtml";
            string Scriptfile = "Script.ExportData.Test1";
            createScriptFiles(Scriptfile);
            createSettingsFileForHtml(Settingsfile, Scriptfile);

            //2. run job
            ExportDataJob sg = new ExportDataJob();
            Assert.DoesNotThrow(() => sg.Execute(Settingsfile, "ExportData", "TestInstallation"));

            //3. clean
            ch.DeleteScriptFile(Scriptfile);
            ch.DeleteSettingsFile(Settingsfile);
        }

        [Test, Order(6)]
        public void ExportDataJob_toPdf()
        {
            //1. create settinga and script files
            string Settingsfile = "Settings.ExportData.ToPdf";
            string Scriptfile = "Script.ExportData.Test1";
            createScriptFiles(Scriptfile);
            createSettingsFileForPdf(Settingsfile, Scriptfile);

            //2. run job
            ExportDataJob sg = new ExportDataJob();
            Assert.DoesNotThrow(() => sg.Execute(Settingsfile, "ExportData", "TestInstallation"));

            //3. clean
            ch.DeleteScriptFile(Scriptfile);
            ch.DeleteSettingsFile(Settingsfile);
        }


        [Test, Order(7)]
        public void ExportDataJob_toRest()
        {
            //1. create settinga and script files
            string Settingsfile = "Settings.ExportData.ToRest";
            string Scriptfile = "Script.ExportData.Test1";
            createScriptFiles(Scriptfile);
            createSettingsFileForRest(Settingsfile, Scriptfile);

            //2. run job
            ExportDataJob sg = new ExportDataJob();
         //   Assert.DoesNotThrow(() => sg.Execute(Settingsfile, "ExportData")); //<----need test web api. see Settings.ExportData.ToRest file

            //3. clean
            ch.DeleteScriptFile(Scriptfile);
            ch.DeleteSettingsFile(Settingsfile);
        }

        [Test, Order(6)]
        public void ExportDataJob_MultySelect()
        {
            //1. create settinga and script files
            string Settingsfile = "Settings.ExportDataMulty.Tocsv";
            string Scriptfile = "Script.ExportDataMulty.Test1";
            createMultyScriptFiles(Scriptfile);
           SettingsModel settingsModel= createSettingsFileMultyForCsv(Settingsfile, Scriptfile);

            SqlParametersTasks sqlparamstask = new SqlParametersTasks(settingsModel);
            string oldid = sqlparamstask.GetSqlParameter(settingsModel.SettingsFile, "@Id");
            if (oldid == null) oldid = "0";
             //2. run job
             ExportDataJob sg = new ExportDataJob();
            Assert.DoesNotThrow(() => sg.Execute(Settingsfile, "ExportData", "TestInstallation"));

            string newid = sqlparamstask.GetSqlParameter(settingsModel.SettingsFile, "@Id");
            Assert.IsTrue(Convert.ToInt32(newid) > Convert.ToInt32(oldid));

            //3. clean
            ch.DeleteScriptFile(Scriptfile);
            ch.DeleteSettingsFile(Settingsfile);
            ch.DeleteSettingsFile("Backup\\"+Settingsfile);
        }


        [OneTimeTearDown]
        public void DeleteSettingsScriptFile()
        {
            
        }

        #region "Private Members" 

        private void createSettingsFileForXML(string Settingsfile, string Scriptfile)
        {
            //create settings file
            if (ch.SettingsFileExist(Settingsfile)) ch.DeleteSettingsFile(Settingsfile);
            SettingsModel sm = new SettingsModel();
            sm.Custom1DB = ch.ConnectionString("sisifos", "protel", "sqladmin", "111111");
            sm.SqlScript = Scriptfile;
            sm.CultureInfo = "en-us";
            sm.Formater = new Dictionary<string, string>();
            sm.Formater.Add("outldate", "yyyy-MM-dd HH:mm:ss");
            sm.Formater.Add("landkz", "N2");
            sm.TimeStamp = "yyyyMMddHHmmss";      

            sm.XmlFilePath = basePath +@"ExportData.xml";
            sm.XmlRootElement = "Records";
            sm.XmlElement = "Record";

            ch.WriteSettingsFile(Settingsfile, sm, true);
        }

        private void createSettingsFileForJson(string Settingsfile, string Scriptfile)
        {
            //create settings file
            if (ch.SettingsFileExist(Settingsfile)) ch.DeleteSettingsFile(Settingsfile);
            SettingsModel sm = new SettingsModel();
            sm.Custom1DB = ch.ConnectionString("sisifos", "protel", "sqladmin", "111111");
            sm.SqlScript = Scriptfile;
            sm.ClassType = "Controller";
            sm.FullClassName = "Hit.Services.WebApi.Controllers.SQL.SqlController";
            sm.CultureInfo = "en-us";
            sm.Formater = new Dictionary<string, string>();
            sm.Formater.Add("outldate", "yyyy-MM-dd HH:mm:ss");
            sm.Formater.Add("landkz", "N2");
            sm.TimeStamp = "yyyyMMddHHmmss";

            sm.JsonFilePath = basePath + @"ExportData.json";

            ch.WriteSettingsFile(Settingsfile, sm, true);
        }

        private SettingsModel createSettingsFileMultyForCsv(string Settingsfile, string Scriptfile)
        {
            //create settings file
            if (ch.SettingsFileExist(Settingsfile)) ch.DeleteSettingsFile(Settingsfile);
            SettingsModel sm = new SettingsModel();
            sm.Custom1DB = ch.ConnectionString("sisifos", "protel", "sqladmin", "111111");
            sm.SettingsFile = Settingsfile;
            sm.SqlScript = Scriptfile;
            sm.CultureInfo = "en-us";
            sm.Formater = new Dictionary<string, string>();
            sm.Formater.Add("outldate", "yyyy-MM-dd HH:mm:ss");
            sm.Formater.Add("landkz", "N2");
            sm.TimeStamp = "yyyyMMddHHmmss";

            sm.CsvFilePath = basePath + @"ExportDataMulty.csv";
            sm.CsvFileHeader = true;
            sm.CsvDelimenter = ";";
            string Id = "0";
            sm.SqlParameters.Add("@Id", Id);
            ch.WriteSettingsFile(Settingsfile, sm, true);
            return sm;
        }

        private void createSettingsFileForCsv(string Settingsfile, string Scriptfile)
        {
            //create settings file
            if (ch.SettingsFileExist(Settingsfile)) ch.DeleteSettingsFile(Settingsfile);
            SettingsModel sm = new SettingsModel();
            sm.Custom1DB = ch.ConnectionString("sisifos", "protel", "sqladmin", "111111");
            sm.SqlScript = Scriptfile;
            sm.CultureInfo = "en-us";
            sm.Formater = new Dictionary<string, string>();
            sm.Formater.Add("outldate", "yyyy-MM-dd HH:mm:ss");
            sm.Formater.Add("landkz", "N2");
            sm.TimeStamp = "yyyyMMddHHmmss";

            sm.CsvFilePath = basePath + @"ExportData.csv";
            sm.CsvFileHeader = true;
            sm.CsvDelimenter = ";";

            ch.WriteSettingsFile(Settingsfile, sm, true);
        }

        private void createSettingsFileForFix(string Settingsfile, string Scriptfile)
        {
            //create settings file
            if (ch.SettingsFileExist(Settingsfile)) ch.DeleteSettingsFile(Settingsfile);
            SettingsModel sm = new SettingsModel();
            sm.Custom1DB = ch.ConnectionString("sisifos", "protel", "sqladmin", "111111");
            sm.SqlScript = Scriptfile;
            sm.CultureInfo = "en-us";
            sm.Formater = new Dictionary<string, string>();
            sm.Formater.Add("outldate", "yyyy-MM-dd HH:mm:ss");
            sm.Formater.Add("landkz", "N2");
            sm.TimeStamp = "yyyyMMddHHmmss";

            sm.FixedLenghtFilePath = basePath + @"ExportData.txt";
            sm.FixedLenghtFileHeader = true;
            sm.FixedLenghtAlignRight = false;
            sm.FixedLengths = new List<int?>() { 4,100,100,20,4,4};

            ch.WriteSettingsFile(Settingsfile, sm, true);
        }

        private void createSettingsFileForHtml(string Settingsfile, string Scriptfile)
        {
            //create settings file
            if (ch.SettingsFileExist(Settingsfile)) ch.DeleteSettingsFile(Settingsfile);
            SettingsModel sm = new SettingsModel();
            sm.Custom1DB = ch.ConnectionString("sisifos", "protel", "sqladmin", "111111");
            sm.SqlScript = Scriptfile;
            sm.CultureInfo = "en-us";
            sm.Formater = new Dictionary<string, string>();
            sm.Formater.Add("outldate", "yyyy-MM-dd HH:mm:ss");
            sm.Formater.Add("landkz", "N2");
            sm.TimeStamp = "yyyyMMddHHmmss";

            sm.HtmlFilePath = basePath + @"ExportData.html";
            sm.HtmlHeader = true;
            sm.HtmlTitle = "Test Table";
            sm.Htmlcss = @"
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

            ch.WriteSettingsFile(Settingsfile, sm, true);
        }


        private void createSettingsFileForPdf(string Settingsfile, string Scriptfile)
        {
            //create settings file
            if (ch.SettingsFileExist(Settingsfile)) ch.DeleteSettingsFile(Settingsfile);
            SettingsModel sm = new SettingsModel();
            sm.Custom1DB = ch.ConnectionString("sisifos", "protel", "sqladmin", "111111");
            sm.SqlScript = Scriptfile;
            sm.CultureInfo = "en-us";
            sm.Formater = new Dictionary<string, string>();
            sm.Formater.Add("outldate", "yyyy-MM-dd HH:mm:ss");
            sm.Formater.Add("landkz", "N2");
            sm.TimeStamp = "yyyyMMddHHmmss";

            sm.PdfFilePath = basePath + @"ExportData.pdf";
            sm.HtmlHeader = true;
            sm.PdfTitle = "Test Table";
            sm.Pdfcss = @"
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
   font-size: 9px;
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
            ch.WriteSettingsFile(Settingsfile, sm, true);
        }

        private void createSettingsFileForRest(string Settingsfile, string Scriptfile)
        {
            //create settings file
            if (ch.SettingsFileExist(Settingsfile)) ch.DeleteSettingsFile(Settingsfile);
            SettingsModel sm = new SettingsModel();
            sm.Custom1DB = ch.ConnectionString("sisifos", "protel", "sqladmin", "111111");
            sm.SqlScript = Scriptfile;
            sm.CultureInfo = "en-us";
            //sm.Formater = new Dictionary<string, string>();
            //sm.Formater.Add("outldate", "yyyy-MM-dd HH:mm:ss");
            //sm.Formater.Add("landkz", "N2");
            //sm.TimeStamp = "yyyyMMddHHmmss";

            sm.RestServerUrl = @"http://localhost:63918/api/Default/getData";
            sm.RestServerHttpMethod = "Post";
            sm.RestServerMediaType = "application/json";



            ch.WriteSettingsFile(Settingsfile, sm, true);
        }

        private void createScriptFiles(string filename)
        {
            //create script file
            if (ch.ScriptFileExist(filename)) ch.DeleteScriptFile(filename);
            string script = @"SELECT TOP (200) [kdnr],[name1],[name2],[outldate],[prof],[landkz]as lkz FROM [protel].[proteluser].[kunden]";
            ch.WriteScriptFile(filename, script, true);
        }

        private void createMultyScriptFiles(string filename)
        {
            //create script file
            if (ch.ScriptFileExist(filename)) ch.DeleteScriptFile(filename);
            string script = @"
                 IF object_id('tempdb..#TempTable') is not null  drop table #TempTable
                 SELECT  TOP (10)  [kdnr],[name1],[name2],[outldate],[prof],[landkz]as lkz INTO #TempTable FROM [protel].[proteluser].[kunden] where kdnr>@Id 
                 select * from #TempTable
                 select max(kdnr) as Id from #TempTable
                 drop table #TempTable";
            ch.WriteScriptFile(filename, script, true);
        }

        private void createWrongScriptFile(string filename)
        {
            //create script file
            if (ch.ScriptFileExist(filename)) ch.DeleteScriptFile(filename);
            string script = @"SELECT TOP (10) [kdnr],[name1]as [Όνομα],[name2] as [Last Name],[outldate],[prof],[landkz] as lkz FROM [protel].[proteluser].[kunden000000]";
            ch.WriteScriptFile(filename, script, true);
        }

      

        private void createSettingsFile_WrongConString(string Settingsfile, string Scriptfile)
        {
            //create settings file
            if (ch.SettingsFileExist(Settingsfile)) ch.DeleteSettingsFile(Settingsfile);
            SettingsModel sm = new SettingsModel();
            sm.Custom1DB = ch.ConnectionString("sisifos", "protel", "sqladmin", "1233445");
            sm.SqlScript = Scriptfile;
            ch.WriteSettingsFile(Settingsfile, sm, true);
        }

        #endregion

    }
}
