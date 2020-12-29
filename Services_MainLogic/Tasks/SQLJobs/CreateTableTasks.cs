using Hit.Services.Core;
using Hit.Services.DataAccess.DT.Sql;
using Hit.Services.DataAccess.DT.SQL;
using Hit.Services.Helpers.Classes;
using Hit.Services.Helpers.Classes.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hit.Services.MainLogic.Tasks.SQLJobs
{
    public class CreateTableTasks
    {

        SettingsModel settings;
        //    string sqlScript;
        ConfigHelper configHelper;
        GenericDT RunScriptDT;
        ConvertDynamicHelper castDynamic;

        /// <summary>
        /// Tasks for managing SqlParameters
        /// </summary>
        /// <param name="settings">Settings Model</param>
        /// <param name="script">sql script to execute</param>
        public CreateTableTasks(SettingsModel settings)
        {
            this.settings = settings;
            configHelper = new ConfigHelper();
            RunScriptDT = new GenericDT();
            castDynamic = new ConvertDynamicHelper();
        }

        public void CreateTable(string connStr)
        {
            CreateTable creatTableDT = new CreateTable();
            creatTableDT.CreateTables(connStr);
        }

        public void CreateMCTable(string connStr)
        {
            CreateTable creatTableDT = new CreateTable();
            creatTableDT.CreateMCTables(connStr);
        }
    }
}
