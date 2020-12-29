using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hit.Services.Models.Models
{
    /// <summary>
    /// Class representing Scheduler (Hangfire) Configuration
    /// </summary>
    public class SchedulerModel
    {
        /// <summary>
        /// DB server for Scheduler
        /// </summary>
        public string SchedulerDBServer { get; set; }

        /// <summary>
        /// DB for Scheduler
        /// </summary>
        public string SchedulerDB { get; set; }

        /// <summary>
        /// DB user for Scheduler
        /// </summary>
        public string SchedulerDBUser { get; set; }

        /// <summary>
        /// DB password for Scheduler
        /// </summary>
        public string SchedulerDBPassword { get; set; }

        /// <summary>
        /// true: scheduler is active
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// true: Dashboard is active
        /// </summary>
        public bool Dashboard { get; set; }

        /// <summary>
        /// Customer - Installation
        /// </summary>
        public string Installation { get; set; } = "";

        /// <summary>
        /// the list of jobs
        /// </summary>
        public List<SchedulerJob> Jobs { get; set; }

        public string MainPassword { get; } = "!4502!";

        public string GuestPassword { get; } = "hit12!@";
    }


    /// <summary>
    /// class representing a Scheduled job
    /// </summary>
    public class SchedulerJob
    {
        /// <summary>
        /// Job's unique Id
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// Job's description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Job's full class name
        /// </summary>
        public string ClassName { get; set; }

        /// <summary>
        /// Settings file
        /// </summary>
        public string Settings { get; set; }

        /// <summary>
        /// Job's Schedule description
        /// </summary>
        public string Schedule { get; set; } = "Every hour";

        /// <summary>
        /// true: Job is active
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Job's cron expression. (see https://crontab-generator.org/)
        /// </summary>
        public string Cron { get; set; } = "0 * * * *";

       
        public override string ToString()
        {
            return Description + ", ClassName: " + ClassName + ", IsActive: " + IsActive.ToString() + ", Cron:" + Cron+", settings: "+ Settings;
        }

    }

    /// <summary>
    /// class that relates a controller with a Settings file.
    /// </summary>
    public class WebApiSettingsModel
    {
        /// <summary>
        /// web api's controller full name
        /// </summary>
        public string Controller { get; set; }

        /// <summary>
        /// Setings filename
        /// </summary>
        public string Settings { get; set; }

    }
}
