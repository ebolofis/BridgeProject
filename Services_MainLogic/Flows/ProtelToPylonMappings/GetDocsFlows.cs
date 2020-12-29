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
  public  class GetDocsFlows
    {
        GetDocsTasks Tasks;
         SettingsModel settings;
        ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        WebApiClientHelper apiHelper;
        public GetDocsFlows(SettingsModel settings)
        {
            this.settings = settings;
            Tasks = new GetDocsTasks(settings);
        }

        public List<FiscalcdModel> GetDocs(string mpehotel)
        {
            return Tasks.GetDocs(mpehotel);
        }

    }
}
