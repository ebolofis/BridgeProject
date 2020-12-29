using Hit.Services.Models;
using Hit.Scheduler.Jobs.SmartIn;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hit.Services.Models.Models;
using Hit.Services.Helpers.Classes.Classes;
using Hit.Services.Core;

namespace HitServicesTest   //  <----<<< IMPORTANT: All tests must have that namespace
{
    [TestFixture]
    public class SmartInFlowTest
    {
        /// <summary>
        /// Main class for SmartIn System
        /// </summary>
        ELockJob smartIn;
        ConfigHelper ch;

        public SmartInFlowTest()
        {
           
        }

        [OneTimeSetUp]
        public void Init()
        {
            ch = new ConfigHelper();
        }

        /// <summary>
        /// Checks Execute
        /// </summary>
        [TestMethod]
        [Test, Order(100)]
        public void SmartIn_System_Execute()
        {
            //1. create a settings file
            string settingsFile = "Test.Smartin";
            SettingsModel sett = new SettingsModel()
            {
                SmartInElockDB = ch.ConnectionString("sisifos", "SmartInK29", "sqladmin", "111111"),
                ProtelDB= ch.ConnectionString("sisifos", "protel", "proteluser", "protel915930"),
                MpeHotel ="1",
                ProtelUser="proteluser"

            };
            ConfigHelper sh = new ConfigHelper();
            sh.WriteSettingsFile(settingsFile, sett, true);

            //2. Execute
            smartIn = new ELockJob();
            smartIn.Execute(settingsFile, "SmartInK29Test", "TestInstallation");

            //3.Delete settings file
            sh.DeleteSettingsFile(settingsFile);
        }
    }
}
