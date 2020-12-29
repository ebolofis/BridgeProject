using Hit.Services.EncryptDecrypt;
using Hit.Services.Helpers.Classes.Classes;
using Hit.Services.Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Hit.Services.Models;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;
using Hit.Scheduler.Jobs;
using log4net;
using Hit.Services.Scheduler.Core;
using Hit.Services.Core;

namespace EncryptDecrypt
{
    public partial class Form1 : Form
    {
        ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        bool administrator;

        /// <summary>
        /// Settings from selected ini file
        /// </summary>
        protected SettingsModel SettingsFromIni = new SettingsModel();

        /// <summary>
        /// Sets if Files are encrypted or not
        /// </summary>
        protected FilesEncryptStatus encrStatus;

        protected FireAndForget fireAndforget;

        public Form1()
        {
            InitializeComponent();
            //Config Automapper
            AutoMapperConfig.RegisterMappings();
        }

        /// <summary>
        /// true if user is administrator
        /// </summary>
        /// <param name="administrator"></param>
        public Form1(bool administrator)
        {
            InitializeComponent();
            this.administrator = administrator;
        }

        /// <summary>
        /// Instance for Jobs
        /// </summary>
        protected JobsConfig jobsConfig;

        /// <summary>
        /// Instance For Text Config
        /// </summary>
        protected TextConfig textConfig;
        protected ApiUsersConfig apiusersConfig;
        protected ScriptsConfig scriptsConfig;
        protected ApiSettingsConfig apiConfig;
        bool init = true;

        /// <summary>
        /// Instance for confing Helper
        /// </summary>
        protected ConfigHelper configHLP;

        public void Form1_Load(object sender, EventArgs e)
        {
            configHLP = new ConfigHelper();
            encrStatus = FilesState.GetEncryptFilesStatus();
            initJobs();
           
            InitScripts();
            initSettings();
            initApiSettings();
            initApiUsers();
            init = false;

            if (!administrator)
            {
                JobsServertb.Enabled = false;
                JobsDBtb.Enabled = false;
                JobsUsertb.Enabled = false;
                JobsPasswordtb.Enabled = false;
                JobsIsActivecb.Enabled = false;
                JobsDachboardcb.Enabled = false;
                JobsListgv.AllowUserToAddRows = false;
                JobsListgv.AllowUserToDeleteRows = false;
                JobsListgv.Columns["Settings"].ReadOnly = true;
                JobsListgv.Columns["ID"].ReadOnly = true;
                JobsListgv.Refresh();


                tabControl1.TabPages.Remove(tabControl1.TabPages[5]);
                tabControl1.TabPages.Remove(tabControl1.TabPages[4]);
                tabControl1.TabPages.Remove(tabControl1.TabPages[3]);
                tabControl1.TabPages.Remove(tabControl1.TabPages[2]);
                // tabControl1.TabPages.Remove(tabControl1.TabPages[1]);
                SettingsCreateNew.Visible = false;
                SettingsDeleteFile.Visible = false;

            }
        }

        #region "Text"
       private void DecryptBT_Click(object sender, EventArgs e)
        {
           
                TextClearTxt.Text = "";
            TextClearTxt.Text = textConfig.Decrypt(TextEncryptedTxt.Text);
        }

        private void EncryptBT_Click(object sender, EventArgs e)
        {
            TextEncryptedTxt.Text = "";
            TextEncryptedTxt.Text = textConfig.Encrypt(TextClearTxt.Text);
        }
        #endregion

        #region "Jobs"

