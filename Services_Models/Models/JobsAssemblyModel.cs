using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Hit.Services.Models.Models
{
    public class JobsAssemblyModel
    {
        /// <summary>
        /// Determine Jobs Origin
        /// 1.For Local Jobs, 2.For External Jobs, 3.For WebApi 
        /// </summary>
        public int JobsOrigin { get; set; }

        /// <summary>
        /// Type
        /// </summary>
        public Type Type { get; set; }

        /// <summary>
        /// Class Full Name
        /// </summary>
        public string ClassName { get; set; }

        /// <summary>
        /// reference assembly
        /// </summary>
        public Assembly Assembly { get; set; }
    }
}
