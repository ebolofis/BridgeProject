using Hit.Services.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hit.Services.Scheduler.Core
{
  public static  class FilesState
    {

        /// <summary>
        /// Get if files ScheduledJobs, ScriptFiles, WebApiSettings and SettingsFiles are encrypted or not
        /// </summary>
        /// <returns></returns>
        public static FilesEncryptStatus GetEncryptFilesStatus()
        {
            FilesEncryptStatus st = new FilesEncryptStatus();
            st.ScheduledJobsFileIsEcrypted = HitServices_Core.Properties.Settings.Default.ScheduledJobsFileIsEcrypted;
            st.ScriptFilesAreEncrypted = HitServices_Core.Properties.Settings.Default.ScriptFilesAreEncrypted;
            st.SettingsFilesAreEncrypted = HitServices_Core.Properties.Settings.Default.SettingsFilesAreEncrypted;
            st.WebApiSettingsIsEcrypted = HitServices_Core.Properties.Settings.Default.WebApiSettingsIsEcrypted;
            return st;
        }
    }
}
