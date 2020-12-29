using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hit.Services.Models.Models.ProtelMappingsToPylon
{
    public class KundenModel
    {
        //kdnr,mpehotel,name1,vorname,strasse,plz,ort,land,gender,telefonnr,funktel,email,deleted,fibudeb,vatno,afmno
        public int kdnr { get; set; } //(int, not null)
        public int mpehotel { get; set; } //(int, not null)
        public string name1 { get; set; } //(varchar(80), not null)
        public string name2 { get; set; } //(varchar(80), not null)
        public string vorname { get; set; } //(varchar(80), not null)
        public string strasse { get; set; } //(varchar(80), not null)
        public string plz { get; set; } //(varchar(17), not null)
        public string ort { get; set; } //(varchar(50), not null)
        public string land { get; set; } //(varchar(80), not null)
        public string telefonnr { get; set; } //(varchar(50), not null)
        public string contract { get; set; } //(varchar(20), not null)
        public string vatno { get; set; } //(varchar(30), not null)
        public string afmno { get; set; } //(varchar(30), not null)
        public string fibudeb { get; set; } //(varchar(20), not null)
    }
}
