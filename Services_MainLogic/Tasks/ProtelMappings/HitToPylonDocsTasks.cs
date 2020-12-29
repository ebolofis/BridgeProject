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
    public class HitToPylonDocsTasks
    {
        SettingsModel settings;
        ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        HitToPylonDocsDT DTs;


        public HitToPylonDocsTasks(SettingsModel settings)
        {
            this.settings = settings;
            DTs = new HitToPylonDocsDT(settings);
        }

        public List<HitToPylonDocsModel> GetHitToPylonDocs( )
        {
            return DTs.GetHitToPylonDocs();
        }
        public void DeleteSentRecords(List<string> deleterecords)
        {
             DTs.DeleteSentRecords(deleterecords);
            return;
        }
    }
}
