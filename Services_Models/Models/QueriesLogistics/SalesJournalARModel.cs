using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hit.Services.Models.Models.QueriesLogistics
{
    /// <summary>
    /// Model used for BOTH Main Courant AND Sales Journal
    /// </summary>
    public class eaSalesJournalARModel
    {
        public DateTime belegdat { get; set; } //--l.rdatum
        public DateTime datum { get; set; }//            datum DATETIME, --rechnr.datum
        public int mpehotel { get; set; }// INT,
        public int typ { get; set; }//, --zahlart type
        public string fibukto { get; set; }
        public int rechnr { get; set; }
        public decimal total_amount { get; set; }
        public int fiscalcode { get; set; }
        public int customer_id { get; set; }
        public int payment_method { get; set; }
        public string name1 { get; set; }
        public string name2 { get; set; }
        public string afmno { get; set; }
        public string fibudeb { get; set; }
        public string doy { get; set; }
        public string land { get; set; }
        public string strasse { get; set; }
        public string ort { get; set; }
        public string plz { get; set; }
    }
}
