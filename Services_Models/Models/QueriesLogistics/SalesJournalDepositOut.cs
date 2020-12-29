using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hit.Services.Models.Models.QueriesLogistics
{
    public class SalesJournalDepositOut
    {
        public DateTime datum { get; set; }// DATETIME, --rechnr.datum
        public DateTime leistdatum { get; set; }// DATETIME,
        public int rechnr { get; set; }// INT,
        public int mpehotel { get; set; }// INT,
        public int fiscalcode { get; set; }// INT, --doctype
        public string fibukto { get; set; }// NVARCHAR(100),
        public decimal total_amount { get; set; }// DECIMAL,
        public int customer_id { get; set; }// INT, --kdnr
        public string name1 { get; set; }// NVARCHAR(100),
        public string name2 { get; set; }// NVARCHAR(100),
        public string afmno { get; set; }// NVARCHAR(100),
        public int agent_id { get; set; }// INT, --kdnr
        public string agent_name1 { get; set; }// NVARCHAR(100),
        public string agent_name2 { get; set; }// NVARCHAR(100),
        public string agent_afmno { get; set; }// NVARCHAR(100),
        public int company_id { get; set; }
        public string company_name1 { get; set; }// NVARCHAR(100),
        public string company_name2 { get; set; }// NVARCHAR(100),
        public string company_afmno { get; set; }// NVARCHAR(100),

    }
}
