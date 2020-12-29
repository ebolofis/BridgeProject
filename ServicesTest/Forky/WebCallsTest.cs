using Hit.Services.Helpers.Classes.Classes;
using Hit.Services.Models.Models.Forky;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HitServicesTest   //  <----<<< IMPORTANT: All tests must have that namespace
{
    [TestFixture]
    class WebCallsTest
    {

        [OneTimeSetUp]
        public void Init()
        {
        }

        [Test, Order(3)]
        public void Forky_TestGetRequest_Return200()
        {
            int returnCode = 0;
            string ErrorMess = "-";

            //call webapi
            WebApiClientHelper ac = new WebApiClientHelper();
            string result = ac.GetRequest("https://devel.forky.gr/api/backend/venues/1-2OS710/orders",
                "Bearer ZTdmZmY1Zjc5MTQ4NDQ5ZTEzMzIyZTBkNTY1YmJlMzJlZjYzZDA2OTIyMDkzOGIxOTY2N2YzNDA2ZWVlMDkwOQ",
                null, out returnCode,
                out ErrorMess,
                "application/json",
                "OAuth2");

            Assert.AreEqual(200, returnCode);
            Assert.AreEqual("", ErrorMess);
            Assert.NotNull(result);
        }

        [Test, Order(4)]
        public void Forky_TestPatchRequest_Return204()
        {
            int returnCode = 0;
            string ErrorMess = "-";
            ForkyPatchOrderModel model = new ForkyPatchOrderModel() { status = "downloaded" };
            //call webapi
            WebApiClientHelper ac = new WebApiClientHelper();
            string result = ac.PatchRequest<ForkyPatchOrderModel>(model, "https://devel.forky.gr/api/backend/orders/2",
                "Bearer ZTdmZmY1Zjc5MTQ4NDQ5ZTEzMzIyZTBkNTY1YmJlMzJlZjYzZDA2OTIyMDkzOGIxOTY2N2YzNDA2ZWVlMDkwOQ",
                null, out returnCode,
                out ErrorMess,
                "application/json",
                "OAuth2");

            Assert.AreEqual(204, returnCode);
            Assert.AreEqual("", ErrorMess);
            Assert.NotNull(result);
        }

        [Test, Order(5)]
        public void Forky_TestPatchRequest_ReturnError()
        {
            int returnCode = 0;
            string ErrorMess = "-";
            ForkyPatchOrderModel model = new ForkyPatchOrderModel() { status = "downloaded" };
            //call with parameter -2 (invalid order id)
            WebApiClientHelper ac = new WebApiClientHelper();
            string result = ac.PatchRequest<ForkyPatchOrderModel>(model, "https://devel.forky.gr/api/backend/orders/-2",
                "Bearer ZTdmZmY1Zjc5MTQ4NDQ5ZTEzMzIyZTBkNTY1YmJlMzJlZjYzZDA2OTIyMDkzOGIxOTY2N2YzNDA2ZWVlMDkwOQ",
                null, out returnCode,
                out ErrorMess,
                "application/json",
                "OAuth2");

            Assert.AreEqual(404, returnCode);
            Assert.IsTrue(ErrorMess.Contains("Not Found"));
            Assert.IsNull(result);
        }

    }
}
