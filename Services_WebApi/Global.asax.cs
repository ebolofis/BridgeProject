using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Http;
using System.Diagnostics;
using Hit.Services.WebApi;

namespace HitServices_WebApi
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            ////AreaRegistration.RegisterAllAreas();
            ////GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);


            ////log4net.Config.XmlConfigurator.Configure();
            ////var logger = log4net.LogManager.GetLogger(this.GetType());
            ////logger.Info("");
            ////logger.Info("");
            ////logger.Info("*****************************************");
            ////logger.Info("*                                       *");
            ////logger.Info("*          Hit Services Started         *");
            ////logger.Info("*                                       *");
            ////logger.Info("*****************************************");
            ////logger.Info("");
            ////System.Diagnostics.Debug.WriteLine("Application Started");
            ////System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            ////FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            ////logger.Info("version: " + fvi.FileVersion);
            ////logger.Info("");
            ////logger.Info("Ready to serve requests!!");
            ////logger.Info("");
            ////logger.Info("");

          //  HangfireBootstrapper.Instance.Start();
        }

        protected void Application_End(object sender, EventArgs e)
        {
        //    HangfireBootstrapper.Instance.Stop();
        }

    }
}