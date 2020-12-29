namespace EncryptDecrypt
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.TextEncryptBT = new System.Windows.Forms.Button();
            this.TextEncryptedTxt = new System.Windows.Forms.TextBox();
            this.TextClearTxt = new System.Windows.Forms.TextBox();
            this.Encryptedlb = new System.Windows.Forms.Label();
            this.Clearlb = new System.Windows.Forms.Label();
            this.TextDecryptBT = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.Installationtb = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.JobsRunJob = new System.Windows.Forms.Button();
            this.JobsScheduleBt = new System.Windows.Forms.Button();
            this.JobsReset = new System.Windows.Forms.Button();
            this.Jobs = new System.Windows.Forms.Label();
            this.JobsListgv = new System.Windows.Forms.DataGridView();
            this.JobsDachboardcb = new System.Windows.Forms.CheckBox();
            this.JobsIsActivecb = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.JobsPasswordtb = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.JobsUsertb = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.JobsDBtb = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.JobsServertb = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.JobsSaveBt = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.SettingsPnlSelection = new System.Windows.Forms.Panel();
            this.SettingsTabComp = new System.Windows.Forms.TabControl();
            this.settingsGroupTop = new System.Windows.Forms.GroupBox();
            this.SettingsUpdateFiles = new System.Windows.Forms.GroupBox();
            this.SettingsBtnReset = new System.Windows.Forms.Button();
            this.settingsBtnSaveNew = new System.Windows.Forms.Button();
            this.SettingsDeleteFile = new System.Windows.Forms.Button();
            this.SettingsLblFileGrid = new System.Windows.Forms.Label();
            this.SettingsFileGrid = new System.Windows.Forms.ComboBox();
            this.SettingsLoadFiles = new System.Windows.Forms.Button();
            this.SettingsCreateNew = new System.Windows.Forms.GroupBox();
            this.settingsCtlSelection = new System.Windows.Forms.ComboBox();
            this.SettingsCreateFileName = new System.Windows.Forms.Button();
            this.SettingsCnNewSettings = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.SettingsCnCreate = new System.Windows.Forms.ToolStripMenuItem();
            this.SettingsCnCopyFrom = new System.Windows.Forms.ToolStripMenuItem();
            this.SettingsNewFileName = new System.Windows.Forms.TextBox();
            this.SettingsNewFile = new System.Windows.Forms.Label();
            this.SettingsLblSelection = new System.Windows.Forms.Label();
            this.SettingsButtons = new System.Windows.Forms.GroupBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.ScriptDeleteBt = new System.Windows.Forms.Button();
            this.ScriptSaveBt = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ScriptNewTb = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.ScriptCreateBt = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.ScriptsCB = new System.Windows.Forms.ComboBox();
            this.ScriptTB = new System.Windows.Forms.TextBox();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.ApiSettingsBt = new System.Windows.Forms.Button();
            this.ApiSettingsGv = new System.Windows.Forms.DataGridView();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.ApiUsersbt = new System.Windows.Forms.Button();
            this.ApiUsersGv = new System.Windows.Forms.DataGridView();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.apiUsersSavebt = new System.Windows.Forms.Button();
            this.label105 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.JobsListgv)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SettingsPnlSelection.SuspendLayout();
            this.settingsGroupTop.SuspendLayout();
            this.SettingsUpdateFiles.SuspendLayout();
            this.SettingsCreateNew.SuspendLayout();
            this.SettingsCnNewSettings.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabPage6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ApiSettingsGv)).BeginInit();
            this.tabPage5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ApiUsersGv)).BeginInit();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // TextEncryptBT
            // 
            this.TextEncryptBT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.TextEncryptBT.Location = new System.Drawing.Point(829, 600);
            this.TextEncryptBT.Name = "TextEncryptBT";
            this.TextEncryptBT.Size = new System.Drawing.Size(75, 23);
            this.TextEncryptBT.TabIndex = 0;
            this.TextEncryptBT.Text = "Encrypt";
            this.TextEncryptBT.UseVisualStyleBackColor = true;
            this.TextEncryptBT.Click += new System.EventHandler(this.EncryptBT_Click);
            // 
            // TextEncryptedTxt
            // 
            this.TextEncryptedTxt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextEncryptedTxt.Location = new System.Drawing.Point(64, 6);
            this.TextEncryptedTxt.Multiline = true;
            this.TextEncryptedTxt.Name = "TextEncryptedTxt";
            this.TextEncryptedTxt.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.TextEncryptedTxt.Size = new System.Drawing.Size(757, 280);
            this.TextEncryptedTxt.TabIndex = 2;
            // 
            // TextClearTxt
            // 
            this.TextClearTxt.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextClearTxt.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.TextClearTxt.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.AllSystemSources;
            this.TextClearTxt.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.TextClearTxt.Location = new System.Drawing.Point(64, 295);
            this.TextClearTxt.Multiline = true;
            this.TextClearTxt.Name = "TextClearTxt";
            this.TextClearTxt.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.TextClearTxt.Size = new System.Drawing.Size(757, 328);
            this.TextClearTxt.TabIndex = 3;
            this.TextClearTxt.WordWrap = false;
            // 
            // Encryptedlb
            // 
            this.Encryptedlb.AutoSize = true;
            this.Encryptedlb.Location = new System.Drawing.Point(3, 19);
            this.Encryptedlb.Name = "Encryptedlb";
            this.Encryptedlb.Size = new System.Drawing.Size(55, 13);
            this.Encryptedlb.TabIndex = 5;
            this.Encryptedlb.Text = "Encrypted";
            // 
            // Clearlb
            // 
            this.Clearlb.AutoSize = true;
            this.Clearlb.Location = new System.Drawing.Point(3, 295);
            this.Clearlb.Name = "Clearlb";
            this.Clearlb.Size = new System.Drawing.Size(31, 13);
            this.Clearlb.TabIndex = 6;
            this.Clearlb.Text = "Clear";
            // 
            // TextDecryptBT
            // 
            this.TextDecryptBT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TextDecryptBT.Location = new System.Drawing.Point(827, 263);
            this.TextDecryptBT.Name = "TextDecryptBT";
            this.TextDecryptBT.Size = new System.Drawing.Size(75, 23);
            this.TextDecryptBT.TabIndex = 7;
            this.TextDecryptBT.Text = "Decrypt";
            this.TextDecryptBT.UseVisualStyleBackColor = true;
            this.TextDecryptBT.Click += new System.EventHandler(this.DecryptBT_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage6);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(918, 657);
            this.tabControl1.TabIndex = 8;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.Installationtb);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.JobsRunJob);
            this.tabPage2.Controls.Add(this.JobsScheduleBt);
            this.tabPage2.Controls.Add(this.JobsReset);
            this.tabPage2.Controls.Add(this.Jobs);
            this.tabPage2.Controls.Add(this.JobsListgv);
            this.tabPage2.Controls.Add(this.JobsDachboardcb);
            this.tabPage2.Controls.Add(this.JobsIsActivecb);
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Controls.Add(this.JobsSaveBt);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(910, 631);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Jobs";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // Installationtb
            // 
            this.Installationtb.Location = new System.Drawing.Point(296, 27);
            this.Installationtb.Name = "Installationtb";
            this.Installationtb.Size = new System.Drawing.Size(97, 20);
            this.Installationtb.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(295, 11);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(57, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Installation";
            // 
            // JobsRunJob
            // 
            this.JobsRunJob.Location = new System.Drawing.Point(525, 13);
            this.JobsRunJob.Name = "JobsRunJob";
            this.JobsRunJob.Size = new System.Drawing.Size(94, 38);
            this.JobsRunJob.TabIndex = 16;
            this.JobsRunJob.Text = "Fire-And-Forget";
            this.JobsRunJob.UseVisualStyleBackColor = true;
            this.JobsRunJob.Click += new System.EventHandler(this.JobsRunJob_Click);
            // 
            // JobsScheduleBt
            // 
            this.JobsScheduleBt.Location = new System.Drawing.Point(525, 61);
            this.JobsScheduleBt.Name = "JobsScheduleBt";
            this.JobsScheduleBt.Size = new System.Drawing.Size(94, 38);
            this.JobsScheduleBt.TabIndex = 15;
            this.JobsScheduleBt.Text = "Schedule Job";
            this.JobsScheduleBt.UseVisualStyleBackColor = true;
            this.JobsScheduleBt.Click += new System.EventHandler(this.JobsScheduleBt_Click);
            // 
            // JobsReset
            // 
            this.JobsReset.Location = new System.Drawing.Point(411, 12);
            this.JobsReset.Name = "JobsReset";
            this.JobsReset.Size = new System.Drawing.Size(98, 38);
            this.JobsReset.TabIndex = 14;
            this.JobsReset.Text = "Reset";
            this.JobsReset.UseVisualStyleBackColor = true;
            this.JobsReset.Click += new System.EventHandler(this.JobsReset_Click);
            // 
            // Jobs
            // 
            this.Jobs.AutoSize = true;
            this.Jobs.Location = new System.Drawing.Point(9, 162);
            this.Jobs.Name = "Jobs";
            this.Jobs.Size = new System.Drawing.Size(29, 13);
            this.Jobs.TabIndex = 13;
            this.Jobs.Text = "Jobs";
            // 
            // JobsListgv
            // 
            this.JobsListgv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.JobsListgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.JobsListgv.Location = new System.Drawing.Point(8, 182);
            this.JobsListgv.MultiSelect = false;
            this.JobsListgv.Name = "JobsListgv";
            this.JobsListgv.Size = new System.Drawing.Size(894, 441);
            this.JobsListgv.TabIndex = 5;
            this.JobsListgv.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.JobsListgv_CellValidated);
            this.JobsListgv.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.JobsListgv_CellValueChanged_1);
            this.JobsListgv.RowLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.JobsListgv_RowEnter);
            // 
            // JobsDachboardcb
            // 
            this.JobsDachboardcb.AutoSize = true;
            this.JobsDachboardcb.Location = new System.Drawing.Point(296, 79);
            this.JobsDachboardcb.Name = "JobsDachboardcb";
            this.JobsDachboardcb.Size = new System.Drawing.Size(78, 17);
            this.JobsDachboardcb.TabIndex = 4;
            this.JobsDachboardcb.Text = "Dashboard";
            this.JobsDachboardcb.UseVisualStyleBackColor = true;
            // 
            // JobsIsActivecb
            // 
            this.JobsIsActivecb.AutoSize = true;
            this.JobsIsActivecb.Location = new System.Drawing.Point(296, 58);
            this.JobsIsActivecb.Name = "JobsIsActivecb";
            this.JobsIsActivecb.Size = new System.Drawing.Size(64, 17);
            this.JobsIsActivecb.TabIndex = 3;
            this.JobsIsActivecb.Text = "IsActive";
            this.JobsIsActivecb.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.JobsPasswordtb);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.JobsUsertb);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.JobsDBtb);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.JobsServertb);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(8, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(271, 138);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Scheduler DB";
            // 
            // JobsPasswordtb
            // 
            this.JobsPasswordtb.Location = new System.Drawing.Point(75, 97);
            this.JobsPasswordtb.Name = "JobsPasswordtb";
            this.JobsPasswordtb.PasswordChar = '@';
            this.JobsPasswordtb.Size = new System.Drawing.Size(169, 20);
            this.JobsPasswordtb.TabIndex = 12;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Password";
            // 
            // JobsUsertb
            // 
            this.JobsUsertb.Location = new System.Drawing.Point(75, 71);
            this.JobsUsertb.Name = "JobsUsertb";
            this.JobsUsertb.Size = new System.Drawing.Size(169, 20);
            this.JobsUsertb.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "User";
            // 
            // JobsDBtb
            // 
            this.JobsDBtb.Location = new System.Drawing.Point(75, 45);
            this.JobsDBtb.Name = "JobsDBtb";
            this.JobsDBtb.Size = new System.Drawing.Size(169, 20);
            this.JobsDBtb.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(22, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "DB";
            // 
            // JobsServertb
            // 
            this.JobsServertb.Location = new System.Drawing.Point(75, 19);
            this.JobsServertb.Name = "JobsServertb";
            this.JobsServertb.Size = new System.Drawing.Size(169, 20);
            this.JobsServertb.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Server";
            // 
            // JobsSaveBt
            // 
            this.JobsSaveBt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.JobsSaveBt.Location = new System.Drawing.Point(411, 61);
            this.JobsSaveBt.Name = "JobsSaveBt";
            this.JobsSaveBt.Size = new System.Drawing.Size(98, 38);
            this.JobsSaveBt.TabIndex = 0;
            this.JobsSaveBt.Text = "Save";
            this.JobsSaveBt.UseVisualStyleBackColor = true;
            this.JobsSaveBt.Click += new System.EventHandler(this.JobsSaveBt_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.SettingsPnlSelection);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(910, 631);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Settings";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // SettingsPnlSelection
            // 
            this.SettingsPnlSelection.Controls.Add(this.SettingsTabComp);
            this.SettingsPnlSelection.Controls.Add(this.settingsGroupTop);
            this.SettingsPnlSelection.Controls.Add(this.SettingsButtons);
            this.SettingsPnlSelection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SettingsPnlSelection.Location = new System.Drawing.Point(0, 0);
            this.SettingsPnlSelection.Name = "SettingsPnlSelection";
            this.SettingsPnlSelection.Size = new System.Drawing.Size(910, 631);
            this.SettingsPnlSelection.TabIndex = 0;
            // 
            // SettingsTabComp
            // 
            this.SettingsTabComp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SettingsTabComp.Location = new System.Drawing.Point(0, 126);
            this.SettingsTabComp.Name = "SettingsTabComp";
            this.SettingsTabComp.SelectedIndex = 0;
            this.SettingsTabComp.Size = new System.Drawing.Size(910, 505);
            this.SettingsTabComp.TabIndex = 10;
            // 
            // settingsGroupTop
            // 
            this.settingsGroupTop.Controls.Add(this.SettingsUpdateFiles);
            this.settingsGroupTop.Controls.Add(this.SettingsCreateNew);
            this.settingsGroupTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.settingsGroupTop.Location = new System.Drawing.Point(0, 0);
            this.settingsGroupTop.Name = "settingsGroupTop";
            this.settingsGroupTop.Size = new System.Drawing.Size(910, 126);
            this.settingsGroupTop.TabIndex = 9;
            this.settingsGroupTop.TabStop = false;
            // 
            // SettingsUpdateFiles
            // 
            this.SettingsUpdateFiles.Controls.Add(this.SettingsBtnReset);
            this.SettingsUpdateFiles.Controls.Add(this.settingsBtnSaveNew);
            this.SettingsUpdateFiles.Controls.Add(this.SettingsDeleteFile);
            this.SettingsUpdateFiles.Controls.Add(this.SettingsLblFileGrid);
            this.SettingsUpdateFiles.Controls.Add(this.SettingsFileGrid);
            this.SettingsUpdateFiles.Controls.Add(this.SettingsLoadFiles);
            this.SettingsUpdateFiles.Location = new System.Drawing.Point(6, 13);
            this.SettingsUpdateFiles.Name = "SettingsUpdateFiles";
            this.SettingsUpdateFiles.Size = new System.Drawing.Size(556, 107);
            this.SettingsUpdateFiles.TabIndex = 1;
            this.SettingsUpdateFiles.TabStop = false;
            this.SettingsUpdateFiles.Text = "Update File";
            // 
            // SettingsBtnReset
            // 
            this.SettingsBtnReset.Location = new System.Drawing.Point(452, 64);
            this.SettingsBtnReset.Name = "SettingsBtnReset";
            this.SettingsBtnReset.Size = new System.Drawing.Size(75, 23);
            this.SettingsBtnReset.TabIndex = 15;
            this.SettingsBtnReset.Text = "Reset";
            this.SettingsBtnReset.UseVisualStyleBackColor = true;
            this.SettingsBtnReset.Click += new System.EventHandler(this.SettingsBtnReset_Click);
            // 
            // settingsBtnSaveNew
            // 
            this.settingsBtnSaveNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.settingsBtnSaveNew.Location = new System.Drawing.Point(6, 61);
            this.settingsBtnSaveNew.Name = "settingsBtnSaveNew";
            this.settingsBtnSaveNew.Size = new System.Drawing.Size(75, 23);
            this.settingsBtnSaveNew.TabIndex = 5;
            this.settingsBtnSaveNew.Text = "Save";
            this.settingsBtnSaveNew.UseVisualStyleBackColor = true;
            this.settingsBtnSaveNew.Click += new System.EventHandler(this.settingsBtnSaveNew_Click);
            // 
            // SettingsDeleteFile
            // 
            this.SettingsDeleteFile.Location = new System.Drawing.Point(452, 33);
            this.SettingsDeleteFile.Name = "SettingsDeleteFile";
            this.SettingsDeleteFile.Size = new System.Drawing.Size(75, 23);
            this.SettingsDeleteFile.TabIndex = 10;
            this.SettingsDeleteFile.Text = "Delete";
            this.SettingsDeleteFile.UseVisualStyleBackColor = true;
            this.SettingsDeleteFile.Click += new System.EventHandler(this.SettingsDeleteFile_Click);
            // 
            // SettingsLblFileGrid
            // 
            this.SettingsLblFileGrid.AutoSize = true;
            this.SettingsLblFileGrid.Location = new System.Drawing.Point(6, 19);
            this.SettingsLblFileGrid.Name = "SettingsLblFileGrid";
            this.SettingsLblFileGrid.Size = new System.Drawing.Size(97, 13);
            this.SettingsLblFileGrid.TabIndex = 9;
            this.SettingsLblFileGrid.Text = "Select Settings File";
            // 
            // SettingsFileGrid
            // 
            this.SettingsFileGrid.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SettingsFileGrid.FormattingEnabled = true;
            this.SettingsFileGrid.Location = new System.Drawing.Point(6, 35);
            this.SettingsFileGrid.Name = "SettingsFileGrid";
            this.SettingsFileGrid.Size = new System.Drawing.Size(315, 21);
            this.SettingsFileGrid.TabIndex = 2;
            // 
            // SettingsLoadFiles
            // 
            this.SettingsLoadFiles.Location = new System.Drawing.Point(340, 35);
            this.SettingsLoadFiles.Name = "SettingsLoadFiles";
            this.SettingsLoadFiles.Size = new System.Drawing.Size(75, 23);
            this.SettingsLoadFiles.TabIndex = 1;
            this.SettingsLoadFiles.Text = "Load";
            this.SettingsLoadFiles.UseVisualStyleBackColor = true;
            this.SettingsLoadFiles.Click += new System.EventHandler(this.SettingsLoadFiles_Click);
            // 
            // SettingsCreateNew
            // 
            this.SettingsCreateNew.Controls.Add(this.settingsCtlSelection);
            this.SettingsCreateNew.Controls.Add(this.SettingsCreateFileName);
            this.SettingsCreateNew.Controls.Add(this.SettingsNewFileName);
            this.SettingsCreateNew.Controls.Add(this.SettingsNewFile);
            this.SettingsCreateNew.Controls.Add(this.SettingsLblSelection);
            this.SettingsCreateNew.Location = new System.Drawing.Point(568, 13);
            this.SettingsCreateNew.Name = "SettingsCreateNew";
            this.SettingsCreateNew.Size = new System.Drawing.Size(339, 107);
            this.SettingsCreateNew.TabIndex = 0;
            this.SettingsCreateNew.TabStop = false;
            this.SettingsCreateNew.Text = "Create New";
            // 
            // settingsCtlSelection
            // 
            this.settingsCtlSelection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.settingsCtlSelection.FormattingEnabled = true;
            this.settingsCtlSelection.Location = new System.Drawing.Point(6, 35);
            this.settingsCtlSelection.Name = "settingsCtlSelection";
            this.settingsCtlSelection.Size = new System.Drawing.Size(327, 21);
            this.settingsCtlSelection.TabIndex = 1;
            // 
            // SettingsCreateFileName
            // 
            this.SettingsCreateFileName.AllowDrop = true;
            this.SettingsCreateFileName.ContextMenuStrip = this.SettingsCnNewSettings;
            this.SettingsCreateFileName.Location = new System.Drawing.Point(279, 61);
            this.SettingsCreateFileName.Name = "SettingsCreateFileName";
            this.SettingsCreateFileName.Size = new System.Drawing.Size(54, 23);
            this.SettingsCreateFileName.TabIndex = 4;
            this.SettingsCreateFileName.Text = "New";
            this.SettingsCreateFileName.UseVisualStyleBackColor = true;
            this.SettingsCreateFileName.Click += new System.EventHandler(this.SettingsCreateFileName_Click);
            // 
            // SettingsCnNewSettings
            // 
            this.SettingsCnNewSettings.AllowDrop = true;
            this.SettingsCnNewSettings.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SettingsCnCreate,
            this.SettingsCnCopyFrom});
            this.SettingsCnNewSettings.Name = "SettingsCnNewSettings";
            this.SettingsCnNewSettings.Size = new System.Drawing.Size(181, 48);
            // 
            // SettingsCnCreate
            // 
            this.SettingsCnCreate.Name = "SettingsCnCreate";
            this.SettingsCnCreate.Size = new System.Drawing.Size(180, 22);
            this.SettingsCnCreate.Text = "Create";
            this.SettingsCnCreate.Click += new System.EventHandler(this.SettingsCnCreate_Click);
            // 
            // SettingsCnCopyFrom
            // 
            this.SettingsCnCopyFrom.Name = "SettingsCnCopyFrom";
            this.SettingsCnCopyFrom.Size = new System.Drawing.Size(180, 22);
            this.SettingsCnCopyFrom.Text = "Copy From Selected";
            this.SettingsCnCopyFrom.Click += new System.EventHandler(this.SettingsCnCopyFrom_Click);
            // 
            // SettingsNewFileName
            // 
            this.SettingsNewFileName.Location = new System.Drawing.Point(60, 62);
            this.SettingsNewFileName.Name = "SettingsNewFileName";
            this.SettingsNewFileName.Size = new System.Drawing.Size(213, 20);
            this.SettingsNewFileName.TabIndex = 3;
            // 
            // SettingsNewFile
            // 
            this.SettingsNewFile.AutoSize = true;
            this.SettingsNewFile.Location = new System.Drawing.Point(4, 66);
            this.SettingsNewFile.Name = "SettingsNewFile";
            this.SettingsNewFile.Size = new System.Drawing.Size(54, 13);
            this.SettingsNewFile.TabIndex = 9;
            this.SettingsNewFile.Text = "File Name";
            // 
            // SettingsLblSelection
            // 
            this.SettingsLblSelection.AutoSize = true;
            this.SettingsLblSelection.Location = new System.Drawing.Point(9, 19);
            this.SettingsLblSelection.Name = "SettingsLblSelection";
            this.SettingsLblSelection.Size = new System.Drawing.Size(94, 13);
            this.SettingsLblSelection.TabIndex = 8;
            this.SettingsLblSelection.Text = "Select Basic Class";
            // 
            // SettingsButtons
            // 
            this.SettingsButtons.Location = new System.Drawing.Point(809, 0);
            this.SettingsButtons.Name = "SettingsButtons";
            this.SettingsButtons.Size = new System.Drawing.Size(101, 140);
            this.SettingsButtons.TabIndex = 8;
            this.SettingsButtons.TabStop = false;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.ScriptDeleteBt);
            this.tabPage4.Controls.Add(this.ScriptSaveBt);
            this.tabPage4.Controls.Add(this.groupBox2);
            this.tabPage4.Controls.Add(this.label5);
            this.tabPage4.Controls.Add(this.ScriptsCB);
            this.tabPage4.Controls.Add(this.ScriptTB);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(910, 631);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Scripts";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // ScriptDeleteBt
            // 
            this.ScriptDeleteBt.Location = new System.Drawing.Point(334, 68);
            this.ScriptDeleteBt.Name = "ScriptDeleteBt";
            this.ScriptDeleteBt.Size = new System.Drawing.Size(75, 22);
            this.ScriptDeleteBt.TabIndex = 8;
            this.ScriptDeleteBt.Text = "Delete";
            this.ScriptDeleteBt.UseVisualStyleBackColor = true;
            this.ScriptDeleteBt.Click += new System.EventHandler(this.ScriptDeleteBt_Click);
            // 
            // ScriptSaveBt
            // 
            this.ScriptSaveBt.Location = new System.Drawing.Point(334, 17);
            this.ScriptSaveBt.Name = "ScriptSaveBt";
            this.ScriptSaveBt.Size = new System.Drawing.Size(75, 36);
            this.ScriptSaveBt.TabIndex = 3;
            this.ScriptSaveBt.Text = "Save";
            this.ScriptSaveBt.UseVisualStyleBackColor = true;
            this.ScriptSaveBt.Click += new System.EventHandler(this.ScriptSaveBt_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ScriptNewTb);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.ScriptCreateBt);
            this.groupBox2.Location = new System.Drawing.Point(617, 15);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(285, 84);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "New Empty Script";
            // 
            // ScriptNewTb
            // 
            this.ScriptNewTb.Location = new System.Drawing.Point(47, 20);
            this.ScriptNewTb.Name = "ScriptNewTb";
            this.ScriptNewTb.Size = new System.Drawing.Size(232, 20);
            this.ScriptNewTb.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "Name";
            // 
            // ScriptCreateBt
            // 
            this.ScriptCreateBt.Location = new System.Drawing.Point(204, 46);
            this.ScriptCreateBt.Name = "ScriptCreateBt";
            this.ScriptCreateBt.Size = new System.Drawing.Size(75, 29);
            this.ScriptCreateBt.TabIndex = 0;
            this.ScriptCreateBt.Text = "Create";
            this.ScriptCreateBt.UseVisualStyleBackColor = true;
            this.ScriptCreateBt.Click += new System.EventHandler(this.ScriptCreateBt_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(39, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Scripts";
            // 
            // ScriptsCB
            // 
            this.ScriptsCB.FormattingEnabled = true;
            this.ScriptsCB.Location = new System.Drawing.Point(53, 17);
            this.ScriptsCB.Name = "ScriptsCB";
            this.ScriptsCB.Size = new System.Drawing.Size(275, 21);
            this.ScriptsCB.TabIndex = 2;
            this.ScriptsCB.SelectedIndexChanged += new System.EventHandler(this.ScriptsCB_SelectedIndexChanged);
            // 
            // ScriptTB
            // 
            this.ScriptTB.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ScriptTB.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.ScriptTB.Location = new System.Drawing.Point(8, 105);
            this.ScriptTB.Multiline = true;
            this.ScriptTB.Name = "ScriptTB";
            this.ScriptTB.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.ScriptTB.Size = new System.Drawing.Size(894, 518);
            this.ScriptTB.TabIndex = 1;
            this.ScriptTB.WordWrap = false;
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.ApiSettingsBt);
            this.tabPage6.Controls.Add(this.ApiSettingsGv);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Size = new System.Drawing.Size(910, 631);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "Api Settings";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // ApiSettingsBt
            // 
            this.ApiSettingsBt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ApiSettingsBt.Location = new System.Drawing.Point(827, 10);
            this.ApiSettingsBt.Name = "ApiSettingsBt";
            this.ApiSettingsBt.Size = new System.Drawing.Size(75, 38);
            this.ApiSettingsBt.TabIndex = 9;
            this.ApiSettingsBt.Text = "Save";
            this.ApiSettingsBt.UseVisualStyleBackColor = true;
            this.ApiSettingsBt.Click += new System.EventHandler(this.ApiSettingsBt_Click);
            // 
            // ApiSettingsGv
            // 
            this.ApiSettingsGv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ApiSettingsGv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ApiSettingsGv.Location = new System.Drawing.Point(8, 54);
            this.ApiSettingsGv.MultiSelect = false;
            this.ApiSettingsGv.Name = "ApiSettingsGv";
            this.ApiSettingsGv.Size = new System.Drawing.Size(894, 566);
            this.ApiSettingsGv.TabIndex = 8;
            this.ApiSettingsGv.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.ApiSettingsGv_CellValueChanged);
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.ApiUsersbt);
            this.tabPage5.Controls.Add(this.ApiUsersGv);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(910, 631);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Api Users";
            // 
            // ApiUsersbt
            // 
            this.ApiUsersbt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ApiUsersbt.Location = new System.Drawing.Point(827, 13);
            this.ApiUsersbt.Name = "ApiUsersbt";
            this.ApiUsersbt.Size = new System.Drawing.Size(75, 38);
            this.ApiUsersbt.TabIndex = 7;
            this.ApiUsersbt.Text = "Save";
            this.ApiUsersbt.UseVisualStyleBackColor = true;
            this.ApiUsersbt.Click += new System.EventHandler(this.apiUsersSavebt_Click);
            // 
            // ApiUsersGv
            // 
            this.ApiUsersGv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ApiUsersGv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ApiUsersGv.Location = new System.Drawing.Point(8, 57);
            this.ApiUsersGv.MultiSelect = false;
            this.ApiUsersGv.Name = "ApiUsersGv";
            this.ApiUsersGv.Size = new System.Drawing.Size(894, 566);
            this.ApiUsersGv.TabIndex = 6;
            this.ApiUsersGv.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.ApiUsersGv_CellEnter);
            this.ApiUsersGv.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.ApiUsersGv_CellLeave);
            this.ApiUsersGv.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.ApiUsersGv_CellValidated);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.TextClearTxt);
            this.tabPage1.Controls.Add(this.TextDecryptBT);
            this.tabPage1.Controls.Add(this.Clearlb);
            this.tabPage1.Controls.Add(this.Encryptedlb);
            this.tabPage1.Controls.Add(this.TextEncryptBT);
            this.tabPage1.Controls.Add(this.TextEncryptedTxt);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(910, 631);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Text";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // apiUsersSavebt
            // 
            this.apiUsersSavebt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.apiUsersSavebt.Location = new System.Drawing.Point(827, 13);
            this.apiUsersSavebt.Name = "apiUsersSavebt";
            this.apiUsersSavebt.Size = new System.Drawing.Size(75, 38);
            this.apiUsersSavebt.TabIndex = 7;
            this.apiUsersSavebt.Text = "Save";
            this.apiUsersSavebt.UseVisualStyleBackColor = true;
            this.apiUsersSavebt.Click += new System.EventHandler(this.apiUsersSavebt_Click);
            // 
            // label105
            // 
            this.label105.Location = new System.Drawing.Point(0, 0);
            this.label105.Name = "label105";
            this.label105.Size = new System.Drawing.Size(100, 23);
            this.label105.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(918, 657);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "HIT Scheduler Configurator";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.JobsListgv)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.SettingsPnlSelection.ResumeLayout(false);
            this.settingsGroupTop.ResumeLayout(false);
            this.SettingsUpdateFiles.ResumeLayout(false);
            this.SettingsUpdateFiles.PerformLayout();
            this.SettingsCreateNew.ResumeLayout(false);
            this.SettingsCreateNew.PerformLayout();
            this.SettingsCnNewSettings.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabPage6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ApiSettingsGv)).EndInit();
            this.tabPage5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ApiUsersGv)).EndInit();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button TextEncryptBT;
        private System.Windows.Forms.TextBox TextEncryptedTxt;
        private System.Windows.Forms.TextBox TextClearTxt;
        private System.Windows.Forms.Label Encryptedlb;
        private System.Windows.Forms.Label Clearlb;
        private System.Windows.Forms.Button TextDecryptBT;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.CheckBox JobsDachboardcb;
        private System.Windows.Forms.CheckBox JobsIsActivecb;
        protected System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox JobsPasswordtb;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox JobsUsertb;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox JobsDBtb;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox JobsServertb;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button JobsSaveBt;
        protected System.Windows.Forms.TabPage tabPage3;
        protected System.Windows.Forms.TabPage tabPage4;
        protected System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.Label Jobs;
        private System.Windows.Forms.DataGridView JobsListgv;
        private System.Windows.Forms.Button JobsReset;
        protected System.Windows.Forms.Panel SettingsPnlSelection;
        private System.Windows.Forms.Label label5;
       
        private System.Windows.Forms.Button apiUsersSavebt;
        private System.Windows.Forms.Label label105;
        private System.Windows.Forms.ComboBox ScriptsCB;
        private System.Windows.Forms.TextBox ScriptTB;
        private System.Windows.Forms.Button ScriptCreateBt;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox ScriptNewTb;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button ScriptSaveBt;
        private System.Windows.Forms.Button ScriptDeleteBt;
        private System.Windows.Forms.GroupBox SettingsButtons;
        private System.Windows.Forms.DataGridView ApiUsersGv;
        private System.Windows.Forms.Button ApiUsersbt;
        private System.Windows.Forms.Button JobsScheduleBt;
        private System.Windows.Forms.Button JobsRunJob;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.Button ApiSettingsBt;
        private System.Windows.Forms.DataGridView ApiSettingsGv;
        private System.Windows.Forms.GroupBox settingsGroupTop;
        private System.Windows.Forms.GroupBox SettingsUpdateFiles;
        private System.Windows.Forms.Button SettingsLoadFiles;
        private System.Windows.Forms.GroupBox SettingsCreateNew;
        private System.Windows.Forms.Button SettingsCreateFileName;
        private System.Windows.Forms.TextBox SettingsNewFileName;
        private System.Windows.Forms.Label SettingsNewFile;
        private System.Windows.Forms.Label SettingsLblSelection;
        private System.Windows.Forms.Button settingsBtnSaveNew;
        protected System.Windows.Forms.ComboBox settingsCtlSelection;
        private System.Windows.Forms.Label SettingsLblFileGrid;
        private System.Windows.Forms.ComboBox SettingsFileGrid;
        private System.Windows.Forms.Button SettingsDeleteFile;
        private System.Windows.Forms.TabControl SettingsTabComp;
        private System.Windows.Forms.ContextMenuStrip SettingsCnNewSettings;
        private System.Windows.Forms.ToolStripMenuItem SettingsCnCreate;
        private System.Windows.Forms.ToolStripMenuItem SettingsCnCopyFrom;
        private System.Windows.Forms.Button SettingsBtnReset;
        private System.Windows.Forms.TextBox Installationtb;
        private System.Windows.Forms.Label label7;
    }
}

