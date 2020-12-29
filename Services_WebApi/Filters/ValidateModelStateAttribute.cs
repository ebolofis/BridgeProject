using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Web.Http.ModelBinding;
using Hit.Services.Helpers.Classes.Classes;

namespace Hit.Services.WebApi.Filters
{
    /// <summary>
    /// Action Filter that obtain the associated ModelState dictionary.
    /// If the state of that object is invalid, it returns an error response with the status code 400 (bad request) back to the client, with the ModelState attached, 
    /// as it will contain the errors from your DataAnnotations or any other validation logic built around IValidateableObject.
    /// This response can then be inspected by the client and appropriate corrective actions taken on its side.
    /// 
    /// In order for the Actions to be validated, must be decorated with the attribute: [ValidateModelState]
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = true)]
    public class ValidateModelStateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (!actionContext.ModelState.IsValid)
            {
                StringBuilder message = new StringBuilder();
                message.Append("<p class='hitservices_p>" + Hit.Services.Resources.Errors.VALIDATIONERRORS + "</p>");
                var errors = actionContext.ModelState.Where(n => n.Value.Errors.Count > 0).ToList();
                message.Append("<ul class='hitservices_ul'>");
                foreach (ModelState modelState in actionContext.ModelState.Values)
                {
                    foreach (ModelError error in modelState.Errors)
                    {
                        if (error.ErrorMessage != "")
                            message.Append("<li class='hitservices_li'>" + error.ErrorMessage + "</li>");
                        else if (error.Exception != null && error.Exception.Message != "")
                            message.Append("<li class='hitservices_li'>" + getMessage(error.Exception.Message) + "</li>");
                        else
                            message.Append("<li class='hitservices_li'>Error</li>");
                    }
                }

                message.Append("</ul>");
                // actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, message.ToString());

                HttpResponseMessage response = null;
                response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(message.ToString()),
                    ReasonPhrase = ""
                };
                actionContext.Response = response;
                return;

            }
        }

        private string getMessage(string message)
        {
            //handle error messages from NewtonJson
            if (message.StartsWith("Required property '"))
            {
                RegExHelper regex = new RegExHelper();
                string prop = regex.GetString(message, "Required property '(?<prop>\\w+)'", "prop");
                if (prop.EndsWith("Id")) prop = prop.Substring(0, prop.Length - 2);//remove the ending Id...
                return String.Format(Hit.Services.Resources.Errors.PROPERTYISREQUIRED, prop);
            }

            return message;

        }
    }
}