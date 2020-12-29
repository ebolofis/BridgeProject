using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hit.Services.Models.Models.SQL
{
   public class ExportFileConfigModel
    {
        /// <summary>
        /// DB's Connection String
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// path for script file
        /// </summary>
        public string ScriptFilePath { get; set; }

        /// <summary>
        /// path for the file to export
        /// </summary>
        public string ExportFilePath { get; set; }

        /// <summary>
        /// export's file type: 
        /// xml: xml file.
        /// json: json file,
        /// separated values: sp[separator], ex: sp;   or   sp,
        /// fixed length values: fl:[list of lenghts], ex: fl:10,6,40,3,15
        /// </summary>
        public string FileType { get; set; }

        /// <summary>
        /// DateTime Format  ex: yyyyMMddHHmmss
        /// </summary>
        public string DateTimeFormat { get; set; }

        /// <summary>
        /// numeric format   ex: ####.##
        /// </summary>
        public string NumericFormat { get; set; }

    }
}
