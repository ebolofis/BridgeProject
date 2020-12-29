using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hit.Services.Models.Models.QueriesLogistics
{
    public class SalesJournalDepositOutSoftModel
    {
        public DateTime datum { get; set; }// DATETIME, --rechnr.datum
        public DateTime leistdatum { get; set; }// DATETIME, --l.rdatum
        public int rechnr { get; set; }// INT,
        public int mpehotel { get; set; }// INT,
        public int fiscalcode { get; set; }// INT, --doctype
        public string fibukto { get; set; }// NVARCHAR(100), --z.fibukto
        public int payment_method { get; set; }// INT, --zanr
        public decimal total_amount { get; set; }// DECIMAL,
        public int customer_id { get; set; }// INT, --kdnr
        public string name1 { get; set; }// NVARCHAR(100),
        public int invoice_id { get; set; }// INT, --kdnr
        public string invoice_name1 { get; set; }// NVARCHAR(100),
        public string invoice_name2 { get; set; }// NVARCHAR(100),
        public string invoice_afmno { get; set; }// NVARCHAR(100),
        public string invoice_land { get; set; }// NVARCHAR(100),
    }
}
