using Hit.Services.Models.Models.QueriesLogistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hit.Services.Models.Models.ProtelMappingsToPylon
{
    public class PostDataToPylonModel
    {

        public List<KundenModel> Kunden { get; set; }
        public List<LeisthisModel> Leisthis { get; set; }
        public List<RechhistModel> Rechhist { get; set; }

        /// <summary>
        ///  QUERIES LOGISTICS MODELS 
        /// </summary>
        public List<SalesJournalModel> SalesJournal { get; set; }
        public List<SalesJournalAAModel> SalesJournalAA { get; set; }
        public List<SalesJournalBankModel> SalesJournalBank { get; set; }
        public List<SalesJournalCardModel> SalesJournalCard { get; set; }
        public List<SalesJournalCashModel> SalesJournalCash { get; set; }
        public List<SalesJournalCreditModel> SalesJournalCredit{ get; set; }
        public List<SalesJournalDepositOut> SalesJournalDepositOut { get; set; }
        public List<SalesJournalDepositOutSoftModel> SalesJournalDepositOutSoft{ get; set; }
       // public List<SalesJournalARModel> SalesJournalARModel { get; set; }

    }
}
