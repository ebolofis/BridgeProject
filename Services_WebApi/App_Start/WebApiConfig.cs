using Hit.Services.WebApi.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Handlers;
using System.Web.Http;
using Hit.Services.WebApi.Modules;

namespace HitServices_WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //// Web API configuration and services

            //// Web API routes
            //config.MapHttpAttributeRoutes();

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);

            //// config.Filters and Handles
            //config.Filters.Add(new ExceptionAttribute());
            //config.MessageHandlers.Add(new Hit.Services.WebApi.Modules.TraceHandler());
        }
    }
}
