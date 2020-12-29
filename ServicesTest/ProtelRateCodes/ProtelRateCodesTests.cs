using Hit.Scheduler.Jobs.Protel;
using Hit.Services.Core;
using Hit.Services.Helpers.Classes.Classes;
using Hit.Services.Models.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HitServicesTest   //  <----<<< IMPORTANT: All tests must have that namespace
{
    [TestFixture]
    public class ProtelRateCodesTests
    {
        ConfigHelper ch;
        string basePath = @"c:\logs\";

        RateCodesJob MainClass;
        public ProtelRateCodesTests()
        {

        }

        [OneTimeSetUp]
        public void Init()
        {
            ch = new ConfigHelper();
        }

        //[TestMethod]
        [Test, Order(300)]
        public void ProtelRateCodes_Execute()
        {
            // 1.create setting file
            string Settingsfile = "ProtelRateCode";
            if (ch.SettingsFileExist(Settingsfile)) ch.DeleteSettingsFile(Settingsfile);
            SettingsModel sm = new SettingsModel();
            sm.ProtelDB = ch.ConnectionString("sisifos", "protel", "proteluser", "protel915930");
            sm.RestServerUrl = "";
            sm.RestServerHttpMethod = "";
            sm.RestServerMediaType = "";
            sm.MpeHotel = "1";
            sm.JsonFilePath = basePath + "ProtelRateCodes";
            sm.WebRateCodeType = 2;
            ch.WriteSettingsFile(Settingsfile, sm, true);

            MainClass = new RateCodesJob();
            Assert.DoesNotThrow(() => MainClass.Execute(Settingsfile, "ProtelRCTest", "TestInstallation"));
            if (File.Exists(basePath + "ProtelRateCodes"))
                File.Delete(basePath + "ProtelRateCodes");
        }

    }
}
