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
    public class PaymentMethodsDT
    {

        ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        string ConnString;
         SettingsModel settings;
        int timeout;


        public PaymentMethodsDT(SettingsModel settings)
        {
            this.settings = settings;
            ConnString = settings.ProtelDB;
            //timeout = Int32.Parse(settings.DBTimeout);
        }

        public List<ZahlartModel> GetPaymentMethods( )
        {
            StringBuilder SQL = new StringBuilder();
            try
            {
                SQL.Append("SELECT  zanr,bez FROM Zahlart");
                using (IDbConnection db = new SqlConnection(ConnString))
                {
                    return db.Query<ZahlartModel>(SQL.ToString()).ToList();
                }
            }
            catch (Exception ex)
            {
                logger.Error("PaymentMethods were not fetched successfully " + SQL.ToString() + " \r\n" + ex.ToString());
                throw new Exception(ex.ToString());
            }
        }
    }
}
