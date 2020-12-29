using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hit.Services.Models.Models.QueriesLogistics
{
    public class MCDepositInhouseModel
    {
        public DateTime datum { get; set; }
        public int mpehotel { get; set; }
        public int leist_ref { get; set; }
        public string log_account { get; set; }
        public int kdnr { get; set; }
        public string name1 { get; set; }
        public string afmno { get; set; }
        public string fibudeb { get; set; }
        public int typ { get; set; }
        public decimal amount { get; set; }
        public int payment_method { get; set; }
        /// <summary>
        /// 0: credit deposit account, 1: debit inhouse account
        /// </summary>
        public int debit { get; set; }
    }

    public class MCDepositInhouseListModel
    {
        DateTime date { get; set; }
        int mpehotel { get; set; }
        public List<MCDepositInhouseModel> mcLines { get; set; }
    }
}
