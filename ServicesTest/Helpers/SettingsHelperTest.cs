using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hit.Services.Helpers.Classes.Classes;
using Hit.Services.Models.Models;
using Hit.Services.Core;

namespace HitServicesTest   //  <----<<< IMPORTANT: All tests must have that namespace
{

    /// <summary>
    /// Test serrings helper class
    /// </summary>
    [TestFixture]
   public class SettingsHelperTest
    {


        [OneTimeSetUp]
        public void Init()
        {
        }


        [Test, Order(1)]
        public void ReadSettingsFile()
        {
            string settingsFile = "Test.Settings";
            SettingsModel sett = new SettingsModel()
            {
                RestServerAuthenticationHeader = "username:password",
                DateTo = new DateTime(2018, 10, 30),
                Duration = 20,
                Parameters = new Dictionary<string, string>()// { "param1","param2"}
            };
            sett.Parameters.Add("key1", "param1");
            sett.Parameters.Add("key2", "param2");
            ConfigHelper sh = new ConfigHelper();
            sh.WriteSettingsFile(settingsFile, sett, true);
            SettingsModel settings= sh.ReadSettingsFile(settingsFile, true);


            Assert.NotNull(settings);
            Assert.AreEqual(sett.RestServerAuthenticationHeader, settings.RestServerAuthenticationHeader);
            Assert.IsNull(settings.DateFrom);
            Assert.AreEqual(sett.DateTo.Value.Year, settings.DateTo.Value.Year);
            Assert.AreEqual(sett.DateTo.Value.Month, settings.DateTo.Value.Month);
            Assert.AreEqual(sett.DateTo.Value.Day, settings.DateTo.Value.Day);
            Assert.AreEqual(sett.Duration, settings.Duration);
            Assert.AreEqual(sett.Parameters["key1"], settings.Parameters["key1"]);
            Assert.AreEqual(sett.Parameters["key2"], settings.Parameters["key2"]);
            //Assert.AreEqual(200, settings);
            //Assert.AreEqual("", ErrorMess);
            sh.DeleteSettingsFile(settingsFile);
        }
    }
}
