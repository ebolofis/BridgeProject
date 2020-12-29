using Hit.Services.Helpers.Classes.Classes;
using Hit.Services.Models.Models;
using Hit.Scheduler.Jobs.Protel;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hit.Services.Models.Models.Protel;
using System.IO;
using Hit.Services.Core;

namespace HitServicesTest   //  <----<<< IMPORTANT: All tests must have that namespace
{
    [TestFixture]
    public class ProtelAvailabilityTest
    {
        ConfigHelper ch;
        string basePath = @"c:\logs\";
        AvailabilityJob Api;
        public ProtelAvailabilityTest()
        {

        }

        [OneTimeSetUp]
        public void Init()
        {
            ch = new ConfigHelper();
        }

        //[TestMethod]
        [Test, Order(200)]
        public void ProtelAvailability_Execute()
        {
            //1. create setting  file
            string Settingsfile = "ProtelAvailable";
            if (ch.SettingsFileExist(Settingsfile)) ch.DeleteSettingsFile(Settingsfile);
            SettingsModel sm = new SettingsModel();
            sm.ProtelDB = ch.ConnectionString("sisifos", "protel", "proteluser", "protel915930");
            sm.RestServerUrl = "";
            sm.RestServerHttpMethod = "";
            sm.RestServerMediaType = "";
            sm.MpeHotel = "1";
            sm.CustomerName = "HitCustomer";
            sm.JsonFilePath = basePath + "ProtelAvailability";
            sm.Duration = 100;
            ch.WriteSettingsFile(Settingsfile, sm, true);

            //2. Execute Job
            Api = new AvailabilityJob();
            Assert.DoesNotThrow(() => Api.Execute(Settingsfile, "ProtelAvailTest", "TestInstallation"));

            //3. Delete Files
            if (File.Exists(basePath + "ProtelAvailability"))
                File.Delete(basePath + "ProtelAvailability");
        }
    }
}
