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
    public class VatsFlows
    {
        VatsTasks Tasks;
        SettingsModel settings;
        ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
       
        WebApiClientHelper apiHelper;
        public VatsFlows(SettingsModel settings)
        {
            this.settings = settings;
            Tasks = new VatsTasks( settings);
        }

        public List<MwstModel> GetVats( )
        {
            return Tasks.GetVats();
        }
        
    }

}
