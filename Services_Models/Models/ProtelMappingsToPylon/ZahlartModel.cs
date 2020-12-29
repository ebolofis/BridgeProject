using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hit.Services.Models.Models.ProtelMappingsToPylon
{
    public class ZahlartModel
    {
        public int zanr { get; set; } //(int, not null) 
        public string za { get; set; } //(varchar(10), not null)
        public string bez { get; set; } //(varchar(40), not null)
        public int typ { get; set; } //(int, not null)
        public string fibukto { get; set; } //(varchar(20), not null)
    }
}
