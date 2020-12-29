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
    public class PylonCustomerDT
    {
        ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        string ConnString;
        SettingsModel settings;
        int timeout;

        public PylonCustomerDT(SettingsModel settings)
        {
            this.settings = settings;
            ConnString = settings.ProtelDB;
        }
        
        public int StorePylonCustomer(string ProtelID, string PylonID )
        {
            StringBuilder SQL = new StringBuilder();

            try
            {
                SQL.Append("UPDATE kunden SET lalala = @PylonID WHERE kdnr = @ProtelID");

                using (IDbConnection db = new SqlConnection(ConnString))
                {
                    return db.Execute(SQL.ToString(), new { @PylonID = PylonID, @ProtelID = ProtelID });
                }
            }
            catch (Exception ex)
            {
                logger.Error("Update customer with pylon customer id failed " + SQL.ToString() + " \r\n" + ex.ToString());

                throw new Exception(ex.ToString());
            }
        }
    }
}
