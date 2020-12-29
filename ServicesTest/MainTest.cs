using System;
using System.IO;
using Hit.Services.Helpers.Classes.Classes;
using Hit.Services.MainLogic.Flows.SQLJobs;
using Hit.Services.Models;
using Hit.Services.Models.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;

namespace HitServicesTest   //  <----<<< IMPORTANT: All tests must have that namespace
{
    [SetUpFixture]
    public class MainTest
    {
        [OneTimeSetUp]
       public void RunBeforeAnyTests()
        {
            //Config Automapper
            AutoMapperConfig.RegisterMappings();


            //get current directory
            var dir = Path.GetDirectoryName(typeof(MainTest).Assembly.Location);
            Environment.CurrentDirectory = dir;

            //1. read ScheduledJobs
            ConfigHelper reader = new ConfigHelper();
            SchedulerModel sch = reader.ReadScheduledJobsFile(true);
            reader.ConnectionString(sch.SchedulerDBServer, sch.SchedulerDB, sch.SchedulerDBUser, sch.SchedulerDBPassword);

            //2.set conString to HangFireDB.ConString
            HangFireDB.ConString = reader.ConnectionString(sch.SchedulerDBServer, sch.SchedulerDB, sch.SchedulerDBUser, sch.SchedulerDBPassword); ;

            //2. create table SqlParameters
            SqlParametersFlows sqlparamflow = new SqlParametersFlows();
            sqlparamflow.CreateSqlParametersTable(1);


        }
    }
}
