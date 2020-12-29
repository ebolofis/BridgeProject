using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hit.Services.Models.Models.QueriesLogistics
{
    public class SalesJournalCreditModel
    {
        public DateTime datum { get; set; }// DATETIME, --rechnr.datum
        public DateTime leistdatum { get; set; }
        public int rechnr { get; set; }
        public int mpehotel { get; set; }
        public int fiscalcode { get; set; }// doctype
        public string invoice_status { get; set; }// NVARCHAR(50),
        public int void_rechnr { get; set; }// INT, --rechnr of voiding
        public string fibukto { get; set; }// NVARCHAR(100),
        public int payment_method { get; set; }// INT, --zanr
        public decimal total_amount { get; set; }// DECIMAL,
        public string zimmer { get; set; }
        public int customer_id { get; set; }// INT, --kdnr
        public string name1 { get; set; }// NVARCHAR(100),
        public string name2 { get; set; }// NVARCHAR(100),
        public string vorname { get; set; }// NVARCHAR(100),
        public string kepyo { get; set; }
        public string afmno { get; set; }// NVARCHAR(100),
        public string doy { get; set; }// NVARCHAR(100),
        public string land { get; set; }// NVARCHAR(100),
        public string strasse { get; set; }// NVARCHAR(100),
        public string ort { get; set; }// NVARCHAR(100),
        public string plz { get; set; }// NVARCHAR(100),
        public string fibudeb { get; set; }// NVARCHAR(100),
    }
}
