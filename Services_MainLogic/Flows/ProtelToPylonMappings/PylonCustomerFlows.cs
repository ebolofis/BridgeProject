using Hit.Services.Core;
using Hit.Services.Helpers.Classes.Classes;
using Hit.Services.MainLogic.Tasks.ProtelMappings;
using Hit.Services.Models.Models.ProtelMappingsToPylon;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hit.Services.MainLogic.Flows.ProtelToPylonMappings
{
    public class PylonCustomerFlows
    {
        PylonCustomerTasks Tasks;
         SettingsModel settings;
        ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        WebApiClientHelper apiHelper;
        public PylonCustomerFlows(SettingsModel settings)
        {
            this.settings = settings;
            Tasks = new PylonCustomerTasks(settings);
        }

        public int StorePylonCustomer(string ProtelID, string PylonID)
        {
            return Tasks.StorePylonCustomer(ProtelID, PylonID);
        }
    }
}