        private void initJobs()
        {
            jobsConfig = new JobsConfig();
            textConfig = new TextConfig();
            fireAndforget = new FireAndForget();
            Jobs_FillControls();
        }
        private void Jobs_FillControls()
        {
            try
            {
                jobsConfig.GetJobs();
                JobsServertb.Text = jobsConfig.Scheduler.SchedulerDBServer;
                JobsDBtb.Text = jobsConfig.Scheduler.SchedulerDB;
                JobsUsertb.Text = jobsConfig.Scheduler.SchedulerDBUser;
                JobsPasswordtb.Text = jobsConfig.Scheduler.SchedulerDBPassword;
                JobsIsActivecb.Checked = jobsConfig.Scheduler.IsActive;
                JobsDachboardcb.Checked = jobsConfig.Scheduler.Dashboard;
                Installationtb.Text = (jobsConfig.Scheduler.Installation??"");
                JobsInitGrid();
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }


        /// <summary>
        /// Return a new DataGridViewComboBoxColumn
        /// </summary>
        /// <param name="dataSource">DataSource for the combo</param>
        /// <param name="name">combo's unique name</param>
        /// <param name="headerText">column's header text </param>
        /// <param name="DataPropertyName"></param>
        /// <param name="valueMember">combo's value</param>
        /// <param name="displayMember">combo's display member</param>
        /// <param name="style">combo's style (oprional)</param>
        /// <returns></returns>
        DataGridViewComboBoxColumn createComboBoxColumn(List<string> dataSource, String name, String headerText, String DataPropertyName, DataGridViewComboBoxDisplayStyle style = DataGridViewComboBoxDisplayStyle.Nothing)
        {
            DataGridViewComboBoxColumn combo = new DataGridViewComboBoxColumn();
            combo.DataSource = dataSource;
            combo.Name = name;
            combo.HeaderText = headerText;
            combo.DataPropertyName = DataPropertyName;
            combo.DisplayStyle = style;
            
            return combo;
        }

        

        private void JobsListgv_CellValueChanged_1(object sender, DataGridViewCellEventArgs e)
        {
            if (init == true) return;
            
            if (e.ColumnIndex == 6)//settings
            {
               string settingsfile= JobsListgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
              SettingsModel sm= jobsConfig.GetSettingsModel(settingsfile);
                if (sm.ClassType!=null && sm.ClassType.ToUpper() == "JOB")
                    JobsListgv.Rows[e.RowIndex].Cells["ClassName"].Value = sm.FullClassName;
                else
                {
                    JobsListgv.Rows[e.RowIndex].Cells["ClassName"].Value = null;
                    MessageBox.Show("The sellected Settings File does not correspond to a Job.");
                }

            }
           
        }


        private void JobsSaveBt_Click(object sender, EventArgs e)
        {
           
            jobsConfig.Scheduler.SchedulerDBServer = JobsServertb.Text;
            jobsConfig.Scheduler.SchedulerDB = JobsDBtb.Text;
            jobsConfig.Scheduler.SchedulerDBUser = JobsUsertb.Text;
            jobsConfig.Scheduler.SchedulerDBPassword = JobsPasswordtb.Text;
            jobsConfig.Scheduler.IsActive = JobsIsActivecb.Checked;
            jobsConfig.Scheduler.Dashboard = JobsDachboardcb.Checked;
            jobsConfig.Scheduler.Installation = Installationtb.Text.Trim();

            if (string.IsNullOrEmpty(jobsConfig.Scheduler.SchedulerDBServer) || string.IsNullOrEmpty(jobsConfig.Scheduler.SchedulerDB) ||
                string.IsNullOrEmpty(jobsConfig.Scheduler.SchedulerDBUser) ||
                string.IsNullOrEmpty(jobsConfig.Scheduler.SchedulerDBPassword) )
            {
                MessageBox.Show("Incorrect Scheduler DB parameters", "ERROR");
                return;
            }
            if (string.IsNullOrEmpty(jobsConfig.Scheduler.Installation))
            {
                MessageBox.Show("Installation must not be empty", "ERROR");
                return;
            }
           

            try
            {
                bool deljobs = false;
                //Remove empty jobs
                for (int i = jobsConfig.Scheduler.Jobs.Count - 1; i >=0; i--)
                {
                    if (jobsConfig.Scheduler.Jobs[i].ID == null && jobsConfig.Scheduler.Jobs[i].Description == null
                        && string.IsNullOrEmpty(jobsConfig.Scheduler.Jobs[i].ClassName) && jobsConfig.Scheduler.Jobs[i].Settings == null
                        )
                    {
                        jobsConfig.Scheduler.Jobs.RemoveAt(i);
                        deljobs = true;
                    }
                }
                if (deljobs) bindJobs();
               

                //chekc for errors
                bool joberror = false;
                string err = SchedulerJobValidate(out joberror);
               

                if ((jobsConfig.Scheduler.Jobs.Count == 0 && joberror) ||
                    (err==""))
                {
                    jobsConfig.SaveSettings(jobsConfig.Scheduler); //save 
                    HangFireDB.ConString = configHLP.ConnectionString(jobsConfig.Scheduler.SchedulerDBServer, jobsConfig.Scheduler.SchedulerDB, jobsConfig.Scheduler.SchedulerDBUser, jobsConfig.Scheduler.SchedulerDBPassword);
                    if (jobsConfig.Scheduler.Installation == "UnknownCustomer")
                    {
                        MessageBox.Show("Jobs are saved!" + Environment.NewLine + "For better recognition Change the Installation from 'UnknownCustomer' to customer's real name and Save again.", "Save with Warning");
                    }
                    else
                    {
                        MessageBox.Show("Jobs are saved!", "SAVE");
                    }
                   

                    //restart hangfire server
                    fireAndforget = null;
                    fireAndforget = new FireAndForget();
                }
                else
                    MessageBox.Show(err, "ERROR");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Saving Jobs");
            }

        }


        private void JobsInitGrid()
        {
            List<string> settingsFiles = jobsConfig.GetSettingsfiles();
            foreach (var job in jobsConfig.Scheduler.Jobs)
            {
                if (settingsFiles.Find(x => x == job.Settings) == null)
                {
                    job.Settings = null;
                    log.Error(job.Settings + "not found, and this setting file is removed from job " + job.ID);
                }
            }
            
            bindJobs();
            JobsListgv.AllowUserToAddRows = true;
            
           // JobsListgv.Columns.Remove("ClassName");
          //  JobsListgv.Columns.Add(createComboBoxColumn(jobsConfig.GetJobs(), "ClassName", "Class", "ClassName"));
            JobsListgv.Columns.Remove("Settings");
            JobsListgv.Columns.Add(createComboBoxColumn(settingsFiles, "Settings", "Settings", "Settings"));

            JobsListgv.Columns["Description"].Width = 200;
            JobsListgv.Columns["Description"].DisplayIndex = 1;
            JobsListgv.Columns["Settings"].DisplayIndex = 2;
            JobsListgv.Columns["Settings"].Width = 200;
            JobsListgv.Columns["ClassName"].Width = 240;
            JobsListgv.Columns["ClassName"].DisplayIndex = 3;
            JobsListgv.Columns["IsActive"].DisplayIndex = 4;
            JobsListgv.Columns["IsActive"].Width = 60;
            JobsListgv.Columns["IsActive"].HeaderText = "Active";
            JobsListgv.Columns["Schedule"].DisplayIndex = 5;
            JobsListgv.Columns["Schedule"].Width = 250;


            JobsListgv.Columns["ID"].Width = 120; 

            JobsListgv.Columns["Cron"].Width = 60;

            JobsListgv.Columns["Cron"].ReadOnly = true;
            JobsListgv.Columns["Schedule"].ReadOnly = true;
            JobsListgv.Columns["ClassName"].ReadOnly = true;
            JobsListgv.Refresh();
        }

        private void bindJobs()
        {
            var bindingList = new BindingList<SchedulerJob>(jobsConfig.Scheduler.Jobs);
            // var source = new BindingSource(bindingList, null);

            JobsListgv.DataSource = null;
            JobsListgv.Refresh();
            JobsListgv.DataSource = bindingList;
        }

        /// <summary>
        /// Validate datagrid JobsListgv
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void JobsListgv_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            //1. get the name of the column
            string column = JobsListgv.Columns[e.ColumnIndex].Name;

            //validate cell in column 'ID' (must not be empty)
            if (column == "ID" || column == "Description" || column == "ClassName" || column == "Settings" || column == "Cron")
            {
                string value = (JobsListgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value ?? "").ToString();
                if (value == null || value.Trim() == "")
                {
                    JobsListgv.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = "Cell must not be empty"; //show red icon
                }
                else
                {
                    JobsListgv.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = "";
                }
            }
        }

