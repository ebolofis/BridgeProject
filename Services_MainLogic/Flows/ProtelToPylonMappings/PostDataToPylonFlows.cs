using Hit.Services.Core;
using Hit.Services.Helpers.Classes.Classes;
using Hit.Services.MainLogic.Tasks.ProtelMappings;
using Hit.Services.Models.Models.ProtelMappingsToPylon;
using Hit.Services.Models.Models.QueriesLogistics;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hit.Services.MainLogic.Flows.ProtelToPylonMappings
{
    public class PostDataToPylonFlows
    {

        PostDataToPylonTasks Tasks;
         SettingsModel settings;
        ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        WebApiClientHelper apiHelper;
        public PostDataToPylonFlows(SettingsModel settings)
        {
            this.settings = settings;
           Tasks = new PostDataToPylonTasks(settings);
        }

        public List<PostDataToPylonModel> PostDataToPylon(eaDateModel model)
        {
            return Tasks.PostDataToPylon(model);
        }

        public List<MCDataModel> PostMCDataToPylon(eaDateModel model)
        {
            return Tasks.PostMCDataToPylon(model);
        }

        public List<SalesJournalAAModel> GetsalesJournalAA()
        {
            return Tasks.GetsalesJournalAA();
        }
        public List<eaSalesJournalARModel> GetsalesJournalAR()
        {
            return Tasks.GetsalesJournalAR();
        }

        public bool FlagSentRecords(List<eaMessagesStatusModel> listtobeflaged)
        {
        return Tasks.FlagSentRecords(listtobeflaged);
        }
        public void GetDocsForSpecifiedRange(eaDateModel model)
        {
             Tasks.GetDocsForSpecifiedRange(model);
        }

        public void Save_SalesJournalDepositOut_To_hit_estia_sales_journal_deposit_out(eaDateModel model)
        {
             Tasks.Save_SalesJournalDepositOut_To_hit_estia_sales_journal_deposit_out(model);
        }

        public void GetMainCourante()
        {
            Tasks.GetMainCourante();
        }

        public List<SalesJournalAAMain> GetSalesJournalAADateRange(eaDateModel model)
        {
           return Tasks.GetSalesJournalAADateRange(model);
        }
        public List<eaSalesJournalARMain> GetDebtorsSalesJournalAR(eaDateModel model)
        {
            return Tasks.GetDebtorsSalesJournalAR(model);
        }

        public List<KundenModel> GetCustomers(eaDateModel model)
        {
            return Tasks.GetCustomers(model);
        }
    }
}