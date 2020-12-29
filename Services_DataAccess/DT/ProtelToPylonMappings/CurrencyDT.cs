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
    public class CurrencyDT
    {
        ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        SettingsModel settings;
        int timeout;
        string ConnString;

        public CurrencyDT(SettingsModel settings)
        {
            this.settings = settings;
            ConnString = settings.ProtelDB;
            //timeout = Int32.Parse(settings.DBTimeout);
        }

        public List<CurrencyModel> GetCurrency()
        {
            StringBuilder SQL = new StringBuilder();
            try
            {
                SQL.Append("SELECT ref AS Ref, name AS Name FROM curren");
                using (IDbConnection db = new SqlConnection(ConnString))
                {
                    return db.Query<CurrencyModel>(SQL.ToString()).ToList();
                }
            }
            catch (Exception ex)
            {
                logger.Error("Currencies were not fetched successfully " + SQL.ToString() + " \r\n" + ex.ToString());
                throw new Exception(ex.ToString());
            }
        }
    }
}
