using Dapper;
using Hit.Services.DataAccess.DT.Sql;
using Hit.Services.Helpers.Classes;
using Hit.Services.MainLogic.Tasks.SQLJobs;
using Hit.Services.Models.Models;
using Hit.Services.Models.Models.SQL;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HitServicesTest   //  <----<<< IMPORTANT: All tests must have that namespace
{

    [TestFixture]
    public class SaveDataToDBHelpersTest   
    {

        [OneTimeSetUp]
        public void Init()
        {
        }


        [Test, Order(1)]
        public void ConstructInsertStatement_WithoutData()
        {         
            string ConString = "server = sisifos; user id = sqladmin; password = 111111; database = Pos_NikkiBeach;";
            GenericDT runsql = new GenericDT();
            SqlConstructorHelper sqlConstruct = new SqlConstructorHelper();

            //create insert statement for table OrderDetail
            DbTableModel table = runsql.GetTableInfo(ConString, "OrderDetail");
            string sql= sqlConstruct.InsertStatment(table);

            int atCount = sql.Count(x => x == '@');

            Assert.IsTrue(sql.StartsWith("INSERT INTO ["+ table.TableName+"] "));
            Assert.AreEqual(table.Columns.Count() -1, atCount);
            Assert.AreEqual(22, table.Columns.Count());
        }



        [Test, Order(2)]
        public void ConstructInsertStatement_WithData()
        {
            string ConString = "server = sisifos; user id = sqladmin; password = 111111; database = Pos_NikkiBeach;";

            SQLTasks sqlTasks = new SQLTasks(null);
            GenericDT runsql = new GenericDT();
            ConvertDynamicHelper dynamicCast = new ConvertDynamicHelper();
            SqlConstructorHelper sqlConstruct = new SqlConstructorHelper();

            //get data from table SqlParameters
            IEnumerable<dynamic> data = runsql.RunSelect(ConString, "select top 1 * from OrderDetail");
           List<IDictionary<string,dynamic>> list= dynamicCast.ToListDictionary(data);

            //create insert statement for table SqlParameters
            DbTableModel table = runsql.GetTableInfo(ConString, "OrderDetail");
            string sql = sqlConstruct.InsertStatment(table, list[0]);
           
            Assert.IsTrue(sql.StartsWith("INSERT INTO [" + table.TableName + "] "));
                  
        }

        [Test, Order(3)]
        public void ConstructInsertEncryptStatement_WithoutData()
        {
            SqlKeyModel sqlKeyModel = new SqlKeyModel() {
                    SymmetricKey ="TestKey",
                    Certificate ="TestCertificate",
                    Password ="mypassword",
                    EncryptedColumns =new List<string>() { "Protelname", "reservationname","email" }
            };
            string ConString = "server = sisifos; user id = sqladmin; password = 111111; database = Pos_NikkiBeach;";
            GenericDT runsql = new GenericDT();
            SqlConstructorHelper sqlConstruct = new SqlConstructorHelper();

            //create insert statement for table OrderDetail
            DbTableModel table = runsql.GetTableInfo(ConString, "TR_ReservationCustomers");
            string sql = sqlConstruct.InsertStatment(table,sqlEncrypt: sqlKeyModel);

            int atCount = sql.Count(x => x == '@');

            Assert.IsTrue(sql.StartsWith("OPEN SYMMETRIC KEY TestKey"));
            Assert.AreEqual(table.Columns.Count() - 1, atCount);
            Assert.AreEqual(8, table.Columns.Count());
        }


        [Test, Order(4)]
        public void ConstructUpdateStatement_WithoutData()
        {
            string ConString = "server = sisifos; user id = sqladmin; password = 111111; database = Pos_NikkiBeach;";
            GenericDT runsql = new GenericDT();
            SqlConstructorHelper sqlConstruct = new SqlConstructorHelper();

            //create insert statement for table OrderDetail
            DbTableModel table = runsql.GetTableInfo(ConString, "OrderDetail");
            string sql = sqlConstruct.UpdateStatment(table);

            int atCount = sql.Count(x => x == '@');

            Assert.IsTrue(sql.StartsWith("UPDATE [" + table.TableName + "] SET"));
            
            Assert.AreEqual(table.Columns.Count() , atCount-2);
            Assert.AreEqual(22, table.Columns.Count());
        }

        [Test, Order(5)]
        public void ConstructUpdateStatement_WithData()
        {
            SqlKeyModel sqlKeyModel = new SqlKeyModel()
            {
                SymmetricKey = "TestKey",
                Certificate = "TestCertificate",
                Password = "mypassword",
                EncryptedColumns = new List<string>() { "Protelname", "reservationname", "email" }
            };
            string ConString = "server = sisifos; user id = sqladmin; password = 111111; database = Pos_NikkiBeach;";

            SQLTasks sqlTasks = new SQLTasks(null);
            GenericDT runsql = new GenericDT();
            SqlConstructorHelper sqlConstruct = new SqlConstructorHelper();
            ConvertDynamicHelper dynamicCast = new ConvertDynamicHelper();

            //get data from table SqlParameters
            IEnumerable<dynamic> data = runsql.RunSelect(ConString, "select top 1 * from OrderDetail");
            List<IDictionary<string, dynamic>> list = dynamicCast.ToListDictionary(data);

            //create insert statement for table SqlParameters
            DbTableModel table = runsql.GetTableInfo(ConString, "OrderDetail");
            string sql = sqlConstruct.UpdateStatment(table, list[0]);

            Assert.IsTrue(sql.StartsWith("UPDATE [" + table.TableName + "] SET"));
        }

        [Test, Order(6)]
        public void ConstructUpdateEncrypStatement_WithoutData()
        {
            SqlKeyModel sqlKeyModel = new SqlKeyModel()
            {
                SymmetricKey = "TestKey",
                Certificate = "TestCertificate",
                Password = "mypassword",
                EncryptedColumns = new List<string>() { "Protelname", "reservationname", "email" }
            };
            string ConString = "server = sisifos; user id = sqladmin; password = 111111; database = Pos_NikkiBeach;";
            GenericDT runsql = new GenericDT();
            SqlConstructorHelper sqlConstruct = new SqlConstructorHelper();

            //create insert statement for table OrderDetail
            DbTableModel table = runsql.GetTableInfo(ConString, "TR_ReservationCustomers");
            string sql = sqlConstruct.UpdateStatment(table, sqlEncrypt: sqlKeyModel);

            int atCount = sql.Count(x => x == '@');

            Assert.IsTrue(sql.StartsWith("OPEN SYMMETRIC KEY TestKey"));

            Assert.AreEqual(table.Columns.Count(), atCount - 2);
            Assert.AreEqual(8, table.Columns.Count());
        }

    }
}
