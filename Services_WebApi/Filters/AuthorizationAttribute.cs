using Hit.Services.Helpers.Classes;
using Hit.Services.Helpers.Classes.Classes;
using Hit.Services.Models.Models;
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
    /// Filter checking Users Permisions for accessing the current Controller's Action
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = true)]
    public class AuthorizationAttribute : ActionFilterAttribute
    {
        protected ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Authenticate User for actions decorated with Attribute 'AuthorizedAction' 
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            base.OnActionExecuting(actionContext);

            LoginModel login;
            HttpResponseMessage response = null;

            try
            {
                //1.  Get username, password and companyId from AuthorizationHeader and pack them into a LoginModel.
                login = getAuthorizationHeader(actionContext);

                //2. If Action is decorated with the Attribute 'AuthorizedAction' then AuthorizationHeader is mandatory.
                if (login == null)
                {
                    logger.Error("Invalid AuthorizationHeader.");
                    response = new HttpResponseMessage(HttpStatusCode.NotFound)
                    { Content = new StringContent(string.Format(Hit.Services.Resources.Errors.USERAUTHENTICATION)), ReasonPhrase = "" };
                    actionContext.Response = response;
                    return;
                }

                //3. Check if user exists in users.json file.
                logger.Info("Authorizing user " + (login.Username ?? "<null>") + "...");
                List<LoginModel> users = readUsersfile();
                string controller = getControllerName(actionContext);

                EncryptionHelper eh = new EncryptionHelper();
                
                LoginModel usr = users.Where(x => x.Username == login.Username && x.Password ==login.Password && x.AccessTo== controller).FirstOrDefault();
                if (usr == null)
                {
                    //Unauthorised user
                    logger.Warn("User " + (login.Username ?? "<null>") + " did not find.");
                    response = new HttpResponseMessage(HttpStatusCode.NotFound) { Content = new StringContent(string.Format(Hit.Services.Resources.Errors.UNAUTHORIZEDUSER)), ReasonPhrase = "" };
                    actionContext.Response = response;
                    return;
                }

                //4. Check if user has rigths to access the specific Controller.
                //string controller = getControllerName(actionContext);
                //if (usr.AccessTo.Contains(controller))
                //{
                //    //Unauthorised user
                //    logger.Warn("User " + (login.Username ?? "<null>") + " has no rigths to access the specific Controller.");
                //    response = new HttpResponseMessage(HttpStatusCode.NotFound) { Content = new StringContent(string.Format(Hit.Services.Resources.Errors.UNAUTHORIZEDUSER)), ReasonPhrase = "" };
                //    actionContext.Response = response;
                //    return;
                //}

                //5.  Add LoginModel to Request in order to be accessible to BasicController
                actionContext.Request.Properties.Add("LOGINMODEL", usr);
            }
            catch (BusinessException ex)
            {
                logger.Warn("Authentication Error: " + ex.Message);
                response = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format(ex.Message)),
                    ReasonPhrase = ""
                };
                actionContext.Response = response;
                return;
            }
            catch (Exception ex)
            {
                logger.Error("Authentication Error: " + ex.ToString());
                response = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format(ex.Message)),
                    ReasonPhrase = ""
                };
                actionContext.Response = response;
                return;
            }

        }


        /// <summary>
        /// get username and password from AuthenticationHeader.
        /// </summary>
        /// <returns></returns>
        private LoginModel getAuthorizationHeader(HttpActionContext actionContext)
        {
            LoginModel login = new LoginModel();
            try
            {
                var authHeader = actionContext.Request.Headers.Authorization;//HttpContext.Current.Request.Headers["Authorization"];
                if (authHeader != null)
                {
                    if (authHeader.Scheme.Equals("basic", StringComparison.OrdinalIgnoreCase) && authHeader.Parameter != null)
                    {
                        var encoding = Encoding.GetEncoding("iso-8859-1");
                        string credentials = encoding.GetString(Convert.FromBase64String(authHeader.Parameter));  //>>---> credentials have stracture:  username:Password

                        int separator = credentials.IndexOf(':');
                        login.Username = credentials.Substring(0, separator);
                        login.Password = credentials.Substring(separator + 1);

                        return login;
                    }
                }
            }
            catch (Exception ex)
            {
                ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                logger.Error("Error getting Credentials from Authorization Header: " + ex.ToString());
                return null;
            }
            return null;
        }

        private string getControllerName(HttpActionContext actionContext)
        {   
            return actionContext.ControllerContext.ControllerDescriptor.ControllerType.FullName;//class name
        }

        /// <summary>
        /// Read users.json file
        /// </summary>
        private List<LoginModel> readUsersfile()
        {
            FileHelpers fh = new FileHelpers();
            string json = fh.ReadEncryptedFile(GetBasePath() + "/Json/Users.json");
            return Newtonsoft.Json.JsonConvert.DeserializeObject<List<LoginModel>>(json);
        }

        public static string GetBasePath()
        {
            string basepath = System.Web.Hosting.HostingEnvironment.MapPath("~/");
            if (basepath == null)
            {
                basepath = AppDomain.CurrentDomain.BaseDirectory;
                System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(basepath);
                basepath = dir.Parent.Parent.FullName + "\\WebApi\\";
            }
            return basepath;
        }

    }

   

}