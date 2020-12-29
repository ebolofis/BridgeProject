using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hit.Services.Models.Models.ProtelMappingsToPylon
{
    public class MwstModel
    {
        public int satznr { get; set; } //(int, not null)
        public decimal satz { get; set; } //(decimal(10,5), not null)
       
    }
}
