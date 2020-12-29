using Hit.Services.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hit.Services.Models.Models.QueriesLogistics
{
    public class MCDebitorModel
    {
        public DateTime datum { get; set; }
        public int mpehotel { get; set; }
        public int rechnr { get; set; }
        public int fiscalcode { get; set; }
        public string log_account { get; set; }
        public int kdnr { get; set; }
        public string name1 { get; set; }
        public string afmno { get; set; }
        public string fibudeb { get; set; }
        public int typ { get; set; }
        public string plz { get; set; }
        public string strasse { get; set; }
        public string ort { get; set; }
        public string land { get; set; }
        public int guest_id { get; set; }
        public string guest_name { get; set; }
        public decimal amount { get; set; }
        public payment_kind kind { get; set; }
        public int payment_method { get; set; }
    }

    public class MCDebitorListModel
    {
        DateTime date { get; set; }
        int mpehotel { get; set; }
        public List<MCDebitorModel> mcLines { get; set; }
    }
}
