using Hit.Services.Core;
using Hit.Services.DataAccess.DT.ProtelToPylonMappings;
using Hit.Services.Models.Models.ProtelMappingsToPylon;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hit.Services.MainLogic.Tasks.ProtelMappings
{
    public class GetDocsTasks
    {
         SettingsModel settings;
        ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        GetDocsDT DTs;


        public GetDocsTasks(SettingsModel settings)
        {
            this.settings = settings;
            DTs = new GetDocsDT(settings);
        }

        public List<FiscalcdModel> GetDocs(string mpehotel)
        {
            return DTs.GetDocs(mpehotel);
        }
    }
}
