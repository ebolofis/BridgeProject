using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Hit.Services.DTOs.Protel;
using Hit.Services.Models.Models;
using Hit.Services.Models.Models.Singular;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Hit.Services.DataAccess.DAOs;
using Hit.Services.Helpers.Classes.Classes;
using Hit.Services.Models.Models.Protel;

namespace HitServicesTest   //  <----<<< IMPORTANT: All tests must have that namespace
{
    [TestFixture]
    public class ProtelDAOTest
    {

        ConfigHelper ch;
        private string ConnString;

       [OneTimeSetUp]
        public void Init()
        {
            ch = new ConfigHelper();
            ConnString = ch.ConnectionString("sisifos", "protel", "proteluser", "protel915930");;
        }

      

        [Test, Order(400)]
        public void ProtelDAO_GetLeistRefTest()
        {
            ProtelDAO protelDao = new ProtelDAO();
            int id,idOld,idfinal;
            using (IDbConnection db = new SqlConnection(ConnString))
            {
                idOld= db.Query<int>("SELECT ISNULL(kdnr, 0)  FROM proteluser.leistref").FirstOrDefault();
                using (var scope = new TransactionScope())
                {
                    id= protelDao.GetLeistRef(db);
                   // scope.Complete(); //<----DO NOT COMMIT----<<<<
                }
                idfinal = db.Query<int>("SELECT ISNULL(kdnr, 0)  FROM proteluser.leistref").FirstOrDefault();
            }

            Assert.IsTrue(id == idOld + 1);
            Assert.IsTrue(idOld == idfinal);
        }


        [Test, Order(401)]
        public void ProtelDAO_GetLeistDataTest()
        {
            ProtelDAO protelDao = new ProtelDAO();
           List<LeistDataModel> data;
            using (IDbConnection db = new SqlConnection(ConnString))
            {              
                    data = protelDao.GetLeistData(db,1,"9601",500);
            }

            Assert.IsNotNull(data);
            Assert.IsTrue(data[0].depId == 500);
            Assert.IsTrue(data[0].ziname == "9601");
        }

        [Test, Order(401)]
        public void ProtelDAO_GetCurrentDateTest()
        {
            ProtelDAO protelDao = new ProtelDAO();
            DateTime dt;
            using (IDbConnection db = new SqlConnection(ConnString))
            {
                dt = protelDao.GetProtelDate(db, 1);
            }

            Assert.IsNotNull(dt);
           
        }

    }
}
