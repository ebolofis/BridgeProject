using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hit.Services.Models.CustomAnotations
{
    /// <summary>
    /// Attribute that provides  descriptions for Jobs and Controllers
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class DescribeAttribute : Attribute
    {
        /// <summary>
        /// Type: 'Job' or 'Controller'
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Help Description for the Job or the Controller
        /// </summary>
        public string Description { get; set; }


    }
}
