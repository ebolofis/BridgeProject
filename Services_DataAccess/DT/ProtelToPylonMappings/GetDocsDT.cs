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
   public class GetDocsDT
    {
     

            ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

             SettingsModel settings;
            int timeout;
        string ConnString;

            public GetDocsDT(SettingsModel settings)
            {
                this.settings = settings;
            ConnString = settings.ProtelDB;
            //timeout = Int32.Parse(settings.DBTimeout);
        }

        public List<FiscalcdModel> GetDocs(string mpehotel)
        {
            List<FiscalcdModel> result = new List<FiscalcdModel>();

            StringBuilder SQL = new StringBuilder();
            try
            {
                SQL.Append("SELECT  ref,text FROM fiscalcd WHERE mpehotel = " + mpehotel);
                using (IDbConnection db = new SqlConnection(ConnString))
                {
                    result = db.Query<FiscalcdModel>(SQL.ToString()).ToList();

                    if ( result.Count == 0 )
                    {
                        SQL.Clear();
                        
                        SQL.Append("SELECT  ref,text FROM fiscalcd WHERE mpehotel = 0");

                        result = db.Query<FiscalcdModel>(SQL.ToString()).ToList();
                    }

                    return result;
                }
            }
            catch (Exception ex)
            {
                logger.Error("Docs were not fetched successfully " + SQL.ToString() + " \r\n" + ex.ToString());
                throw new Exception(ex.ToString());
            }

        }

        }
    }
