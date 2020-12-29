using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hit.Services.Models.Models.ProtelMappingsToPylon
{
    public class FiscalcdModel
    {

        public int Ref { get; set; } //(int, not null)
        public int mpehotel { get; set; } //(int, not null)
        public DateTime validfrom { get; set; } //(datetime, not null)
        public DateTime validto { get; set; } //(datetime, not null)
        public string text { get; set; } //(varchar(40), not null)
        public string code { get; set; } //(varchar(10), not null)
        public int counter { get; set; } //(int, not null)
        public int clx { get; set; } //(int, not null)
        public int clxcode { get; set; } //(int, not null)
    }
}
