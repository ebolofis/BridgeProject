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
    public class BranchDT
    {
        ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        SettingsModel settings;
        int timeout;
        private string ConnString;


        public BranchDT(SettingsModel settings)
        {
            this.settings = settings;
            ConnString = settings.ProtelDB;
            //timeout = Int32.Parse(settings.DBTimeout);
        }

        public List<LizenzModel> GetMatchingBranch( )
        {
            StringBuilder SQL = new StringBuilder();
            try
            {
                SQL.Append("SELECT  mpehotel,hotel FROM lizenz");
                using (IDbConnection db = new SqlConnection(ConnString))
                {
                    return db.Query<LizenzModel>(SQL.ToString()).ToList();
                }
            }
            catch (Exception ex)
            {
                logger.Error("Matching Branches were not fetched successfully " + SQL.ToString() + " \r\n" + ex.ToString());
                throw new Exception(ex.ToString());
            }

        }
    }
}
