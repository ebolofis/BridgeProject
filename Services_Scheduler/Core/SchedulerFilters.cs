using Hangfire;
using Hangfire.Common;
using Hangfire.Server;
using Hangfire.States;
using Hangfire.Storage;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hit.Services.Scheduler.Core
{



    public class ProlongExpirationTimeAttribute : JobFilterAttribute, IApplyStateFilter
    {
        public void OnStateApplied(ApplyStateContext context, IWriteOnlyTransaction transaction)
        {
            context.JobExpirationTimeout = TimeSpan.FromDays(5);
           
        }

        public void OnStateUnapplied(ApplyStateContext context, IWriteOnlyTransaction transaction)
        {
            //throw new NotImplementedException();
        }
    }



    /// <summary>
    /// Hangfire's filter that return JobId (see TestJob)
    /// </summary>
    public class GetScheduledJobId : IServerFilter
    {
        [ThreadStatic]
        private static string _jobId;

        public static string JobId { get { return _jobId; } set { _jobId = value; } }


        [ThreadStatic]
        private static DateTime _lastDate;

        public static DateTime LastDate { get { return _lastDate; } set { _lastDate = value; } }

        public void OnPerformed(PerformedContext filterContext)
        {
            JobId = filterContext.BackgroundJob.Id;
            //lock (this)
            //{
            //    LastDate = GetLastExecutionTime(JobId);
            //}
        }

        public void OnPerforming(PerformingContext context)
        {
            JobId = context.BackgroundJob.Id;
            
        }




    //    /// <summary>
    //    /// This function will return last execution time of job based on the job key.
    //    /// If it runs for the first time then it will return min value of date time.
    //    /// </summary>
    //    /// <param name="jobId">The job key is the identifier (constant class: HangFireJobKeys).</param>
    //    /// <returns></returns>
    //    protected DateTime GetLastExecutionTime(string jobId)
    //    {
            
    //            using (var connection = JobStorage.Current.GetConnection())
    //            {
    //                var currentJob = connection.GetRecurringJobs().FirstOrDefault(p => p.LastJobId == jobId);
    //                var currentJob2 = connection.GetRecurringJobs().OrderByDescending(p => p.LastJobId == jobId).Take(2).ToList();
    //                if (currentJob2 != null && currentJob2.Count() >= 2)
    //                    return currentJob2[1].LastExecution ?? (DateTime)SqlDateTime.MinValue;
    //                else
    //                    return (DateTime)SqlDateTime.MinValue;

    //                //if (currentJob == null)
    //                //{
    //                //    // _log.Error(string.Format(JobNotfoundWithJobIdError, jobId));
    //                //    return (DateTime)SqlDateTime.MinValue;
    //                //}
    //                //return currentJob.LastExecution?? (DateTime)SqlDateTime.MinValue;
    //            }
           
           

    //        //var currentJob = GetJob(jobId);
    //        //var currentjobLastData = GetPreviousJob(currentJob.LastJobId);
    //        //if (currentjobLastData != null)
    //        //{
    //        //    return currentjobLastData.CreatedAt;
    //        //}

    //        //return (DateTime)SqlDateTime.MinValue;
    //    }

    //    /// <summary>
    //    /// The previous job date identifier is being used to set the last job id which will match the Hash table of the
    //    /// Hangfire Database.
    //    /// </summary>
    //    private const int ThePreviousJobDateId = 1;

    //    /// <summary>
    //    /// This method returns all the job data of previous execution of job
    //    /// based on the lastJobId.
    //    /// </summary>
    //    /// <param name="lastJobid">The last jobid.(integer value belongs to Hangfire database).</param>
    //    /// <returns></returns>
    //    protected JobData GetPreviousJob(string lastJobid)
    //    {
    //        using (var connection = JobStorage.Current.GetConnection())
    //        {
    //            var previousJob = connection.GetJobData((Convert.ToInt32(lastJobid) - ThePreviousJobDateId).ToString());
    //            return previousJob;
    //        }
    //    }

    //    /// <summary>
    //    /// This method is used for retrieving data transfer object for recurring job besed on the job key from
    //    /// Hangfire database.
    //    /// </summary>
    //    /// <param name="jobId">The job identifier.(constant class: HangFireJobKeys).</param>
    //    /// <returns></returns>
    //    /// <exception cref="System.Exception"></exception>
    //    protected RecurringJobDto GetJob(string jobId)
    //    {
    //        using (var connection = JobStorage.Current.GetConnection())
    //        {
    //            var currentJob = connection.GetRecurringJobs().FirstOrDefault(p => p.LastJobId== jobId);
    //            if (currentJob == null)
    //            {
    //               // _log.Error(string.Format(JobNotfoundWithJobIdError, jobId));
    //               throw new Exception(string.Format(JobNotfoundWithJobIdError, jobId));
    //            }
    //            return currentJob;
    //        }
    //    }
    //    private const string JobNotfoundWithJobIdError = "Background job does not exist with jobid: {0}";

    }
}
