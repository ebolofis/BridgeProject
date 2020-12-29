using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hit.Services.Models.Models.QueriesLogistics
{
    public class MCIncomeModel
    {
        public DateTime datum { get; set; }
        public int mpehotel { get; set; }
        public int vatno { get; set; }
        public string department { get; set; }
        public int transaction_id { get; set; }
        public int line { get; set; }
        public string log_account { get; set; }
        public decimal amount { get; set; }
        public decimal tax { get; set; }
    }

    public class MCIncomeListModel
    {
        DateTime date { get; set; }
        int mpehotel { get; set; }
        public List<MCIncomeModel> mcLines { get; set; }
    }
}
