using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Hit.Services.WebApi.Modules
{
    public class TraceHandler : DelegatingHandler
    {
        log4net.ILog logger;
        public TraceHandler()
        {
            this.logger = log4net.LogManager.GetLogger(this.GetType());
        }
        protected async override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            string debugInfo;

            //1. Request
            try
            {
                if (logger.IsInfoEnabled)
                {
                    debugInfo = "";
                    string requestBody = await request.Content.ReadAsStringAsync();

                    debugInfo = string.Format("Request  :  [{0}]  [{1}] [{2}] - {3}", request.Method, DetermineCompName(), request.RequestUri, requestBody).Replace("\\r\\n", Environment.NewLine);
                    if (debugInfo.Length > 1000) debugInfo = debugInfo + Environment.NewLine;
                    if (request.Method == HttpMethod.Options || request.Method == HttpMethod.Get)
                        logger.Debug(debugInfo);
                    else
                        logger.Info(debugInfo);

                }
            }
            catch (Exception ex)
            {
                logger.Error("Error tracking Request: " + ex.ToString());
            }


            //2.>>> Call the inner handler. <<<<<<
            var response = await base.SendAsync(request, cancellationToken);


            //3. Response 
            try
            {
                //3a.
                if (response.Content != null && logger.IsInfoEnabled && (int)response.StatusCode < 400)  //(response.Content != null && (logger.IsDebugEnabled || logger.IsInfoEnabled) && (int)response.StatusCode < 400)
                {

                    if ((request.Method == HttpMethod.Options || request.Method == HttpMethod.Get) && logger.IsDebugEnabled)
                    {
                        debugInfo = await getResponseBody(request, response);
                        logger.Debug(debugInfo);
                    }
                    else if (request.Method != HttpMethod.Options)
                    {
                        debugInfo = await getResponseBody(request, response);
                        logger.Info(debugInfo);
                    }
                    // logger.Info(debugInfo);


                }
                //3b.
                if (response.Content != null && (int)response.StatusCode >= 400)
                {
                    debugInfo = string.Format("Response : [{0}] [{1}]  [{2}]  -- ", request.Method, response.StatusCode, request.RequestUri);
                    try
                    {
                        Stream dataStream = await response.Content.ReadAsStreamAsync();
                        StreamReader reader = new StreamReader(dataStream);
                        string strResponse = reader.ReadToEnd();
                        strResponse = strResponse.Replace("\\r\\n", Environment.NewLine);
                        debugInfo = debugInfo + strResponse;
                    }
                    catch (Exception ex1)
                    {
                        logger.Error("Error reading Response.Content : " + ex1.ToString());
                    }


                    logger.Error(debugInfo);
                }

                //3c.
                if (response.Content == null && logger.IsInfoEnabled)
                {
                    if (request.Method == HttpMethod.Options && logger.IsDebugEnabled)
                    {
                        var warmInfo = string.Format("Response : [{0}] [{1}]  [{2}] --- {3}", request.Method, response.StatusCode, request.RequestUri, response.ToString()).Replace("\\r\\n", Environment.NewLine);
                        logger.Debug(warmInfo);
                    }
                    else if (request.Method != HttpMethod.Options)
                    {
                        var warmInfo = string.Format("Response : [{0}] [{1}]  [{2}] --- {3}", request.Method, response.StatusCode, request.RequestUri, response.ToString()).Replace("\\r\\n", Environment.NewLine);
                        logger.Info(warmInfo);
                    }

                }
            }
            catch (Exception ex)
            {
                logger.Error("Error tracking Response: " + ex.ToString());
            }

            return response;
        }

        private async Task<string> getResponseBody(HttpRequestMessage request, HttpResponseMessage response)
        {
            string responseBody = await response.Content.ReadAsStringAsync();
            if (responseBody.Length > 1000) responseBody = responseBody.Substring(0, 1000) + "...";
            string debugInfo = string.Format("Response : [{0}] [{1}]  [{2}] - {3}", request.Method, response.StatusCode, request.RequestUri, responseBody).Replace("\\r\\n", Environment.NewLine);
            if (debugInfo.Length > 1000) debugInfo = debugInfo + Environment.NewLine;

            return debugInfo;
        }

        public void Debug(string message)
        {
            ThreadPool.QueueUserWorkItem(task => logger.Debug(message));
        }

        public void Info(string message)
        {
            ThreadPool.QueueUserWorkItem(task => logger.Info(message));
        }
        public void Error(string message)
        {
            ThreadPool.QueueUserWorkItem(task => logger.Info(message));
        }


        /// <summary>
        /// Get client's computer name
        /// </summary>
        /// <returns></returns>
        public static string DetermineCompName()
        {
            try
            {
                string ip = HttpContext.Current != null ? HttpContext.Current.Request.UserHostAddress : "";
                //IPAddress myIP = IPAddress.Parse(ip);
                //IPHostEntry GetIPHost = Dns.GetHostEntry(myIP);
                //List<string> compName = GetIPHost.HostName.ToString().Split('.').ToList();
                //return compName.First();
                return ip;
            }
            catch
            {
                return "ClientNameNotFound";
            }

        }
    }
}