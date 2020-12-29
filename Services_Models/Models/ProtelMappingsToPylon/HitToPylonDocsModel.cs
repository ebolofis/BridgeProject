using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hit.Services.Models.Models.ProtelMappingsToPylon
{
    public enum MatchingKindEnum { Generic = 0, Customers = 1, SalesEntries = 2, MainCourante = 3, CustomerImport = 4, CustomerExport = 5, Deposits = 6, DebitorReceivables = 7, MainCourantePay = 10 }
    public class HitToPylonDocsModel
    { 
        public int? kdnr { get;set; }
        public int rechnr { get; set; } //(int, not null)
        public int fisccode { get; set; } //(int, not null)
        public int mpehotel { get; set; } //(int, not null)
        public int is_sent { get; set; } //(int, not null)
        public DateTime date_created { get; set; } //(datetime, not null)
        
        public MatchingKindEnum? Kind { get; set; }

    }
}
