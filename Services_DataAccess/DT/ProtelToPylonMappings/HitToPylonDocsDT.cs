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
    public class HitToPylonDocsDT
    {
        ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

         SettingsModel settings;
        int timeout;
        string ConnString;

        public HitToPylonDocsDT(SettingsModel settings)
        {
            this.settings = settings;
            ConnString = settings.ProtelDB;
            //timeout = Int32.Parse(settings.DBTimeout);
        }
        
             public void DeleteSentRecords(List<string>deleted)
        {
            
            string dataarray = "";
           
        for (int i = 0; i < deleted.Count; i++)
            {
                dataarray += deleted[i] + ",";
            }
           dataarray= dataarray.TrimEnd(',');
            string deletesql = "DELETE FROM hit_to_pylon_docs WHERE rechnr IN ( " + dataarray + " ) ";
           
            try
            {
                
                using (IDbConnection db = new SqlConnection(ConnString))
                {
                    db.Execute(deletesql);
                }
            }
            catch (Exception ex)
            {
                logger.Error("The deletion of the records was not completed - " + deletesql.ToString() + " \r\n" + ex.ToString());
                throw new Exception(ex.ToString());
            }
            return;
        }

        public List<HitToPylonDocsModel> GetHitToPylonDocs()
        {
            StringBuilder SQL = new StringBuilder();
            try
            {
                SQL.Append("SELECT rechnr, fisccode, mpehotel, is_sent, date_created FROM hit_to_pylon_docs");
                using (IDbConnection db = new SqlConnection(ConnString))
                {
                    return db.Query<HitToPylonDocsModel>(SQL.ToString()).ToList();
                }
            }
            catch (Exception ex)
            {
                logger.Error("Protel docs to sent to Pylon were not fetched successfully " + SQL.ToString() + " \r\n" + ex.ToString());
                throw new Exception(ex.ToString());
            }

        }
    }
}
