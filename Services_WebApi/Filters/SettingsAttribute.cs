using Hit.Scheduler.Jobs;
using Hit.Services.Core;
using Hit.Services.Helpers.Classes;
using Hit.Services.Helpers.Classes.Classes;
using Hit.Services.Models.Models;
using Hit.Services.Scheduler.Core;
using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Hit.Services.WebApi.Filters
{
    /// <summary>
    /// Filter  getting the proper Settings for the controller.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = true)]
    public class SettingsAttribute : ActionFilterAttribute
    {

        protected ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Get the proper Settings for the controller. 
        /// SettingsModel is stored as Property with Key 'SETTINGSMODEL' (see HitController)
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            base.OnActionExecuting(actionContext);
         
           // HttpResponseMessage response = null;
                   
            try
            {
                string controller = getControllerName(actionContext);

                ConfigHelper ch = new ConfigHelper();

                //1. get WebApiSettingsModel for the specific controler
                WebApiSettingsModel apiSettingsModel = ch.ReadWebApiSettings(FilesState.GetEncryptFilesStatus().WebApiSettingsIsEcrypted).Where(x => x.Controller == controller).FirstOrDefault();

                //2. get SettingsModel
                if (apiSettingsModel !=null && apiSettingsModel.Settings != null)
                {
                    SettingsModel model = ch.ReadSettingsFile(apiSettingsModel.Settings, FilesState.GetEncryptFilesStatus().SettingsFilesAreEncrypted);
                    actionContext.Request.Properties.Add("SETTINGSMODEL", model);
                }
            }
            catch (Exception ex)
            {
                logger.Warn("SettingsAttribute Error: " + ex.ToString());
            }
           

        }

        private string getControllerName(HttpActionContext actionContext)
        {
            return actionContext.ControllerContext.ControllerDescriptor.ControllerType.FullName;
        }
    }
}