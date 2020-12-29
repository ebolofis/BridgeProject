using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hit.Services.Helpers.Classes
{
    /// <summary>
    /// Class that serialise/deserialise objects to json and vise versa
    /// </summary>
   public class JsonHelper
    {
        /// <summary>
        /// Serialise an object to json string
        /// </summary>
        /// <param name="model">model to serialise</param>
        /// <returns></returns>
        public string GetJson(object model)
        {
           return Newtonsoft.Json.JsonConvert.SerializeObject(model);
        }

        /// <summary>
        /// Deserialise a json string to model
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json">json to deserialise</param>
        /// <returns></returns>
        public T GetModel<T>(string json)
        {
          return  Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
        }

    }
}
