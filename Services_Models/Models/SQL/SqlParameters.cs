using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hit.Services.Models.Models.SQL
{

    /// <summary>
    /// Class that represent a record in table SqlParameters in HitServicesDB (HangFireDB)
    /// </summary>
    public class SqlParameters
    {
        public int Id { get; set; }
        public string SettingsFile { get; set; }
        public string Parameter { get; set; }
        public string Value { get; set; }
        public string OldValue { get; set; }
    }
}
