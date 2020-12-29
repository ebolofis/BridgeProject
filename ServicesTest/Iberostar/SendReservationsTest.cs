using Hit.Scheduler.Jobs.Iberostar;
using Hit.Services.Core;
using Hit.Services.DataAccess.DT.Iberostar;
using Hit.Services.Helpers.Classes.Classes;
using Hit.Services.Models.Models;
using Hit.Services.Models.Models.Iberostar;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HitServicesTest
{
    [TestFixture]
    public class SendReservationsTest
    {

        ConfigHelper ch;
        string basePath = @"c:\logs\";

        [OneTimeSetUp]
        public void Init()
        {
            ch = new ConfigHelper();
        }

        //[TestMethod]
        [Test, Order(400)]
        public void ExecuteJob()
        {
            //1. create setting  file
            string Settingsfile = "SendReservations";
            if (ch.SettingsFileExist(Settingsfile)) ch.DeleteSettingsFile(Settingsfile);
            SettingsModel sm = new SettingsModel();
            sm.ProtelDB = ch.ConnectionString("chaos", "protel", "proteluser", "protel915930");
            sm.RestServerUrl = "";
            sm.RestServerHttpMethod = "";
            sm.RestServerMediaType = "";
            sm.MpeHotel = "1";
            sm.CustomerName = "HitCustomer";
            sm.XmlFilePath = basePath + "SendReservations.xml";
            sm.Duration = 100;
            ch.WriteSettingsFile(Settingsfile, sm, true);


            //2. Execute Job
            SendReservationsJob Job = new SendReservationsJob();
            Assert.DoesNotThrow(() => Job.Execute(Settingsfile, "SendReservationsTest", "TestInstallation"));


            //3. Delete Files
            SendeReservationsDTs DTO = new SendeReservationsDTs(sm);
            List<MpeHotelsModel> hotels = DTO.GetProtelHotels(0);
            foreach (MpeHotelsModel item in hotels)
            {
                if (File.Exists(basePath + item.mpehotel.ToString() + "_" + item.shortName + ".xml"))
                    File.Delete(basePath + item.mpehotel.ToString() + "_" + item.shortName + ".xml");
            }
            if (File.Exists(sm.XmlFilePath))
                File.Delete(sm.XmlFilePath);
            
            
        }
    }
}
