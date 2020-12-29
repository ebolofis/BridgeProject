using AutoMapper;
using Hit.Services.DataAccess.DAOs;
using Hit.Services.DataAccess.DT.Sql;
using Hit.Services.DTOs.HitServices;
using Hit.Services.Models.Models.SQL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hit.Services.DataAccess.DT.SQL
{
    /// <summary>
    /// class that handles Tasks for HitServicesDB
    /// </summary>
    public class HitServicesDT
    {
        protected log4net.ILog logger;

        public HitServicesDT()
        {
            logger = log4net.LogManager.GetLogger(this.GetType());
        }
        
        /// <summary>
        /// Create table SqlParameters in DbHitServicesDB
        /// </summary>
        /// <param name="conString">HitServicesDB (Hangfire Db) connection string</param>
        public void CreateSqlParametersTable(string conString,int retry)
        {
            logger.Info("Checking for table SqlParameters...");
            string createScript = @"
              IF  NOT EXISTS (SELECT * FROM sys.objects 
                WHERE object_id = OBJECT_ID(N'[dbo].[SqlParameters]') AND type in (N'U'))
                begin
                CREATE TABLE [dbo].[SqlParameters](
	                    [id] [int] IDENTITY(1,1) NOT NULL,
	                    [SettingsFile] [nvarchar](200) NULL,
	                    [Parameter] [nvarchar](200) NULL,
	                    [Value] [nvarchar](2000) NULL,
	                    [OldValue] [nvarchar](2000) NULL,
                     CONSTRAINT [PK_SqlParameters] PRIMARY KEY CLUSTERED 
                    (
	                    [id] ASC
                    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
                    ) ON [PRIMARY]

               CREATE UNIQUE NONCLUSTERED INDEX IX_SqlParametersIndex  
                                        ON SqlParameters (SettingsFile,Parameter);
                end";

            //this is the first time HitServeces connect to DB. 
            //On error wait for sql server start-up for up to 3 min...
            int retries = 0;
            while (true)
            {
                try
                {
                    GenericDT runsql = new GenericDT();
                    runsql.RunScript(conString, createScript);
                    break;
                }
                catch (Exception ex)
                {
                    retries++;
                    logger.Warn(ex.Message+"  Retring again...");
                    if (retries >= retry) throw;
                    Thread.Sleep(5000);
                }
            }
          
           
        }

        /// <summary>
        /// Update or Insert an SqlParameter to HitServicesDB (HangFireDB)
        /// </summary>
        /// <param name="conString">HitServicesDB (HangFireDB) connection string</param>
        /// <param name="settingfile">settingfile</param>
        /// <param name="parameter">SqlParameter</param>
        /// <param name="value">new value</param>
        public void UpdateParameter(string conString,string settingfile, string parameter, string value)
        {
            GenericDAO<SqlParametersDTO> sqlparams = new GenericDAO<SqlParametersDTO>();
            using (IDbConnection db = new SqlConnection(conString))
            {
                SqlParametersDTO p = sqlparams.Select(db, "where SettingsFile=@settingsfile and Parameter=@parameter", new { settingsfile = settingfile, parameter = parameter }).FirstOrDefault();

                if (p == null)
                {
                    //insert
                    p = new SqlParametersDTO() { Id = 0, SettingsFile = settingfile, Parameter = parameter, Value = value, OldValue = "" };
                    sqlparams.InsertInt(db,p);
                }
                else
                {
                    //update
                    p.OldValue = p.Value;
                    p.Value = value;
                    sqlparams.Update(db,p);
                }
            }
        }

        /// <summary>
        /// Return the list of sql parameters for a specific settingfile
        /// </summary>
        /// <param name="conString">HitServicesDB (HangFireDB) connection string</param>
        /// <param name="settingfile">settingfile</param>
        /// <returns></returns>
        public List<SqlParameters> GetParameters(string conString, string settingfile)
        {
            GenericDAO<SqlParametersDTO> sqlparams = new GenericDAO<SqlParametersDTO>();
            using (IDbConnection db = new SqlConnection(conString))
            {
              List< SqlParametersDTO> list=  sqlparams.Select(db, "where SettingsFile=@settingsfile", new { settingsfile = settingfile });
              logParameters(settingfile, list);
              return AutoMapper.Mapper.Map<List<SqlParameters>>(list);
            }
         }

        /// <summary>
        /// Return the value of an SqlParameter from DB
        /// </summary>
        /// <param name="conString">HitServicesDB (HangFireDB) connection string</param>
        /// <param name="settingfile">settingfile</param>
        /// <param name="parameter">Sql parameter</param>
        /// <returns></returns>
        public string GetParameterValue(string conString, string settingfile,string parameter)
        {
            GenericDAO<SqlParametersDTO> sqlparams = new GenericDAO<SqlParametersDTO>();
            using (IDbConnection db = new SqlConnection(conString))
            {
                SqlParametersDTO model = sqlparams.Select(db, "where SettingsFile=@settingsfile and Parameter=@parameter", new { settingsfile = settingfile, parameter= parameter }).FirstOrDefault();

                if (model == null)
                    model=new SqlParametersDTO();
             
                logger.Info("SQLParameter for '"+ settingfile + "'. Param: " + parameter + ", Value : " + (model.Value ?? "<null>") + ", OldValue : " + (model.OldValue ?? "<null>") + ".");
                return model.Value;

            }
        }

        private void logParameters(string settingfile,List<SqlParametersDTO> list)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("SQLParameters for ");
                sb.Append(settingfile);
                sb.Append(" : ");
                foreach (var item in list)
                {
                    sb.Append("Param: "+ item.Parameter+", Value : "+(item.Value??"<null>") + ", OldValue : " + (item.OldValue ?? "<null>")+".  ");
                }
                logger.Info(sb.ToString());
            }
            catch (Exception ex)
            {

            }
        }

    }
}
