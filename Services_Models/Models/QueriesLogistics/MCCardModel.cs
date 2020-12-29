using Hit.Services.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hit.Services.Models.Models.QueriesLogistics
{
    public class MCCardModel
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
        public decimal amount { get; set; }
        public payment_kind kind { get; set; }
        public int payment_method { get; set; }
    }

    public class MCCardListModel
    {
        DateTime date { get; set; }
        int mpehotel { get; set; }
        public List<MCCardModel> mcLines { get; set; }
    }
}
