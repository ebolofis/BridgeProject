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
   public  class PaymentMethodsTasks
    {
        SettingsModel settings;
        ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        PaymentMethodsDT DTs;


        public PaymentMethodsTasks(SettingsModel settings)
        {
            this.settings = settings;
            DTs = new PaymentMethodsDT(settings);
        }

        public List<ZahlartModel> GetPaymentMethods( )
        {
            return DTs.GetPaymentMethods();
        }
    }
}
