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
    public class CurrencyFlows
    {
        CurrencyTasks Tasks;
        SettingsModel settings;
        ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        WebApiClientHelper apiHelper;
        public CurrencyFlows(SettingsModel settings)
        {
            this.settings = settings;
            Tasks = new CurrencyTasks(settings);
        }

        public List<CurrencyModel> GetCurrency()
        {
            return Tasks.GetCurrency();
        }
    }
}
