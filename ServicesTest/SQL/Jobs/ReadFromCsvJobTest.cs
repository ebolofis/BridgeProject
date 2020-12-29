using Hit.Services.DataAccess.DAOs;
using Hit.Services.DTOs.HitServices;
using Hit.Services.Helpers.Classes.Classes;
using Hit.Services.Models.Models;
using Hit.Services.Scheduler.Jobs.SQL;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Hit.Services.Core;

namespace HitServicesTest //  <----<<< IMPORTANT: All tests must have that namespace
{
    [TestFixture]
    public class ReadFromCsvJobTest
    {
        ConfigHelper ch;
        string basePath = @"c:\logs\";

        [OneTimeSetUp]
        public void Init()
        {
            ch = new ConfigHelper();
           
        }


        [Test, Order(1)]
        public void ReadCSVWithHeader()
        {
            //1. crete csv file to read
            File.Delete(basePath + @"ReadFile.txt");
            string text =
@"id;SettingsFile;Parameter;Value;OldValue
1;fffff;@PP1;1;2
2;fffff;@PP2;3;4";
            System.IO.File.WriteAllText(basePath + @"ReadFile.txt", text);

            //1. create setting  file
            string Settingsfile = "Settings.ReadFromCSV";
            if (ch.SettingsFileExist(Settingsfile)) ch.DeleteSettingsFile(Settingsfile);
            SettingsModel sm = new SettingsModel();
            sm.DestinationDB = ch.ConnectionString("sisifos", "HitServices", "sqladmin", "111111");
            sm.DestinationDBTableName = "SqlParametersBackup";
            sm.DBOperation = 0;
            sm.DBTransaction = true;
            sm.CsvFilePath = basePath + @"ReadFile.txt";
            sm.CsvFileHeader = true;
            sm.CsvDelimenter = ";";

            ch.WriteSettingsFile(Settingsfile, sm, true);

            //2. run job
            ReadFromCsvJob sg = new ReadFromCsvJob();
            Assert.DoesNotThrow(() => sg.Execute(Settingsfile, "ExportData", "TestInstallation"));

            //3. clean setting file and records added in DB
            ch.DeleteSettingsFile(Settingsfile);
            GenericDAO<SqlParametersDTO> sqlparams = new GenericDAO<SqlParametersDTO>();
            using (IDbConnection db = new SqlConnection(sm.DestinationDB))
            {
                int count=  db.Query<int>("select count(*) from SqlParametersBackup where Settingsfile='fffff'").First();
                Assert.AreEqual(2, count);

                db.Execute("delete SqlParametersBackup where Settingsfile='fffff'");
            }
        }

        [Test, Order(2)]
        public void ReadCSVWithoutHeader()
        {
            //1. crete csv file to read
            File.Delete(basePath + @"ReadFile.txt");
            string text =
@"1;fffff;@PP1;1;2
2;fffff;@PP2;3;4";
            System.IO.File.WriteAllText(basePath + @"ReadFile.txt", text);

            //1. create setting  file
            string Settingsfile = "Settings.ReadFromCSV";
            if (ch.SettingsFileExist(Settingsfile)) ch.DeleteSettingsFile(Settingsfile);
            SettingsModel sm = new SettingsModel();
            sm.DestinationDB = ch.ConnectionString("sisifos", "HitServices", "sqladmin", "111111");
            sm.DestinationDBTableName = "SqlParametersBackup";
            sm.DBOperation = 0;
            sm.DBTransaction = true;
            sm.CsvFileHeaders = new List<string>() { "id", "SettingsFile", "Parameter", "Value", "OldValue" };
            sm.CsvFilePath = basePath + @"ReadFile.txt";
            sm.CsvFileHeader = false;//<-------
            sm.CsvDelimenter = ";";

            ch.WriteSettingsFile(Settingsfile, sm, true);

            //2. run job
            ReadFromCsvJob sg = new ReadFromCsvJob();
            Assert.DoesNotThrow(() => sg.Execute(Settingsfile, "ExportData", "TestInstallation"));

            //3. clean setting file and records added in DB
            ch.DeleteSettingsFile(Settingsfile);
            GenericDAO<SqlParametersDTO> sqlparams = new GenericDAO<SqlParametersDTO>();
            using (IDbConnection db = new SqlConnection(sm.DestinationDB))
            {
                int count = db.Query<int>("select count(*) from SqlParametersBackup where Settingsfile='fffff'").First();
                Assert.AreEqual(2, count);

                db.Execute("delete SqlParametersBackup where Settingsfile='fffff'");
            }
        }


    }
}
