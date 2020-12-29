using Hit.Services.Models.Models.QueriesLogistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hit.Services.Models.Models.ProtelMappingsToPylon
{
    public class MCDataModel
    {
        public DateTime date { get; set; }
        public int mpehotel { get; set; }
        public List<KundenModel> customersList { get; set; }
        public List<MCIncomeModel> mcIncomeList { get; set; }
        public List<MCCashModel> mcCashList { get; set; }
        public List<MCCardModel> mcCardList { get; set; }
        public List<MCBankModel> mcBankList { get; set; }
        public List<MCDebitorModel> mcDebitorList { get; set; }
        public List<MCDepositNewModel> mcDepositNewList { get; set; }
        public List<MCDepositInhouseModel> mcDepositInhouseList { get; set; }
        public List<eaSalesJournalARModel> arPayoffList { get; set; }
    }
}
