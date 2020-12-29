using Hit.Services.Helpers.Classes.Classes;
using log4net;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hit.Services.Helpers.Classes.Classes
{
    public class ExceptionMessHelper
    {
        protected ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Log the Exception and Return proper Error Message
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="notes">additional info for logging</param>
        /// <returns></returns>
        public string GetErrorMessage(Exception ex, string notes = "")
        {

            //BusinessException
            if (ex is BusinessException)
            {
                logger.Warn(notes + ex.ToString());

                return ex.Message;
            }



            //SqlException
            if (ex is SqlException)
            {

                if ((ex.Message.Contains("The INSERT statement conflicted with the FOREIGN KEY constraint")))
                {
                    logger.Warn(notes + ex.ToString());
                    RegExHelper RegEx = new RegExHelper();
                    string table = RegEx.GetString(ex.Message, @"table ""dbo.HES(?<table>\w+)""", "table");
                    return string.Format(Hit.Services.Resources.Errors.INSERTFOREIGNKEYCONSTRAINT, table);
                }

                else if (ex.Message.Contains("The DELETE statement conflicted with the REFERENCE constraint") || ((ex as SqlException).InnerException != null && (ex as SqlException).InnerException.InnerException.Message.Contains("The DELETE statement conflicted with the REFERENCE constraint")))
                {
                    logger.Warn(notes + ex.ToString());
                    RegExHelper RegEx = new RegExHelper();
                    string table = RegEx.GetString(ex.Message, @"table ""dbo.HES(?<table>\w+)""", "table");
                    return string.Format(Hit.Services.Resources.Errors.DELETEFOREIGNKEYCONSTRAINT, table);
                }
                else if (ex.Message.Contains("The UPDATE statement conflicted with the REFERENCE constraint") || ((ex as SqlException).InnerException != null && (ex as SqlException).InnerException.InnerException.Message.Contains("The UPDATE statement conflicted with the REFERENCE constraint")))
                {
                    logger.Warn(ex.ToString());
                    RegExHelper RegEx = new RegExHelper();
                    string table = RegEx.GetString(ex.Message, @"table ""dbo.HES(?<table>\w+)""", "table");
                    return notes + string.Format(Hit.Services.Resources.Errors.UPDATEFOREIGNKEYCONSTRAINT, table);
                }
                else if (ex.Message.Contains("Violation of UNIQUE KEY constraint") || ((ex as SqlException).InnerException != null && (ex as SqlException).InnerException.InnerException.Message.Contains("Violation of UNIQUE KEY constraint")))
                {
                    logger.Warn(notes + ex.ToString());
                    return Hit.Services.Resources.Errors.SQLVIOLATIONOFUNIQUEKEY;
                }
                else
                {
                    logger.Error(notes + ex.ToString());
                    return string.Format(Hit.Services.Resources.Errors.SQLERROR + ex.Message);
                }
            }




            //other Exception
            logger.Error(notes + ex.ToString());
            return ex.Message;
        }


    }
}