        private void JobsListgv_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            string error = "Cell must not be empty";
            if (JobsListgv.Rows[e.RowIndex].Cells["Description"].Value==null)
                JobsListgv.Rows[e.RowIndex].Cells["Description"].ErrorText = error; //show red icon
            else
                JobsListgv.Rows[e.RowIndex].Cells["Description"].ErrorText = "";
            if (JobsListgv.Rows[e.RowIndex].Cells["ID"].Value == null)
                JobsListgv.Rows[e.RowIndex].Cells["ID"].ErrorText = error; //show red icon
            else
                JobsListgv.Rows[e.RowIndex].Cells["ID"].ErrorText = "";
            if (JobsListgv.Rows[e.RowIndex].Cells["ClassName"].Value == null)
                JobsListgv.Rows[e.RowIndex].Cells["ClassName"].ErrorText = error; //show red icon
            else
                JobsListgv.Rows[e.RowIndex].Cells["ClassName"].ErrorText = "";
            if (JobsListgv.Rows[e.RowIndex].Cells["Settings"].Value == null)
                JobsListgv.Rows[e.RowIndex].Cells["Settings"].ErrorText = error; //show red icon
            else
                JobsListgv.Rows[e.RowIndex].Cells["Settings"].ErrorText = "";
        }

        /// <summary>
        /// Check if datagrid JobsListgv has Error Texts
        /// </summary>
        /// <returns></returns>
        private bool JobsListHasErrorText()
        {
            bool hasErrorText = false;
            //replace this.dataGridView1 with the name of your datagridview control
            foreach (DataGridViewRow row in this.JobsListgv.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.ErrorText.Length > 0)
                    {
                        hasErrorText = true;
                        break;
                    }
                }
                if (hasErrorText)
                    break;
            }
            return hasErrorText;
        }

        /// <summary>
        /// Validate SchedulerJob model. Return the error
        /// </summary>
        /// <returns></returns>
        private string SchedulerJobValidate(out bool joberror)
        {
            joberror = false;
           
            if (JobsServertb.Text.Trim() == "")
            {
                return "Server must have a value.";
            }
            if (JobsDBtb.Text.Trim() == "")
            {
                return "DB must have a value.";
            }
            if (JobsUsertb.Text.Trim() == "")
            {
                return "User must have a value.";
            }
            if (JobsPasswordtb.Text.Trim() == "")
            {
                return "Password must have a value.";
            }

            bool errors = JobsListHasErrorText();
            if (errors)
            {
                joberror = true;
                return "Fix job errors to save.";
            }

            //job Id's must be unique
            for (int i = 0; i < jobsConfig.Scheduler.Jobs.Count - 1; i++)
            {
                for (int j = i + 1; j < jobsConfig.Scheduler.Jobs.Count; j++)
                {
                    if (jobsConfig.Scheduler.Jobs[i].ID == jobsConfig.Scheduler.Jobs[j].ID)
                    {
                        joberror = true;
                        return "Job Id '" + jobsConfig.Scheduler.Jobs[i].ID + "' is not unique.";
                    }
                }
            }

            for (int i = 0; i < jobsConfig.Scheduler.Jobs.Count; i++)
            {
                if (string.IsNullOrEmpty(jobsConfig.Scheduler.Jobs[i].ID))
                    return "Row " + (i + 1).ToString() + " : Id must not be emprty";
                if (string.IsNullOrEmpty(jobsConfig.Scheduler.Jobs[i].Description))
                    return "Row " + (i + 1).ToString() + " : Description must not be emprty";
                if (string.IsNullOrEmpty(jobsConfig.Scheduler.Jobs[i].ClassName))
                    return "Row " + (i + 1).ToString() + " : ClassName must not be emprty";
                if (string.IsNullOrEmpty(jobsConfig.Scheduler.Jobs[i].Settings))
                    return "Row " + (i+1).ToString() + " : Settings must not be emprty";
            }
                return "";
        }

        private void JobsReset_Click(object sender, EventArgs e)
        {
            initJobs();
        }

        /// <summary>
        /// Set Cron expression for a Job
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void JobsScheduleBt_Click(object sender, EventArgs e)
        {

            int row = JobsListgv.CurrentCell.RowIndex;
            if (row == -1)
            {
                MessageBox.Show("Select a Job first");
                return;
            }
            if (
               jobsConfig.Scheduler.Jobs[row].ID == "" || jobsConfig.Scheduler.Jobs[row].ID == null
                || jobsConfig.Scheduler.Jobs[row].ClassName == "" || jobsConfig.Scheduler.Jobs[row].ClassName == null
                || jobsConfig.Scheduler.Jobs[row].Settings == "" || jobsConfig.Scheduler.Jobs[row].Settings == null
                || JobsListHasErrorText()
               )
            {
                MessageBox.Show("ID, ClassName and Settings are required for the Job to set a Schedule.");
                return;
            }

            //this.Hide();
            CronForm f1 = new CronForm(jobsConfig.Scheduler.Jobs[row].ID, jobsConfig.Scheduler.Jobs[row].Cron);
            f1.ShowDialog();
         //   this.Show();

            if (CronSchedule.Cron == null || CronSchedule.Description == null) return;
            jobsConfig.Scheduler.Jobs[row].Cron = CronSchedule.Cron;
            jobsConfig.Scheduler.Jobs[row].Schedule = CronSchedule.Description;
        }

        /// <summary>
        /// Fire-And-forget job.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void JobsRunJob_Click(object sender, EventArgs e)
        {
            int row = JobsListgv.CurrentCell.RowIndex;
            if (row == -1)
            {
                MessageBox.Show("Select a Job first");
                return;
            }

            if (
                jobsConfig.Scheduler.Jobs[row].ID=="" || jobsConfig.Scheduler.Jobs[row].ID == null 
                 || jobsConfig.Scheduler.Jobs[row].ClassName == "" || jobsConfig.Scheduler.Jobs[row].ClassName == null
                 || jobsConfig.Scheduler.Jobs[row].Settings == ""  || jobsConfig.Scheduler.Jobs[row].Settings == null
                )
            {
                MessageBox.Show("ID, ClassName and Settings are required for the Job to run.");
                return;
            }

            DialogResult dialogResult = MessageBox.Show("You are going to run Job: '"+ jobsConfig.Scheduler.Jobs[row].ID+"'", "Fire-And-forget", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.No) return;
            try
            {
                string err = fireAndforget.Execute(jobsConfig.Scheduler.Jobs[row], jobsConfig.Scheduler.Installation);

                if (err == "")
                    MessageBox.Show("Job: '" + jobsConfig.Scheduler.Jobs[row].ID + "' is running." + Environment.NewLine + " Do NOT close this application before execution ends." + Environment.NewLine + "(See log files or Dashboard)", "Fire-And-forget");
                else
                    MessageBox.Show(err, "ERROR");
            }catch(Exception ex)
            {
                log.Error(ex.ToString());
                MessageBox.Show(ex.ToString(), "ERROR");
            }
         
        }
        #endregion

        #region "ApiUsers"
        public void initApiUsers()
        {
            apiusersConfig = new ApiUsersConfig();

            var bindingList = new BindingList<LoginModel>(apiusersConfig.Users);
            
            ApiUsersGv.DataSource = null;
             ApiUsersGv.Refresh();
            ApiUsersGv.DataSource = bindingList;
            ApiUsersGv.AllowUserToAddRows = true;

            ApiUsersGv.Columns.Remove("AccessTo");
            ApiUsersGv.Refresh();
            ApiUsersGv.Columns.Add(createComboBoxColumn(apiConfig.GetControllers(), "AccessTo", "AccessTo", "AccessTo"));
            ApiUsersGv.Columns["Notes"].Width = 300;
            ApiUsersGv.Columns["Password"].Width = 90;
            ApiUsersGv.Columns["AccessTo"].Width = 360;
            ApiUsersGv.Columns["Password"].DefaultCellStyle.BackColor = Color.Black;
            //ApiUsersGv.Columns["AccessTo"].DisplayIndex = 2;
            //ApiUsersGv.Columns["Notes"].DisplayIndex = 3;
            if (!administrator)
            {
                ApiUsersGv.AllowUserToAddRows = false;
                ApiUsersGv.AllowUserToDeleteRows = false;
                ApiUsersGv.Columns["AccessTo"].ReadOnly = true;
                ApiUsersbt.Enabled = false;


            }

            ApiUsersGv.Refresh();
        }

        private void apiUsersSavebt_Click(object sender, EventArgs e)
        {
            bool errors = UsersListHasErrorText();
            if (errors)
            {
                MessageBox.Show("Fix errors to save.");
                return;
            }
            apiusersConfig.SaveUsers();
        }

        private void ApiUsersGv_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            string value = (ApiUsersGv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value ?? "").ToString();
            if (value == null || value.Trim() == "")
            {
                ApiUsersGv.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = "Cell must not be empty"; //show red icon
            }
            else
            {
                ApiUsersGv.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = "";
            }

        }


        /// <summary>
        /// Check if datagrid ApiUsersGv has Error Texts
        /// </summary>
        /// <returns></returns>
        private bool UsersListHasErrorText()
        {
            if (this.ApiUsersGv.Rows.Count <= 1) return false;
            bool hasErrorText = false;
            //replace this.dataGridView1 with the name of your datagridview control
            foreach (DataGridViewRow row in this.ApiUsersGv.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.ErrorText.Length > 0)
                    {
                        hasErrorText = true;
                        break;
                    }
                }
                if (hasErrorText)
                    break;
            }
            return hasErrorText;
        }



       

        private void ApiUsersGv_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                ApiUsersGv.CurrentCell.Style.BackColor = Color.White;
            }


        }

        private void ApiUsersGv_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                ApiUsersGv.CurrentCell.Style.BackColor = Color.Black;
            }

        }

        #endregion

        #region "Scripts"
        public void InitScripts()
        {
            scriptsConfig = new ScriptsConfig();
            ScriptsCB.DataSource = null;
            //ScriptsCB.DataSource = scriptsConfig.Files;
            //ScriptsCB.DisplayMember = "Files";
            //ScriptsCB.ValueMember = "Files";
            ScriptsCB.Items.Clear();
            foreach (string item in scriptsConfig.Files)
            {
                ScriptsCB.Items.Add(item);
            }

            if (!administrator)
            {
                ScriptDeleteBt.Enabled = false;
                groupBox2.Enabled = false;
                ScriptSaveBt.Enabled = false;
            }
        }


        private void ScriptsCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (init) return;
            ScriptTB.Text = scriptsConfig.Readfile(ScriptsCB.SelectedItem.ToString());
        }


        private void ScriptCreateBt_Click(object sender, EventArgs e)
        {
            ScriptNewTb.Text = ScriptNewTb.Text.Trim().Replace("/", "").Replace("\\", "").Replace("<", "").Replace(">", "").Replace(":", "").Replace("|", "").Replace("?", "").Replace("*", "");
            if (ScriptNewTb.Text == "")
            {
                MessageBox.Show("Set a unique name for the new script");
                return;
            }

            foreach (string name in scriptsConfig.Files)
            {
                if (name == ScriptNewTb.Text)
                {
                    MessageBox.Show("Script name already exists. Choose a unique name");
                    return;
                }
            }
            bool result = scriptsConfig.Writefile(ScriptNewTb.Text, "");
            if (!result) return;
            InitScripts();
            ScriptsCB.SelectedIndex = ScriptsCB.FindString(ScriptNewTb.Text);
            ScriptNewTb.Text = "";
            
        }

        private void ScriptSaveBt_Click(object sender, EventArgs e)
        {
            if (ScriptsCB.SelectedItem == null) return;
            DialogResult dialogResult = MessageBox.Show("Do you want to save the script '" + ScriptsCB.SelectedItem.ToString() + "'?", "Save Script", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                scriptsConfig.Writefile(ScriptsCB.SelectedItem.ToString(), ScriptTB.Text);
            }
        }

        private void ScriptDeleteBt_Click(object sender, EventArgs e)
        {
            if (ScriptsCB.SelectedItem == null) return;

            DialogResult dialogResult = MessageBox.Show("Do you want to delete the script '" + ScriptsCB.SelectedItem.ToString() + "'?", "Delete Script", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                bool result = scriptsConfig.Deletefile(ScriptsCB.SelectedItem.ToString());
                if (result)
                {
                    InitScripts();
                    ScriptNewTb.Text = "";
                    ScriptsCB.Text = "";
                    initJobs();
                }
            }


        }






        #endregion

        #region "ApiSettings"

        public void initApiSettings(){
            apiConfig = new ApiSettingsConfig();
            
            //get settings files
            List<string> settingsFiles = jobsConfig.GetSettingsfiles();

            //from ApiSettings remove settings files that no longer exist
            foreach (var job in apiConfig.ApiSettings)
            {
                if (settingsFiles.Find(x => x == job.Settings) == null)
                {
                    job.Settings = null;
                    log.Error(job.Settings + "not found, and this setting file is removed from Controller " + job.Controller);
                }
            }

            //bind
            bingApiSettings(settingsFiles);
            if (!administrator)
            {
                ApiSettingsGv.AllowUserToDeleteRows = false;
                ApiSettingsGv.AllowUserToAddRows = false;
                ApiSettingsGv.Columns["Settings"].ReadOnly = true;
                ApiSettingsBt.Enabled = false;

            }
        }

        private void bingApiSettings(List<string> settingsFiles)
        {
          
            var bindingList = new BindingList<WebApiSettingsModel>(apiConfig.ApiSettings);
            ApiSettingsGv.DataSource = null;
            ApiSettingsGv.Refresh();
            ApiSettingsGv.DataSource = bindingList;

            ApiSettingsGv.AllowUserToAddRows = true;

           // ApiSettingsGv.Columns.Remove("Controller");
          //  ApiSettingsGv.Columns.Add(createComboBoxColumn(apiConfig.GetControllers(), "Controller", "Controller", "Controller"));
            ApiSettingsGv.Columns.Remove("Settings");
            ApiSettingsGv.Columns.Add(createComboBoxColumn(settingsFiles, "Settings", "Settings", "Settings"));
            ApiSettingsGv.Columns["Controller"].Width = 400;
            ApiSettingsGv.Columns["Controller"].ReadOnly = true;
            ApiSettingsGv.Columns["Settings"].Width = 400;
            ApiSettingsGv.Refresh();
        }

        private void ApiSettingsBt_Click(object sender, EventArgs e)
        {
            //Remove empty  controllers
            //for (int i = apiConfig.ApiSettings.Count - 1; i >= 0; i--)
            //{
            //    if (apiConfig.ApiSettings[i].Controller == null || apiConfig.ApiSettings[i].Controller == "")
            //    {
            //        apiConfig.ApiSettings.RemoveAt(i);
            //    }
            //}

            for (int i = 0; i < apiConfig.ApiSettings.Count; i++)
            {
                if (string.IsNullOrEmpty(apiConfig.ApiSettings[i].Controller))
                {
                    MessageBox.Show("Row " + (i + 1).ToString() + " : Controller must not be emprty");
                    return;
                }
                   
                if (string.IsNullOrEmpty(apiConfig.ApiSettings[i].Settings))
                {
                    MessageBox.Show("Row " + (i + 1).ToString() + " : Settings must not be emprty");
                    return;
                }
                    
           
            }

            //check for dublicates
            for (int i = 0; i < apiConfig.ApiSettings.Count - 1; i++)
            {
                for (int j = i + 1; j < apiConfig.ApiSettings.Count; j++)
                {
                    if (apiConfig.ApiSettings[i].Controller == apiConfig.ApiSettings[j].Controller)
                    {
                        MessageBox.Show("Controller '" + apiConfig.ApiSettings[i].Controller + "' is not unique");
                        return;
                    }
                }
            }

            apiConfig.SaveUsers();
            List<string> settingsFiles = jobsConfig.GetSettingsfiles();
            bingApiSettings(settingsFiles);
        }

        private void ApiSettingsGv_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (init == true) return;

            if (e.ColumnIndex == 1)//settings
            { 
                string settingsfile = ApiSettingsGv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                SettingsModel sm = jobsConfig.GetSettingsModel(settingsfile);
                if (sm.ClassType != null && sm.ClassType.ToUpper() == "CONTROLLER")
                    ApiSettingsGv.Rows[e.RowIndex].Cells["Controller"].Value = sm.FullClassName;
                else
                {
                    ApiSettingsGv.Rows[e.RowIndex].Cells["Controller"].Value = null;
                    MessageBox.Show("The sellected Settings File does not correspond to a Controller.");
                }
            }
        }

        #endregion

        private void DBConString_Leave(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;
            tb.BackColor = Color.Black;
        }

        private void DBConString_Enter(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;
            tb.BackColor = Color.White;
        }

        
    }




}
