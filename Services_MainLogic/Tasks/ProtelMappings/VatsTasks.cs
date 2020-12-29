﻿using Hit.Services.Core;
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
    public class VatsTasks
    {

       SettingsModel settings;
        ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        VatDT DTs;
        

        public VatsTasks(SettingsModel settings)
        {
            this.settings = settings;
           DTs = new VatDT(settings);
        }

        public List<MwstModel> GetVats( )
        {
           return DTs.GetVats();
        }

    }
}
