using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hit.Services.Models.Models.QueriesLogistics
{
    public class SalesJournalAAModel
    {
        public DateTime datum { get; set; }// DATETIME, --rechnr.datum
        public DateTime bankdatum { get; set; }// DATETIME, --l.rdatum
        public int mpehotel { get; set; }
        public int typ { get; set; }// int, --zahlart type
        public string fibukto { get; set; }// NVARCHAR(100),
        public int payment_method { get; set; }// INT, --zanr
        public decimal total_amount { get; set; }
        public int customer_id { get; set; }// INT, --kdnr
        public string name1 { get; set; }// NVARCHAR(100),
        public string name2 { get; set; }// NVARCHAR(100),
        public string afmno { get; set; }// NVARCHAR(100),
        public int agent_id { get; set; }// INT, --kdnr
        public string agent_name1 { get; set; }// NVARCHAR(100),
        public string agent_name2 { get; set; }// NVARCHAR(100),
        public string agent_afmno { get; set; }// NVARCHAR(100),
        public int company_id { get; set; }// INT, --kdnr
        public string company_name1 { get; set; }// NVARCHAR(100),
        public string company_name2 { get; set; }// NVARCHAR(100),
        public string company_afmno { get; set; }// NVARCHAR(100)
    }
}
