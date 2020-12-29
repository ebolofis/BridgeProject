using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Hit.Services.Models.Models
{
    public class ExtAssemblyModel
    {
        public List<string> ClassNames { get; set; }

        public Type Type { get; set; }

        public Assembly Assembly { get; set; }
    }
}
