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
    public class KundenFlows
    {
        KundenTasks Tasks;
        SettingsModel settings;
        ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        WebApiClientHelper apiHelper;
        public KundenFlows(SettingsModel settings)
        {
            this.settings = settings;
            Tasks = new KundenTasks(settings);
        }

        public bool UpdateFibudebToPylonCustomerId(string ProtelId, string PylonId)
        {
            return Tasks.UpdateFibudebToPylonCustomerId( ProtelId,  PylonId);
        }
    }
}
