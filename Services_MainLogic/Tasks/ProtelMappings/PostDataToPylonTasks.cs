using Hit.Services.Core;
using Hit.Services.Models.Models.ProtelMappingsToPylon;
using Hit.Services.Models.Models.QueriesLogistics;
using Hit.Services.DataAccess.DT.ProtelToPylonMappings;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hit.Services.MainLogic.Tasks.ProtelMappings
{
    public class PostDataToPylonTasks
    {

        SettingsModel settings;

        ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        PostDataToPylonDT DTs;

        MCDataDT mcDT;

        public PostDataToPylonTasks(SettingsModel settings)
        {
            this.settings = settings;

            DTs = new PostDataToPylonDT(settings);

            mcDT = new MCDataDT(settings);
        }

        public List<PostDataToPylonModel> PostDataToPylon(eaDateModel model)
        {
            return DTs.PostDataToPylon(model);
        }

        public List<MCDataModel> PostMCDataToPylon(eaDateModel model)
        {
            return mcDT.mcData(model);
        }

        public List<SalesJournalAAModel> GetsalesJournalAA()
        {
            return DTs.GetsalesJournalAA();
        }
        public List<eaSalesJournalARModel> GetsalesJournalAR()
        {
            return DTs.GetsalesJournalAR();
        }
        public bool FlagSentRecords(List<eaMessagesStatusModel> listtobeflaged)
        {
            return DTs.FlagSentRecords(listtobeflaged);
        }
        public void GetDocsForSpecifiedRange(eaDateModel model)
        {
            DTs.GetDocsForSpecifiedRange(model);
        }
        public void Save_SalesJournalDepositOut_To_hit_estia_sales_journal_deposit_out(eaDateModel model)
        {
            DTs.Save_SalesJournalDepositOut_To_hit_estia_sales_journal_deposit_out(model);
        }
        public void GetMainCourante()
        {
            DTs.GetMainCourante();
        }
        public List<SalesJournalAAMain> GetSalesJournalAADateRange(eaDateModel model)
        {
          return  DTs.GetSalesJournalAADateRange(model);
        }
        public List<eaSalesJournalARMain> GetDebtorsSalesJournalAR(eaDateModel model)
        {
            return DTs.GetDebtorsSalesJournalAR(model);
        }

        public List<KundenModel> GetCustomers(eaDateModel model)
        {
            return DTs.GetCustomers(model);
        }
    }
}
