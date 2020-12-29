
using Hit.Services.Core;
using Hit.Services.Helpers.Classes.Classes;
using Hit.Services.Models.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HitServicesTest   //  <----<<< IMPORTANT: All tests must have that namespace
{
    /// <summary>
    ///Forex fixer Tests
    /// </summary>
    [TestFixture]
    public class FixerJobTest
    {
        ConfigHelper ch;
        string basePath = @"c:\logs\";

        [OneTimeSetUp]
        public void Init()
        {
            ch = new ConfigHelper();
        }

        [Test, Order(3)]
        public void ForexFixerJobTest()
        {
           
            //1. create setting  file
            string Settingsfile = "ForexFixerImport";
            if (ch.SettingsFileExist(Settingsfile)) ch.DeleteSettingsFile(Settingsfile);
            SettingsModel sm = new SettingsModel();

            sm.Custom1DB = ch.ConnectionString("sisifos", "protel", "proteluser", "protel915930");
            sm.ForexFixerFrom = "EUR";
            sm.ForexFixerTo = "USD,AUD,CAD,PLN,MXN,ALL,MKD";
            sm.ForexFixerDeleteOld = true;
            sm.ForexFixerApiKey = "fa1da865ec00e61261158866d19f9d68";
            sm.ForexFixerBaseUrl = "http://data.fixer.io/api/latest";

            sm.ErrorEmailFrom = "estia@hit.com.gr";
            sm.ErrorEmailTo= new List<string>() { "estia@hit.com.gr" };
            sm.ErrorEmailSmtp = "192.168.15.3";
            sm.ErrorEmailPort = 587;
            sm.ErrorEmailUsername = "estia@hit.com.gr";
            sm.ErrorEmailPassword = "estia111111";
            sm.ErrorEmailSsl = true;

            ch.WriteSettingsFile(Settingsfile, sm, true);

            //2. Execute Job
            //FixerJob Job = new FixerJob();
           // Assert.DoesNotThrow(() => Job.Execute(Settingsfile, Settingsfile, "TestInstallation"));

            //3. Delete Settingsfile
            if (ch.SettingsFileExist(Settingsfile)) ch.DeleteSettingsFile(Settingsfile);


        }




    }
}
