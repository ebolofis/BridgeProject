using Hit.Services.Models;
using Hit.Scheduler.Jobs.SQL;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hit.Services.Helpers.Classes.Classes;
using Hit.Services.Models.Models;
using Hit.Services.Core;

namespace HitServicesTest //  <----<<< IMPORTANT: All tests must have that namespace
{
    [TestFixture]
    public class RunScriptJobTest
    {

        ConfigHelper ch;
    
        [OneTimeSetUp]
        public void Init()
        {
            ch = new ConfigHelper();
        }


        [Test, Order(1)]
        public void RunScriptJobTest_Execute()
        {
            //1. create settinga and script files
            string Settingsfile = "Settings.RunScript.Test1";
            string Scriptfile = "Script.RunScript.Test1";
            createScriptFiles(Scriptfile);
            createSettingsFile(Settingsfile, Scriptfile);

            //2. run job
            RunScriptJob sg = new RunScriptJob();
            Assert.DoesNotThrow(() => sg.Execute(Settingsfile, "RunScriptJobTest", "TestInstallation"));

            //3. clean
            ch.DeleteScriptFile(Scriptfile);
            ch.DeleteSettingsFile(Settingsfile);
        }

        [Test, Order(2)]
        public void RunScriptJobTest_InvalidSettingsFile_ThrowsException()
        {
            //run job using unexisting Settingsfile
            RunScriptJob sg = new RunScriptJob();
           var ex= Assert.Throws<Exception>(() => sg.Execute("DammySettingsFile", "RunScriptJobTest", "TestInstallation"));
            Assert.That(ex.Message, Is.EqualTo("Invalid Settings"));
        }

        [Test, Order(3)]
        public void RunScriptJobTest_InvalidConnectionString_ThrowsException()
        {
            //Create SettingsFile with a wrong connectionstring
            string Settingsfile = "Settings.RunScript.Test1";
            string Scriptfile = "Script.RunScript.Test1";
            createScriptFiles(Scriptfile);
            createSettingsFile_WrongConString(Settingsfile, Scriptfile);

            RunScriptJob sg = new RunScriptJob();
            var ex = Assert.Throws<System.Data.SqlClient.SqlException>(() => sg.Execute(Settingsfile, "RunScriptJobTest", "TestInstallation"));
           
            ch.DeleteScriptFile(Scriptfile);
            ch.DeleteSettingsFile(Settingsfile);
        }

        [Test, Order(3)]
        public void RunScriptJobTest_InvalidScript_ThrowsException()
        {
            //Create ScriptFile with a wrong sql
            string Settingsfile = "Settings.RunScript.Test1";
            string Scriptfile = "Script.RunScript.Test1";
            createWrongScriptFile(Scriptfile);
            createSettingsFile(Settingsfile, Scriptfile);

            RunScriptJob sg = new RunScriptJob();
            var ex = Assert.Throws<System.Data.SqlClient.SqlException>(() => sg.Execute(Settingsfile, "RunScriptJobTest", "TestInstallation"));
            Assert.That(ex.Message, Is.EqualTo("Invalid column name 'shoooooooooooortname'."));

            ch.DeleteScriptFile(Scriptfile);
            ch.DeleteSettingsFile(Settingsfile);
        }

        [OneTimeTearDown]
        public void DeleteSettingsScriptFile()
        {
           
        }


        #region "Private Members" 

        private void createScriptFiles(string filename)
        {
            //create script file
            if (ch.ScriptFileExist(filename)) ch.DeleteScriptFile(filename);
            string script = @"declare @kdnr int
                select top 1 @kdnr=kdnr from proteluser.kunden
                update proteluser.kunden set shortname='Test User' where kdnr=@kdnr";
            ch.WriteScriptFile(filename, script, true);
        }

        private void createWrongScriptFile(string filename)
        {
            //create script file
            if (ch.ScriptFileExist(filename)) ch.DeleteScriptFile(filename);
            string script = @"declare @kdnr int
                select top 1 @kdnr=kdnr from proteluser.kunden
                update proteluser.kunden set shoooooooooooortname='Test User' where kdnr=@kdnr";
            ch.WriteScriptFile(filename, script, true);
        }

        private void createSettingsFile(string Settingsfile, string Scriptfile)
        {
            //create settings file
            if (ch.SettingsFileExist(Settingsfile)) ch.DeleteSettingsFile(Settingsfile);
            SettingsModel sm = new SettingsModel();
            sm.Custom1DB = ch.ConnectionString("sisifos", "protel", "sqladmin", "111111");
            sm.SqlScript = Scriptfile;
            ch.WriteSettingsFile(Settingsfile, sm, true);
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
