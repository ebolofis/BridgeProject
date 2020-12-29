using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hit.Services.Models.Models.ProtelMappingsToPylon
{
    public class RechhistModel
    {
        //Ref,mpehotel,rechnr,resno,invindex,voidinv,fisccode,kdnr,formnr,Void,deposit,name,username,datum,sum_zahl,sum_belast 
        public int Ref { get; set; } //(int, not null)
        public DateTime datum { get; set; } //(datetime, not null)
        public int mpehotel { get; set; } //(int, not null)
        public int rechnr { get; set; } //(int, not null)
        public int resno { get; set; } //(int, not null)
        public int invindex { get; set; } //(int, not null)
        public int voidinv { get; set; } //(int, not null)
        public int fisccode { get; set; } //(int, not null)
        public int kdnr { get; set; } //(int, not null)
        public int Void { get; set; } //(int, not null)
        public decimal sum_zahl { get; set; } //(decimal(19,2), not null)
        public decimal sum_belast { get; set; } //(decimal(19,2), not null)
    }
}
