using Hit.Scheduler.Jobs.Iberostar;
using Hit.Scheduler.Jobs.Singular;
using Hit.Services.Core;
using Hit.Services.DataAccess.DT.Iberostar;
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
    /// <summary>
    ///Miragio Fuel Import Tests
    /// </summary>
    [TestFixture]
    public class FuelImportTests
    {
        ConfigHelper ch;
        string basePath = @"c:\logs\";

        [OneTimeSetUp]
        public void Init()
        {
            ch = new ConfigHelper();
        }

        //[TestMethod]
        [Test, Order(6)]
        public void SingularStationManager_FuelExportsJob()  
        {
            //1. copy file to the right location
            if(!File.Exists(@"\\sisifos\ftp\HitServices\Implementations\Singular\FuelExports\TestFolder\SMexp-20180517-1720.exp"))
                 File.Copy(@"\\sisifos\ftp\HitServices\Implementations\Singular\FuelExports\SMexp-20180517-1720-ForTest.exp", @"\\sisifos\ftp\HitServices\Implementations\Singular\FuelExports\TestFolder\SMexp-20180517-1720.exp");
           
            //2. create setting  file
            string Settingsfile = "MiragioFuelExport";
            if (ch.SettingsFileExist(Settingsfile)) ch.DeleteSettingsFile(Settingsfile);
            SettingsModel sm = new SettingsModel();
            sm.ProtelDB = ch.ConnectionString("sisifos", "protel", "proteluser", "protel915930");//"192.168.15.93\\sql_2014", "protel", "proteluser", "protel915930"
            sm.MpeHotel = "1";
            sm.FileInfoDirectory = @"\\sisifos\ftp\HitServices\Implementations\Singular\FuelExports\TestFolder";
            sm.FileInfoFileName = "SMexp-*.exp";
            sm.FileInfoCommentsChar = ";";
            sm.ProtelTypeToRoom = new Dictionary<string, string>();
            sm.ProtelTypeToRoom.Add("818310500", "9601");//FuelType, Room
            sm.ProtelTypeToRoom.Add("828214500", "9601");//FuelType, Room
            sm.ProtelTypeToRoom.Add("828215500", "9601");//FuelType, Room
            sm.ProtelTypeToRoom.Add("818320500", "9601");//FuelType, Room

            sm.ProtelTypeToDepartment = new Dictionary<string, string>();
            sm.ProtelTypeToDepartment.Add("818310500", "500");//FuelType, Department  834
            sm.ProtelTypeToDepartment.Add("828214500", "501");//FuelType, Department 833
            sm.ProtelTypeToDepartment.Add("828215500", "502");//FuelType, Department 832
            sm.ProtelTypeToDepartment.Add("818320500", "503");//FuelType, Department 830

            ch.WriteSettingsFile(Settingsfile, sm, true);

            //3. Execute Job
            FuelExportsJob Job = new FuelExportsJob();
            Assert.DoesNotThrow(() => Job.Execute(Settingsfile, Settingsfile, "TestInstallation"));

            //4. Delete Settingsfile
            if (ch.SettingsFileExist(Settingsfile)) ch.DeleteSettingsFile(Settingsfile);


        }


    }
}
