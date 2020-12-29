using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CsvHelper;

namespace Hit.Services.Helpers.Classes
{
    /// <summary>
    /// Class that Casts dynamic types
    /// </summary>
   public class ConvertDynamicHelper
    {

        /// <summary>
        /// Convert a IEnumerable(dynamic) to List(IDictionary(string, dynamic)) [Required if you want to save data to a file]
        /// </summary>
        /// <param name="list">a dynamic IEnumerable  (see RunSelect)</param>
        /// <returns></returns>
        public List<IDictionary<string, dynamic>> ToListDictionary(IEnumerable<dynamic> list)
        {
            List<IDictionary<string, dynamic>> results = new List<IDictionary<string, dynamic>>();
            foreach (var item in list.ToList())
            {
                results.Add(Mapper.Map<IDictionary<string, dynamic>>(item));
            }
            return results;
        }

        /// <summary>
        /// Convert a dynamic object to IDictionary(string, dynamic) 
        /// </summary>
        /// <param name="obj">dynamic object</param>
        /// <param name="header">the list containing the names of the desired properties (keys). If null then keep the original names</param>
        /// <returns></returns>
        public IDictionary<string, dynamic> ToDictionary(dynamic obj, List<string> header = null)
        {

            IDictionary<string, dynamic> data = Mapper.Map<IDictionary<string, dynamic>>(obj);

            if (header != null)
            {
                int i = 0;
                List<string> keys = data.Keys.ToList();
                IDictionary<string, dynamic> result = new Dictionary<string, dynamic>();
                foreach (string item in header)
                {
                    result.Add(item, data[keys[i]]);
                    i++;
                }
                return result;
            }
            else
                return data;
        }

        /// <summary>
        /// Convert a IEnumerable(dynamic) to List of a Model
        /// </summary>
        /// <param name="list">a dynamic IEnumerable  (see RunSelect)</param>
        /// <returns></returns>
        public List<T> ToModelList<T>(IEnumerable<dynamic> list)
        {
            return Mapper.Map<List<T>>(list);
        }

    }
}
