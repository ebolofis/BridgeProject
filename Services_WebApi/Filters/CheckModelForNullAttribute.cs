using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Hit.Services.WebApi.Filters
{
    /// <summary>
    /// Action filter that checks the action arguments to find out whether any of them has been passed as null
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = true)]
    public class CheckModelForNullAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (actionContext.ActionArguments.ContainsValue(null))
            {
                actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                        string.Format("The argument cannot be null: {0}", string.Join(",",
                         actionContext.ActionArguments.Where(i => i.Value == null).Select(i => i.Key))));
            }
        }
    }
}