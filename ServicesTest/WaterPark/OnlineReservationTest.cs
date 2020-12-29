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
using Hit.Services.MainLogic.Flows.WaterPark;
using Hit.Services.Models.Models.WaterPark;
using Hit.Services.DataAccess.DT.WaterPark;
using Hit.Services.Core;

namespace HitServicesTest //  <----<<< IMPORTANT: All tests must have that namespace
{
    [TestFixture]
    public class OnlineReservationTest
    {
        ConfigHelper ch;
        string basePath = @"c:\logs\";

        [OneTimeSetUp]
        public void Init()
        {
            ch = new ConfigHelper();
           
        }


        [Test, Order(1)]
        public void WaterParkOnLineRegistration()
        {
            //1. create setting  file
            string Settingsfile = "waterpark.onlinereservation";
            if (ch.SettingsFileExist(Settingsfile)) ch.DeleteSettingsFile(Settingsfile);
            SettingsModel sm = new SettingsModel();
            sm.WebPosDB = ch.ConnectionString("sisifos", "webpos_WP", "sqladmin", "111111");
            sm.DBTimeout = "20";
            ch.WriteSettingsFile(Settingsfile, sm, true);

            //1. create new registration
            OnLineRegistrationModel model = new OnLineRegistrationModel()
            {
                BarCode = DateTime.Now.ToString("ddHHmmss"),
                FirtName = "Houlio",
                LastName = "Delacouna",
                Mobile = "12345678",
                Dates = 2,
                Children = 2,
                Adults = 2,
                PaymentType = 0,
                ChildTicket = 15,
                AdultTicket = 20,
                RegistrationDate = new DateTime(2018, 5, 12)
            };

            //3. run flow
            OnlineRegistrationFlow sg = new OnlineRegistrationFlow(sm);
            Assert.DoesNotThrow(() => sg.AddRegistration(model));

            //4. clean setting file and records added in DB
            ch.DeleteSettingsFile(Settingsfile);
            OnlineRegistrationDT dt = new OnlineRegistrationDT(sm);
           // int count = dt.Delete(model.BarCode);

           // Assert.AreEqual(1, count);
          
        }

      
     


    }
}
