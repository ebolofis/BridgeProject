using Dapper;
using Hit.Services.Core;
using Hit.Services.Models.Models.ProtelMappingsToPylon;
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hit.Services.DataAccess.DT.ProtelToPylonMappings
{
    public class KundenDT
    {

        ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        SettingsModel settings;
        int timeout;
        private string ConnString;


        public KundenDT(SettingsModel settings)
        {
            this.settings = settings;
            ConnString = settings.ProtelDB;
            //timeout = Int32.Parse(settings.DBTimeout);
        }

        public bool UpdateFibudebToPylonCustomerId(string Proteld, string PylonId)
        {
          
            string Update = "UPDATE Kunden SET fibudeb = @PylonId  WHERE kdnr=@ProtelId ";

       long protel = Int32.Parse(Proteld);

           long pylon = Int32.Parse(PylonId);

            KundenModel model;
            try
            {
               
                
                using (IDbConnection db = new SqlConnection(ConnString))
                {
              
                        db.Execute(Update, new { PylonId = pylon, ProtelId = protel });
                        return true;
                    
               
                }
            }
            catch (Exception ex)
            {
                logger.Error("Check Customer failed" + Update.ToString() + " \r\n" + ex.ToString());
                throw new Exception(ex.ToString());

            }
            return false;
        }
    }
}
