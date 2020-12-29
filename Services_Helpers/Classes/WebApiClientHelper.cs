using Hit.Services.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Formatting;
using System.Xml.Serialization;
using Hit.Services.Models.Enums;

namespace Hit.Services.Helpers.Classes.Classes
{
    public class WebApiClientHelper
    {

        /// <summary>
        /// Get Request
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="user">user and password for Authentication Header. Format for Basic: "Username:Password", Format for OAuth2: "Bearer  ZTdmZmY1Zjc5MTQ4NDQ5ZTEzMzIyZTOQ"</param>
        /// <param name="headers">custom headers (key: header name, value: header value)</param>
        /// <param name="returnCode">return Code (200, 400,...)</param>
        /// <param name="ErrorMess">Error message (for 200 ErrorMess="")</param>
        /// <param name="mediaType">mediaType. Default: "application/json" . Other Values:  application/xml  </param>
        /// <param name="authenticationType">Type of authentication (Basic or OAuth2) </param>
        /// <returns>the result or the request as string </returns>
        public string GetRequest(string url, string user, Dictionary<string, string> headers, out int returnCode, out string ErrorMess, string mediaType = "application/json", string authenticationType="Basic")
        {
            string result = null;
            HttpRequestMessage request;
            request = new HttpRequestMessage();
            request.RequestUri = new Uri(url);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType));
            request.Method = HttpMethod.Get;
            // HttpClient client = new HttpClient();
            using (HttpClient client = new HttpClient())
            {
                //1. Greate  headers
                setHeaders(client, authenticationType, user, headers);

                //3. Send Request
                using (HttpResponseMessage response = client.SendAsync(request).Result)
                {
                    var readAsStringAsync = response.Content.ReadAsStringAsync();
                    returnCode = response.StatusCode.GetHashCode();
                    if (returnCode == 200)
                    {
                        ErrorMess = "";
                        result = readAsStringAsync.Result;
                    }
                    else
                    {
                        if (readAsStringAsync.Result is HttpMessage)
                        {
                            //if(mediaType == "application/json")
                                ErrorMess = Newtonsoft.Json.JsonConvert.DeserializeObject<HttpMessage>(readAsStringAsync.Result).Message;
                            //else
                            //{
                            //    XmlSerializer serializer = new XmlSerializer(typeof(HttpMessage));
                            //    serializer.Deserialize(readAsStringAsync.Result)
                            //}
                        }
                          
                        else
                            ErrorMess = readAsStringAsync.Result;
                    }
                }
            }
            request.Dispose();
            return result;
        }


        /// <summary>
        /// Post Request
        /// </summary>
        /// <param name="model">model to Post</param>
        /// <param name="url">url</param>
        /// <param name="user">user and password for Authentication Header. Format "Username + : + Password"</param>
        /// <param name="headers">custom headers (key: header name, value: header value)</param>
        /// <param name="returnCode">return Code (200, 400,...)</param>
        /// <param name="ErrorMess">Error message (for 200 ErrorMess="")</param>
        /// <param name="mediaType">mediaType. Default: "application/json". Other Values: application/xml </param>
        /// <param name="authenticationType">Type of authentication (Basic or OAuth2) </param>
        /// <returns>the result or the request as string </returns>
        public string PostRequest<T>( T model, string url, string user, Dictionary<string, string> headers, out int returnCode, out string ErrorMess, string mediaType = "application/json", string authenticationType = "Basic")
        {
            string result = null;
            HttpRequestMessage request;
            request = new HttpRequestMessage();
            request.RequestUri = new Uri(url);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType));
            request.Method = HttpMethod.Post;
            MediaTypeFormatter formatter;
            if (mediaType == "application/json")
                formatter = new JsonMediaTypeFormatter();
            else
                formatter = new XmlMediaTypeFormatter();
             
            request.Content = new ObjectContent<T>(model, formatter);

            using (HttpClient client = new HttpClient())
            {
                //1. Greate headers
                setHeaders(client, authenticationType, user, headers);

                //2. Send Request
                using (HttpResponseMessage response = client.SendAsync(request).Result)
                {
                    var readAsStringAsync = response.Content.ReadAsStringAsync();
                    returnCode = response.StatusCode.GetHashCode();
                    if (returnCode == 200)
                    {
                        ErrorMess = "";
                        result = readAsStringAsync.Result;
                    }
                    else
                    {
                        if (readAsStringAsync.Result is HttpMessage)
                            ErrorMess = Newtonsoft.Json.JsonConvert.DeserializeObject<HttpMessage>(readAsStringAsync.Result).Message;
                        else
                            ErrorMess = readAsStringAsync.Result;
                    }
                }
            }
            request.Dispose();
            return result;

        }



        /// <summary>
        /// Patch Request
        /// </summary>
        /// <param name="model">model to Patch</param>
        /// <param name="url">url</param>
        /// <param name="user">user and password for Authentication Header. Format "Username + : + Password"</param>
        /// <param name="headers">custom headers (key: header name, value: header value)</param>
        /// <param name="returnCode">return Code (200, 400,...)</param>
        /// <param name="ErrorMess">Error message (for 200 ErrorMess="")</param>
        /// <param name="mediaType">mediaType. Default: "application/json". Other Values: application/xml </param>
        /// <param name="authenticationType">Type of authentication (Basic or OAuth2) </param>
        /// <returns>the result or the request as string </returns>
        public string PatchRequest<T>(T model, string url, string user, Dictionary<string, string> headers, out int returnCode, out string ErrorMess, string mediaType = "application/json", string authenticationType = "Basic")
        {
            string result = null;
            HttpRequestMessage request;
            request = new HttpRequestMessage();
            request.RequestUri = new Uri(url);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType));
            request.Method = new HttpMethod("PATCH");
            MediaTypeFormatter formatter;
            if (mediaType == "application/json")
                formatter = new JsonMediaTypeFormatter();
            else
                formatter = new XmlMediaTypeFormatter();

            request.Content = new ObjectContent<T>(model, formatter);

            using (HttpClient client = new HttpClient())
            {
                //1. Greate headers
                setHeaders(client, authenticationType, user, headers);

                //2. Send Request
                using (HttpResponseMessage response = client.SendAsync(request).Result)
                {
                    var readAsStringAsync = response.Content.ReadAsStringAsync();
                    returnCode = response.StatusCode.GetHashCode();
                    if (returnCode >= 200 && returnCode <= 299)
                    {
                        ErrorMess = "";
                        result = readAsStringAsync.Result;
                    }
                    else
                    {
                        if (readAsStringAsync.Result is HttpMessage)
                            ErrorMess = Newtonsoft.Json.JsonConvert.DeserializeObject<HttpMessage>(readAsStringAsync.Result).Message;
                        else
                            ErrorMess = readAsStringAsync.Result;
                    }
                }
            }
            request.Dispose();
            return result;

        }




        /// <summary>
        /// Create Headers for the rest client
        /// </summary>
        /// <param name="client">HttpClient</param>
        /// <param name="authenticationType">type of authentication (Basic or OAuth2)</param>
        /// <param name="user">user and password or token for Authentication Header. Format for Basic: "Username:Password", Format for OAuth2: "Bearer  ZTdmZmY1Zjc5MTQ4NDQ5ZTEzMzIyZTOQ"</param>
        /// <param name="headers">custom headers </param>
        private void setHeaders(HttpClient client, string authenticationType, string user, Dictionary<string, string> headers)
        {
            //1. Greate Authorization header
            if (!string.IsNullOrEmpty(user))
            {
                switch (authenticationType)
                {
                    case "Basic":
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes(user)));
                        break;
                    case "OAuth2":
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization", user);
                        break;
                }
            }

            //2. Greate custom headers
            if (headers != null)
            {
                foreach (string key in headers.Keys)
                {
                    if (key != null && headers[key] != null)
                        client.DefaultRequestHeaders.Add(key, headers[key]);
                }
            }
        }


        private class HttpMessage
        {
            public string Message { get; set; }
        }
    }
 
}
