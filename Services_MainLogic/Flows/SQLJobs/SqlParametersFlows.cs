using Hit.Services.DataAccess.DT.SQL;
using Hit.Services.Models.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hit.Services.MainLogic.Flows.SQLJobs
{
    /// <summary>
    /// Flows handling Table SqlParameters in HitServicesDB (HangFireDB)
    /// </summary>
    public class SqlParametersFlows
    {
        HitServicesDT hitServicesDT;
        ILog logger;

        public SqlParametersFlows() {
            hitServicesDT = new HitServicesDT();
            log4net.Config.XmlConfigurator.Configure();
            logger = log4net.LogManager.GetLogger(this.GetType());
        }


        /// <summary>
        /// Create Table SqlParameters in HitServicesDB (HangFireDB)
        /// </summary>
        public void CreateSqlParametersTable(int retry=40)
        {
            try
            {
                hitServicesDT.CreateSqlParametersTable(HangFireDB.ConString, retry);
                logger.Info("Table [SqlParameters] is in place.");
            }
            catch (Exception ex)
            {
                logger.Error("Error Creating Table [SqlParameters] : " + ex.ToString());
            }
        }


        /// <summary>
        /// Update or Insert an SqlParameter to HitServicesDB (HangFireDB)
        /// </summary>
        /// <param name="settingfile">settingfile</param>
        /// <param name="parameter">SqlParameter</param>
        /// <param name="value">new value</param>
        public void UpdateParameter(string settingfile, string parameter, string value)
        {
            hitServicesDT.UpdateParameter(HangFireDB.ConString, settingfile, parameter, value);
        }
     }
}
