using Dapper;
using Hit.Scheduler.Jobs.SQL;
using Hit.Services.Core;
using Hit.Services.DataAccess.DT.Sql;
using Hit.Services.Helpers.Classes;
using Hit.Services.Helpers.Classes.Classes;
using Hit.Services.MainLogic.Tasks.SQLJobs;
using Hit.Services.Models.Models;
using Hit.Services.Models.Models.SQL;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HitServicesTest   //  <----<<< IMPORTANT: All tests must have that namespace
{
    [TestFixture]
    public class SaveToTableJobTest
    {
        ConfigHelper ch;

        [OneTimeSetUp]
        public void Init()
        {
            ch = new ConfigHelper();
        }


        [Test, Order(1)]
        public void SaveToTable()
        {
            //1. create settinga and script files
            string Settingsfile = "Settings.SaveToTable.Test1";
            string Scriptfile = "Script.SaveToTable.Test1";
            createScriptFiles(Scriptfile);
            createSettingsFile(Settingsfile, Scriptfile);

            //2. run job
            SaveToTableJob sg = new SaveToTableJob();
            Assert.DoesNotThrow(() => sg.Execute(Settingsfile, "RunSaveToTableTest", "TestInstallation"));

            //3. clean
            ch.DeleteScriptFile(Scriptfile);
            ch.DeleteSettingsFile(Settingsfile);
        }






        #region "Private Members" 

        private void createScriptFiles(string filename)
        {
            //create script file
            if (ch.ScriptFileExist(filename)) ch.DeleteScriptFile(filename);
            string script = @"select * from SqlParameters";
            ch.WriteScriptFile(filename, script, true);
        }

     

        private void createSettingsFile(string Settingsfile, string Scriptfile)
        {
            //create settings file
            if (ch.SettingsFileExist(Settingsfile)) ch.DeleteSettingsFile(Settingsfile);
            SettingsModel sm = new SettingsModel();
            sm.SourceDB = ch.ConnectionString("sisifos", "HitServices", "sqladmin", "111111");
            sm.DestinationDB = ch.ConnectionString("sisifos", "HitServices", "sqladmin", "111111");
            sm.DestinationDBTableName = "SqlParametersBackup";
            sm.DBTransaction = true;
            sm.DBOperation = 2;
            sm.SqlScript = Scriptfile;
            ch.WriteSettingsFile(Settingsfile, sm, true);
        }

      

        #endregion

    }
}
