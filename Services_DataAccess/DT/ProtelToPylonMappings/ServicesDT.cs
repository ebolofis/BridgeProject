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
    public class ServicesDT
    {
        ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

         SettingsModel settings;
        int timeout;
        string ConnString;


        public ServicesDT(SettingsModel settings)
        {
            this.settings = settings;
            ConnString = settings.ProtelDB;
            //timeout = Int32.Parse(settings.DBTimeout);
        }

        public List<UktoModel> GetServices(string mpehotel)
        {
            List<UktoModel> result = new List<UktoModel>();

            StringBuilder SQL = new StringBuilder();
            try
            {
                SQL.Append("SELECT  ktonr, '(' + kto + ') ' + bez AS bez FROM ukto WHERE mpehotel in ( 0, " + mpehotel + ")");

                using (IDbConnection db = new SqlConnection(ConnString))
                {
                    result = db.Query<UktoModel>(SQL.ToString()).ToList();

                    if (result.Count == 0)
                    {
                        SQL.Clear();

                        SQL.Append("SELECT  ktonr,bez FROM ukto WHERE mpehotel = 0");

                        result = db.Query<UktoModel>(SQL.ToString()).ToList();
                    }

                    return result;
                }
            }
            catch (Exception ex)
            {
                logger.Error("Services were not fetched successfully " + SQL.ToString() + " \r\n" + ex.ToString());
                throw new Exception(ex.ToString());
            }

        }

    }
}
