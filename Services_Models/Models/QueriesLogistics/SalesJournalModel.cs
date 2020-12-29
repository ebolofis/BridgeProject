using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hit.Services.Models.Models.ProtelMappingsToPylon
{
    public class SalesJournalModel
    {
        public DateTime datum { get; set; } //(datetime, not null)
        public int rechnr { get; set; } //(int, not null)
        public int mpehotel { get; set; } //(int, not null)
        public int fiscalcode { get; set; } //(int, not null)
        public string invoiceStatus { get; set; } //(varchar(16), not null)
        public int void_rechnr { get; set; } //INT, --rechnr of voiding
        public string log_account { get; set; } //(varchar(50), null) , u.exTAXtext

        public int transaction_id { get; set; } // INT, --service
        public int vat_id { get; set; } // INT, --vat
        public decimal total_amount { get; set; }
        public decimal total_net_amount { get; set; }
        public decimal total_vat_amount { get; set; }
        public decimal city_tax_percent { get; set; }
        public decimal city_tax_amount { get; set; }

        public int customer_id { get; set; }// INT, --kdnr
        public string name1 { get; set; }// NVARCHAR(100),
        public string name2 { get; set; }// NVARCHAR(100),
        public string vorname { get; set; }// NVARCHAR(100),
        public string kepyo { get; set; }// NVARCHAR(100),
        public string afmno { get; set; }// NVARCHAR(100),
        public string doy { get; set; }// NVARCHAR(100), --k.vatno
        public string land { get; set; }// NVARCHAR(100),
        public string strasse { get; set; } // NVARCHAR(100), 
        public string ort { get; set; }     // NVARCHAR(100),
        public string plz { get; set; }    // NVARCHAR(100),
        public string fibudeb { get; set; }  // NVARCHAR(100),






    }
}
