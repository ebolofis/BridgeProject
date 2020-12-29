using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hit.Services.Models.Models
{
    /// <summary>
    /// Class that keeps what files are encrypted
    /// </summary>
    public class FilesEncryptStatus
    {

        /// <summary>
        /// True if ScheduledJobs File is encrypted
        /// </summary>
        public bool ScheduledJobsFileIsEcrypted { get;set;}

        /// <summary>
        /// True if Settings Files Files are encrypted
        /// </summary>
        public bool SettingsFilesAreEncrypted { get;set;}

        /// <summary>
        /// True if Script Files are encrypted
        /// </summary>
        public bool ScriptFilesAreEncrypted { get;set;}


        /// True if WebApiSettings File is encrypted
        /// </summary>
        public bool WebApiSettingsIsEcrypted { get; set; }
    }
}
