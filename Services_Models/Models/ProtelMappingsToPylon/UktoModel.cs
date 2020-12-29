using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hit.Services.Models.Models.ProtelMappingsToPylon
{
    public class UktoModel
    {
        public string kto { get; set; } //(varchar(10), not null)
        public string bez { get; set; } //(varchar(40), not null)
        public int ktonr { get; set; } //(int, not null)

    }
}
