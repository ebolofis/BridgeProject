using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hit.Services.Helpers.Classes;
using Hit.Services.Models.Models;
using NUnit.Framework;

namespace HitServicesTest   //  <----<<< IMPORTANT: All tests must have that namespace
{

    /// <summary>
    /// Test web api client
    /// </summary>
    [TestFixture]
   public class EmailHelperTests
    {

        [OneTimeSetUp]
        public void Init()
        {
        }


        [Test, Order(10)]
        public void SendEmailTest()
        {
            EmailSendModel email = new EmailSendModel();

            email.From = "estia@hit.com.gr";
            email.To = new List<string>() { "estia@hit.com.gr" };
            email.Subject = "HitServices test email";
            email.Body = "Test Body";

            EmailHelper sendEmail = new EmailHelper();
            sendEmail.Init("192.168.15.3", 587, true, "estia@hit.com.gr", "estia111111");

            Assert.DoesNotThrow(() => sendEmail.Send(email));
        }
    }
}
