using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hit.Services.Models.Models.ProtelMappingsToPylon
{
    public class LizenzModel
    {
        public int mpehotel { get; set; } //(int, not null)
        public string hotel { get; set; } //(varchar(100), not null)
        public string Short { get; set; } //(varchar(100), not null)
    }
}
