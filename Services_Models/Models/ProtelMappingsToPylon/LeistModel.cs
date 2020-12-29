using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hit.Services.Models.Models.ProtelMappingsToPylon
{
    public class LeistModel
    {
        public DateTime datum { get; set; } //(datetime, not null)
        //public int mpehotel { get; set; } //(int, not null)
        public int rechnr { get; set; } //(int, not null)
        //public int fisccode { get; set; } //(int, not null)
        public int Ref { get; set; } //(int, not null)
        public int kundennr { get; set; } //(int, not null)
        public decimal epreis { get; set; } //(decimal(19,2), not null)
        public int anzahl { get; set; } //(int, not null)
        public string zimmer { get; set; } //(varchar(15), not null)
        public int rechnung { get; set; } //(int, not null)
        public decimal mwstsatz { get; set; } //(decimal(19,4), not null)
        public int vatno { get; set; } //(int, not null)
        public decimal tax1 { get; set; } //(decimal(10,6), not null)
        public int rkz { get; set; } //(int, not null)
        public int ukto { get; set; } //(int, not null)
        public int voidref { get; set; } //(int, not null)
        public string voidreason { get; set; } //(varchar(250), not null)
    }
}
