using Hit.Services.Helpers.Classes.Classes;
using Hit.Services.Models.Enums;
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
    /// Test web api client
    /// </summary>
    [TestFixture]
    public class ApiClientHelperTest 
    {

        string storeId = "0001eeec-752a-45cf-a214-1a868731f088";
        string user = "1:1";

        [OneTimeSetUp]
        public void Init()
        {
        }


        [Test, Order(1)]
        public void TestGetRequest_Return200()
        {
            int returnCode = 0;
            string ErrorMess = "-";

            //create user and custom headers
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("STOREID", storeId);

            //call webapi
            WebApiClientHelper ac = new WebApiClientHelper();
            string result= ac.GetRequest("http://sisifos:5420/api/Staff", user, headers, out  returnCode, out  ErrorMess); //v3fix_webapi

            Assert.AreEqual(200, returnCode);
            Assert.AreEqual("", ErrorMess);
            Assert.NotNull(result);
        }


    



        [Test, Order(2)]
        public void TestGetRequest_Return404()
        {
            int returnCode = 0;
            string ErrorMess = "-";

            //create user and custom headers
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("STOREID", storeId);

            WebApiClientHelper ac = new WebApiClientHelper();
            //send wrong url, expecting 404
            string result = ac.GetRequest("http://sisifos:5420/api/Staff2", user, headers, out returnCode, out ErrorMess);

            Assert.AreEqual(404, returnCode);
            Assert.IsTrue(ErrorMess.Contains("No HTTP resource was found that matches the request "));
            Assert.Null(result);
        }


        [Test, Order(3)]
        public void TestGetRequest_Return401()
        {
            int returnCode = 0;
            string ErrorMess = "-";
          
            //create user and custom headers
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("STOREID", storeId);
            string wrongUser = "999:999"; 

            WebApiClientHelper ac = new WebApiClientHelper();
            string result = ac.GetRequest("http://sisifos:5420/api/Staff", wrongUser, headers, out returnCode, out ErrorMess);

            Assert.AreEqual(401, returnCode);
            Assert.IsTrue(ErrorMess.Contains("Authorization has been denied for this request."));
            Assert.Null(result);
        }


        [Test, Order(1)]
        public void TestPostRequest_Return200()
        {
            int returnCode = 0;
            string ErrorMess = "-";

            LoginModel user = new LoginModel() { Username = "sysadmin", Password = "111111" };

            WebApiClientHelper ac = new WebApiClientHelper();
            string result = ac.PostRequest< LoginModel>(user,"http://sisifos:5430/api/Main/Login", null, null, out returnCode, out ErrorMess);

            Assert.AreEqual(200, returnCode);
            Assert.AreEqual("", ErrorMess);
            Assert.NotNull(result);
        }

        [Test, Order(2)]
        public void TestPostRequest_Return404()
        {
            int returnCode = 0;
            string ErrorMess = "-";

            LoginModel user = new LoginModel() { Username = "sysadmin", Password = "111111" };

            WebApiClientHelper ac = new WebApiClientHelper();
            //send wrong url, expecting 404
            string result = ac.PostRequest<LoginModel>(user, "http://sisifos:5430/api/Main/Login2", null, null, out returnCode, out ErrorMess);

            Assert.AreEqual(404, returnCode);
            Assert.IsTrue(ErrorMess.Contains("The resource cannot be found"));
            Assert.Null(result);
        }


  
       

    }
}
