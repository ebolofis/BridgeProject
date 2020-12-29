
using Hit.Services.Helpers.Classes.Classes;
using log4net;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;

namespace Hit.Services.WebApi.Filters
{
    /// <summary>
    /// Class that handles exception from Controllers
    /// </summary>
    public class ExceptionAttribute : ExceptionFilterAttribute
    {
        protected ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        public override void OnException(HttpActionExecutedContext context)
        {
            HttpResponseMessage response = null;
            ExceptionMessHelper exc = new ExceptionMessHelper();
            logger.Error(context.Exception.ToString());
            response = new HttpResponseMessage(HttpStatusCode.NotFound)
            {
                Content = new StringContent(exc.GetErrorMessage(context.Exception)),
                ReasonPhrase = ""
            };
            context.Response = response;



            return;

        }
    }
}