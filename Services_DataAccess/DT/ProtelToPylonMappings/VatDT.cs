using Dapper;
using Hit.Services.Core;
using Hit.Services.DTOs.Protel;
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
    public class VatDT
    {

        ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        SettingsModel settings;
        int timeout;
        string ConnString;


        public VatDT(SettingsModel settings)
        {
            this.settings = settings;
            ConnString = settings.ProtelDB;
            //timeout = Int32.Parse(settings.DBTimeout);
        }

        public List<MwstModel> GetVats( )
        {
            StringBuilder SQL = new StringBuilder();
            try
            {
                SQL.Append("SELECT  satznr,satz FROM mwst");
                using (IDbConnection db = new SqlConnection(ConnString))
                {
                    return db.Query<MwstModel>(SQL.ToString()).ToList();
                }
            }
            catch (Exception ex)
            {
                logger.Error("Vats were not fetched successfully " + SQL.ToString() + " \r\n" + ex.ToString());
                throw new Exception(ex.ToString());
            }
            
        }



    }
}
