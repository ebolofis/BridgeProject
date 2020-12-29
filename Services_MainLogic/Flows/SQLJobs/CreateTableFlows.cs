using Hit.Services.Core;
using Hit.Services.DataAccess.DT.Sql;
using Hit.Services.Helpers.Classes.Classes;
using Hit.Services.MainLogic.Tasks.SQLJobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hit.Services.MainLogic.Flows.SQLJobs
{
    public class CreateTableFlows
    {
        SQLTasks sqlTasks;
        SettingsModel settings;
        FileHelpers fh = new FileHelpers();
        ExportDataTasks exportDataTask;
        SqlParametersTasks sqlParametersTasks;
        GenericDT sqlScriptsDT;

        public CreateTableFlows(SettingsModel settings)
        {
            this.settings = settings;
        }

        public void CreateTable()
        {
            CreateTableTasks creatTableTasks = new CreateTableTasks(this.settings);
            creatTableTasks.CreateTable(settings.ProtelDB);
        }

        public void CreateMCTable()
        {
            CreateTableTasks creatTableTasks = new CreateTableTasks(this.settings);
            creatTableTasks.CreateMCTable(settings.ProtelDB);
        }
    }
}
