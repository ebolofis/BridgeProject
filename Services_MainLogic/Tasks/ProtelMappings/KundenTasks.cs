using Hit.Services.Core;
using Hit.Services.DataAccess.DT.ProtelToPylonMappings;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hit.Services.MainLogic.Tasks.ProtelMappings
{
    public class KundenTasks
    {
        SettingsModel settings;
        ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        KundenDT DTs;


        public KundenTasks(SettingsModel settings)
        {
            this.settings = settings;
            DTs = new KundenDT(settings);
        }

        public bool UpdateFibudebToPylonCustomerId(string ProtelId,string PylonId)
        {
            return DTs.UpdateFibudebToPylonCustomerId(ProtelId,PylonId);
        }
    }
}
