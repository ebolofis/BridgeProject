namespace Hit.Services.EncryptDecrypt
{
    partial class CronForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.MinInterval = new System.Windows.Forms.RadioButton();
            this.MinManual = new System.Windows.Forms.RadioButton();
            this.MinOdd = new System.Windows.Forms.RadioButton();
            this.MinEven = new System.Windows.Forms.RadioButton();
            this.MinEvery = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.MinList = new System.Windows.Forms.ListBox();
            this.MinNum = new System.Windows.Forms.NumericUpDown();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.HourInterval = new System.Windows.Forms.RadioButton();
            this.HourManual = new System.Windows.Forms.RadioButton();
            this.HourOdd = new System.Windows.Forms.RadioButton();
            this.HourEven = new System.Windows.Forms.RadioButton();
            this.HourEvery = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.HourList = new System.Windows.Forms.ListBox();
            this.HourNum = new System.Windows.Forms.NumericUpDown();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.DayInterval = new System.Windows.Forms.RadioButton();
            this.DayManual = new System.Windows.Forms.RadioButton();
            this.DayOdd = new System.Windows.Forms.RadioButton();
            this.DayEven = new System.Windows.Forms.RadioButton();
            this.DayEvery = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.DayList = new System.Windows.Forms.ListBox();
            this.DayNum = new System.Windows.Forms.NumericUpDown();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.MonthInterval = new System.Windows.Forms.RadioButton();
            this.MonthManual = new System.Windows.Forms.RadioButton();
            this.MonthOdd = new System.Windows.Forms.RadioButton();
            this.MonthEven = new System.Windows.Forms.RadioButton();
            this.MonthEvery = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.MonthList = new System.Windows.Forms.ListBox();
            this.MonthNum = new System.Windows.Forms.NumericUpDown();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.WeekManual = new System.Windows.Forms.RadioButton();
            this.WeekEvery = new System.Windows.Forms.RadioButton();
            this.WeekList = new System.Windows.Forms.ListBox();
            this.OccurrencesLB = new System.Windows.Forms.ListBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.Cancelbt = new System.Windows.Forms.Button();
            this.Returnbt = new System.Windows.Forms.Button();
            this.CronExpression = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MinNum)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.HourNum)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DayNum)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MonthNum)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.MinInterval);
            this.groupBox1.Controls.Add(this.MinManual);
            this.groupBox1.Controls.Add(this.MinOdd);
            this.groupBox1.Controls.Add(this.MinEven);
            this.groupBox1.Controls.Add(this.MinEvery);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.MinList);
            this.groupBox1.Controls.Add(this.MinNum);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(239, 199);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Minutes";
            // 
            // MinInterval
            // 
            this.MinInterval.AutoSize = true;
            this.MinInterval.Location = new System.Drawing.Point(9, 97);
            this.MinInterval.Name = "MinInterval";
            this.MinInterval.Size = new System.Drawing.Size(52, 17);
            this.MinInterval.TabIndex = 8;
            this.MinInterval.Text = "Every";
            this.MinInterval.UseVisualStyleBackColor = true;
            this.MinInterval.CheckedChanged += new System.EventHandler(this.IntervalCntrl_CheckedChanged);
            // 
            // MinManual
            // 
            this.MinManual.AutoSize = true;
            this.MinManual.Location = new System.Drawing.Point(133, 29);
            this.MinManual.Name = "MinManual";
            this.MinManual.Size = new System.Drawing.Size(91, 17);
            this.MinManual.TabIndex = 7;
            this.MinManual.Text = "Manual select";
            this.MinManual.UseVisualStyleBackColor = true;
            this.MinManual.CheckedChanged += new System.EventHandler(this.ListCtrl_CheckedChanged);
            // 
            // MinOdd
            // 
            this.MinOdd.AutoSize = true;
            this.MinOdd.Location = new System.Drawing.Point(9, 75);
            this.MinOdd.Name = "MinOdd";
            this.MinOdd.Size = new System.Drawing.Size(85, 17);
            this.MinOdd.TabIndex = 6;
            this.MinOdd.Text = "Odd Minutes";
            this.MinOdd.UseVisualStyleBackColor = true;
            this.MinOdd.CheckedChanged += new System.EventHandler(this.EveryCntrl_CheckedChanged);
            // 
            // MinEven
            // 
            this.MinEven.AutoSize = true;
            this.MinEven.Location = new System.Drawing.Point(9, 52);
            this.MinEven.Name = "MinEven";
            this.MinEven.Size = new System.Drawing.Size(90, 17);
            this.MinEven.TabIndex = 5;
            this.MinEven.Text = "Even Minutes";
            this.MinEven.UseVisualStyleBackColor = true;
            this.MinEven.CheckedChanged += new System.EventHandler(this.EveryCntrl_CheckedChanged);
            // 
            // MinEvery
            // 
            this.MinEvery.AutoSize = true;
            this.MinEvery.Checked = true;
            this.MinEvery.Location = new System.Drawing.Point(9, 29);
            this.MinEvery.Name = "MinEvery";
            this.MinEvery.Size = new System.Drawing.Size(71, 17);
            this.MinEvery.TabIndex = 4;
            this.MinEvery.TabStop = true;
            this.MinEvery.Text = "Every min";
            this.MinEvery.UseVisualStyleBackColor = true;
            this.MinEvery.CheckedChanged += new System.EventHandler(this.EveryCntrl_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(103, 101);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "mins";
            // 
            // MinList
            // 
            this.MinList.Enabled = false;
            this.MinList.FormattingEnabled = true;
            this.MinList.Location = new System.Drawing.Point(140, 52);
            this.MinList.Name = "MinList";
            this.MinList.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.MinList.Size = new System.Drawing.Size(78, 134);
            this.MinList.TabIndex = 1;
            this.MinList.SelectedIndexChanged += new System.EventHandler(this.ListBox_SelectedIndexChanged);
            // 
            // MinNum
            // 
            this.MinNum.Enabled = false;
            this.MinNum.Location = new System.Drawing.Point(63, 97);
            this.MinNum.Maximum = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.MinNum.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.MinNum.Name = "MinNum";
            this.MinNum.Size = new System.Drawing.Size(38, 20);
            this.MinNum.TabIndex = 0;
            this.MinNum.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.MinNum.ValueChanged += new System.EventHandler(this.UpDown_ValueChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.HourInterval);
            this.groupBox2.Controls.Add(this.HourManual);
            this.groupBox2.Controls.Add(this.HourOdd);
            this.groupBox2.Controls.Add(this.HourEven);
            this.groupBox2.Controls.Add(this.HourEvery);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.HourList);
            this.groupBox2.Controls.Add(this.HourNum);
            this.groupBox2.Location = new System.Drawing.Point(289, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(248, 199);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Hours";
            // 
            // HourInterval
            // 
            this.HourInterval.AutoSize = true;
            this.HourInterval.Location = new System.Drawing.Point(9, 97);
            this.HourInterval.Name = "HourInterval";
            this.HourInterval.Size = new System.Drawing.Size(52, 17);
            this.HourInterval.TabIndex = 8;
            this.HourInterval.Text = "Every";
            this.HourInterval.UseVisualStyleBackColor = true;
            this.HourInterval.CheckedChanged += new System.EventHandler(this.IntervalCntrl_CheckedChanged);
            // 
            // HourManual
            // 
            this.HourManual.AutoSize = true;
            this.HourManual.Location = new System.Drawing.Point(137, 29);
            this.HourManual.Name = "HourManual";
            this.HourManual.Size = new System.Drawing.Size(91, 17);
            this.HourManual.TabIndex = 7;
            this.HourManual.Text = "Manual select";
            this.HourManual.UseVisualStyleBackColor = true;
            this.HourManual.CheckedChanged += new System.EventHandler(this.ListCtrl_CheckedChanged);
            // 
            // HourOdd
            // 
            this.HourOdd.AutoSize = true;
            this.HourOdd.Location = new System.Drawing.Point(9, 75);
            this.HourOdd.Name = "HourOdd";
            this.HourOdd.Size = new System.Drawing.Size(76, 17);
            this.HourOdd.TabIndex = 6;
            this.HourOdd.Text = "Odd Hours";
            this.HourOdd.UseVisualStyleBackColor = true;
            this.HourOdd.CheckedChanged += new System.EventHandler(this.EveryCntrl_CheckedChanged);
            // 
            // HourEven
            // 
            this.HourEven.AutoSize = true;
            this.HourEven.Location = new System.Drawing.Point(9, 52);
            this.HourEven.Name = "HourEven";
            this.HourEven.Size = new System.Drawing.Size(81, 17);
            this.HourEven.TabIndex = 5;
            this.HourEven.Text = "Even Hours";
            this.HourEven.UseVisualStyleBackColor = true;
            this.HourEven.CheckedChanged += new System.EventHandler(this.EveryCntrl_CheckedChanged);
            // 
            // HourEvery
            // 
            this.HourEvery.AutoSize = true;
            this.HourEvery.Checked = true;
            this.HourEvery.Location = new System.Drawing.Point(9, 29);
            this.HourEvery.Name = "HourEvery";
            this.HourEvery.Size = new System.Drawing.Size(78, 17);
            this.HourEvery.TabIndex = 4;
            this.HourEvery.TabStop = true;
            this.HourEvery.Text = "Every Hour";
            this.HourEvery.UseVisualStyleBackColor = true;
            this.HourEvery.CheckedChanged += new System.EventHandler(this.EveryCntrl_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(101, 101);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "hours";
            // 
            // HourList
            // 
            this.HourList.Enabled = false;
            this.HourList.FormattingEnabled = true;
            this.HourList.Location = new System.Drawing.Point(138, 52);
            this.HourList.Name = "HourList";
            this.HourList.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.HourList.Size = new System.Drawing.Size(84, 134);
            this.HourList.TabIndex = 1;
            this.HourList.SelectedIndexChanged += new System.EventHandler(this.ListBox_SelectedIndexChanged);
            // 
            // HourNum
            // 
            this.HourNum.Enabled = false;
            this.HourNum.Location = new System.Drawing.Point(63, 97);
            this.HourNum.Maximum = new decimal(new int[] {
            24,
            0,
            0,
            0});
            this.HourNum.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.HourNum.Name = "HourNum";
            this.HourNum.Size = new System.Drawing.Size(38, 20);
            this.HourNum.TabIndex = 0;
            this.HourNum.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.HourNum.ValueChanged += new System.EventHandler(this.UpDown_ValueChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.DayInterval);
            this.groupBox3.Controls.Add(this.DayManual);
            this.groupBox3.Controls.Add(this.DayOdd);
            this.groupBox3.Controls.Add(this.DayEven);
            this.groupBox3.Controls.Add(this.DayEvery);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.DayList);
            this.groupBox3.Controls.Add(this.DayNum);
            this.groupBox3.Location = new System.Drawing.Point(575, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(244, 199);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Days";
            // 
            // DayInterval
            // 
            this.DayInterval.AutoSize = true;
            this.DayInterval.Location = new System.Drawing.Point(9, 97);
            this.DayInterval.Name = "DayInterval";
            this.DayInterval.Size = new System.Drawing.Size(52, 17);
            this.DayInterval.TabIndex = 8;
            this.DayInterval.Text = "Every";
            this.DayInterval.UseVisualStyleBackColor = true;
            this.DayInterval.CheckedChanged += new System.EventHandler(this.IntervalCntrl_CheckedChanged);
            // 
            // DayManual
            // 
            this.DayManual.AutoSize = true;
            this.DayManual.Location = new System.Drawing.Point(138, 29);
            this.DayManual.Name = "DayManual";
            this.DayManual.Size = new System.Drawing.Size(91, 17);
            this.DayManual.TabIndex = 7;
            this.DayManual.Text = "Manual select";
            this.DayManual.UseVisualStyleBackColor = true;
            this.DayManual.CheckedChanged += new System.EventHandler(this.ListCtrl_CheckedChanged);
            // 
            // DayOdd
            // 
            this.DayOdd.AutoSize = true;
            this.DayOdd.Location = new System.Drawing.Point(9, 75);
            this.DayOdd.Name = "DayOdd";
            this.DayOdd.Size = new System.Drawing.Size(72, 17);
            this.DayOdd.TabIndex = 6;
            this.DayOdd.Text = "Odd Days";
            this.DayOdd.UseVisualStyleBackColor = true;
            this.DayOdd.CheckedChanged += new System.EventHandler(this.EveryCntrl_CheckedChanged);
            // 
            // DayEven
            // 
            this.DayEven.AutoSize = true;
            this.DayEven.Location = new System.Drawing.Point(9, 52);
            this.DayEven.Name = "DayEven";
            this.DayEven.Size = new System.Drawing.Size(77, 17);
            this.DayEven.TabIndex = 5;
            this.DayEven.Text = "Even Days";
            this.DayEven.UseVisualStyleBackColor = true;
            this.DayEven.CheckedChanged += new System.EventHandler(this.EveryCntrl_CheckedChanged);
            // 
            // DayEvery
            // 
            this.DayEvery.AutoSize = true;
            this.DayEvery.Checked = true;
            this.DayEvery.Location = new System.Drawing.Point(9, 29);
            this.DayEvery.Name = "DayEvery";
            this.DayEvery.Size = new System.Drawing.Size(74, 17);
            this.DayEvery.TabIndex = 4;
            this.DayEvery.TabStop = true;
            this.DayEvery.Text = "Every Day";
            this.DayEvery.UseVisualStyleBackColor = true;
            this.DayEvery.CheckedChanged += new System.EventHandler(this.EveryCntrl_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(102, 101);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "days";
            // 
            // DayList
            // 
            this.DayList.Enabled = false;
            this.DayList.FormattingEnabled = true;
            this.DayList.Location = new System.Drawing.Point(138, 52);
            this.DayList.Name = "DayList";
            this.DayList.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.DayList.Size = new System.Drawing.Size(85, 134);
            this.DayList.TabIndex = 1;
            this.DayList.SelectedIndexChanged += new System.EventHandler(this.ListBox_SelectedIndexChanged);
            // 
            // DayNum
            // 
            this.DayNum.Enabled = false;
            this.DayNum.Location = new System.Drawing.Point(63, 97);
            this.DayNum.Maximum = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.DayNum.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.DayNum.Name = "DayNum";
            this.DayNum.Size = new System.Drawing.Size(38, 20);
            this.DayNum.TabIndex = 0;
            this.DayNum.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.DayNum.ValueChanged += new System.EventHandler(this.UpDown_ValueChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.MonthInterval);
            this.groupBox4.Controls.Add(this.MonthManual);
            this.groupBox4.Controls.Add(this.MonthOdd);
            this.groupBox4.Controls.Add(this.MonthEven);
            this.groupBox4.Controls.Add(this.MonthEvery);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.MonthList);
            this.groupBox4.Controls.Add(this.MonthNum);
            this.groupBox4.Location = new System.Drawing.Point(12, 239);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(242, 199);
            this.groupBox4.TabIndex = 11;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Months";
            // 
            // MonthInterval
            // 
            this.MonthInterval.AutoSize = true;
            this.MonthInterval.Location = new System.Drawing.Point(9, 98);
            this.MonthInterval.Name = "MonthInterval";
            this.MonthInterval.Size = new System.Drawing.Size(52, 17);
            this.MonthInterval.TabIndex = 8;
            this.MonthInterval.Text = "Every";
            this.MonthInterval.UseVisualStyleBackColor = true;
            this.MonthInterval.CheckedChanged += new System.EventHandler(this.IntervalCntrl_CheckedChanged);
            // 
            // MonthManual
            // 
            this.MonthManual.AutoSize = true;
            this.MonthManual.Location = new System.Drawing.Point(139, 29);
            this.MonthManual.Name = "MonthManual";
            this.MonthManual.Size = new System.Drawing.Size(91, 17);
            this.MonthManual.TabIndex = 7;
            this.MonthManual.Text = "Manual select";
            this.MonthManual.UseVisualStyleBackColor = true;
            this.MonthManual.CheckedChanged += new System.EventHandler(this.ListCtrl_CheckedChanged);
            // 
            // MonthOdd
            // 
            this.MonthOdd.AutoSize = true;
            this.MonthOdd.Location = new System.Drawing.Point(9, 75);
            this.MonthOdd.Name = "MonthOdd";
            this.MonthOdd.Size = new System.Drawing.Size(83, 17);
            this.MonthOdd.TabIndex = 6;
            this.MonthOdd.Text = "Odd Months";
            this.MonthOdd.UseVisualStyleBackColor = true;
            this.MonthOdd.CheckedChanged += new System.EventHandler(this.EveryCntrl_CheckedChanged);
            // 
            // MonthEven
            // 
            this.MonthEven.AutoSize = true;
            this.MonthEven.Location = new System.Drawing.Point(9, 52);
            this.MonthEven.Name = "MonthEven";
            this.MonthEven.Size = new System.Drawing.Size(88, 17);
            this.MonthEven.TabIndex = 5;
            this.MonthEven.Text = "Even Months";
            this.MonthEven.UseVisualStyleBackColor = true;
            this.MonthEven.CheckedChanged += new System.EventHandler(this.EveryCntrl_CheckedChanged);
            // 
            // MonthEvery
            // 
            this.MonthEvery.AutoSize = true;
            this.MonthEvery.Checked = true;
            this.MonthEvery.Location = new System.Drawing.Point(9, 29);
            this.MonthEvery.Name = "MonthEvery";
            this.MonthEvery.Size = new System.Drawing.Size(85, 17);
            this.MonthEvery.TabIndex = 4;
            this.MonthEvery.TabStop = true;
            this.MonthEvery.Text = "Every Month";
            this.MonthEvery.UseVisualStyleBackColor = true;
            this.MonthEvery.CheckedChanged += new System.EventHandler(this.EveryCntrl_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(99, 101);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(36, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "month";
            // 
            // MonthList
            // 
            this.MonthList.Enabled = false;
            this.MonthList.FormattingEnabled = true;
            this.MonthList.Location = new System.Drawing.Point(139, 52);
            this.MonthList.Name = "MonthList";
            this.MonthList.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.MonthList.Size = new System.Drawing.Size(86, 134);
            this.MonthList.TabIndex = 1;
            this.MonthList.SelectedIndexChanged += new System.EventHandler(this.ListBox_SelectedIndexChanged);
            // 
            // MonthNum
            // 
            this.MonthNum.Enabled = false;
            this.MonthNum.Location = new System.Drawing.Point(61, 97);
            this.MonthNum.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.MonthNum.Name = "MonthNum";
            this.MonthNum.Size = new System.Drawing.Size(38, 20);
            this.MonthNum.TabIndex = 0;
            this.MonthNum.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.MonthNum.ValueChanged += new System.EventHandler(this.UpDown_ValueChanged);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.WeekManual);
            this.groupBox5.Controls.Add(this.WeekEvery);
            this.groupBox5.Controls.Add(this.WeekList);
            this.groupBox5.Location = new System.Drawing.Point(289, 239);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(248, 199);
            this.groupBox5.TabIndex = 12;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Weekdays";
            // 
            // WeekManual
            // 
            this.WeekManual.AutoSize = true;
            this.WeekManual.Location = new System.Drawing.Point(139, 29);
            this.WeekManual.Name = "WeekManual";
            this.WeekManual.Size = new System.Drawing.Size(91, 17);
            this.WeekManual.TabIndex = 7;
            this.WeekManual.Text = "Manual select";
            this.WeekManual.UseVisualStyleBackColor = true;
            this.WeekManual.CheckedChanged += new System.EventHandler(this.ListCtrl_CheckedChanged);
            // 
            // WeekEvery
            // 
            this.WeekEvery.AutoSize = true;
            this.WeekEvery.Checked = true;
            this.WeekEvery.Location = new System.Drawing.Point(9, 29);
            this.WeekEvery.Name = "WeekEvery";
            this.WeekEvery.Size = new System.Drawing.Size(101, 17);
            this.WeekEvery.TabIndex = 4;
            this.WeekEvery.TabStop = true;
            this.WeekEvery.Text = "Every Weekday";
            this.WeekEvery.UseVisualStyleBackColor = true;
            this.WeekEvery.CheckedChanged += new System.EventHandler(this.EveryCntrl_CheckedChanged);
            // 
            // WeekList
            // 
            this.WeekList.Enabled = false;
            this.WeekList.FormattingEnabled = true;
            this.WeekList.Location = new System.Drawing.Point(143, 52);
            this.WeekList.Name = "WeekList";
            this.WeekList.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.WeekList.Size = new System.Drawing.Size(80, 134);
            this.WeekList.TabIndex = 1;
            this.WeekList.SelectedIndexChanged += new System.EventHandler(this.ListBox_SelectedIndexChanged);
            // 
            // OccurrencesLB
            // 
            this.OccurrencesLB.BackColor = System.Drawing.SystemColors.Control;
            this.OccurrencesLB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.OccurrencesLB.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.OccurrencesLB.ItemHeight = 15;
            this.OccurrencesLB.Location = new System.Drawing.Point(6, 20);
            this.OccurrencesLB.Name = "OccurrencesLB";
            this.OccurrencesLB.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.OccurrencesLB.Size = new System.Drawing.Size(239, 165);
            this.OccurrencesLB.TabIndex = 16;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.OccurrencesLB);
            this.groupBox6.Location = new System.Drawing.Point(560, 239);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(255, 199);
            this.groupBox6.TabIndex = 16;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Occurrences";
            // 
            // Cancelbt
            // 
            this.Cancelbt.Location = new System.Drawing.Point(740, 444);
            this.Cancelbt.Name = "Cancelbt";
            this.Cancelbt.Size = new System.Drawing.Size(75, 23);
            this.Cancelbt.TabIndex = 18;
            this.Cancelbt.Text = "Cancel";
            this.Cancelbt.UseVisualStyleBackColor = true;
            this.Cancelbt.Click += new System.EventHandler(this.Cancelbt_Click);
            // 
            // Returnbt
            // 
            this.Returnbt.Location = new System.Drawing.Point(740, 474);
            this.Returnbt.Name = "Returnbt";
            this.Returnbt.Size = new System.Drawing.Size(75, 35);
            this.Returnbt.TabIndex = 19;
            this.Returnbt.Text = "Return";
            this.Returnbt.UseVisualStyleBackColor = true;
            this.Returnbt.Click += new System.EventHandler(this.Returnbt_Click);
            // 
            // CronExpression
            // 
            this.CronExpression.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.CronExpression.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.CronExpression.ForeColor = System.Drawing.Color.Maroon;
            this.CronExpression.Location = new System.Drawing.Point(11, 444);
            this.CronExpression.Name = "CronExpression";
            this.CronExpression.Size = new System.Drawing.Size(723, 68);
            this.CronExpression.TabIndex = 20;
            this.CronExpression.Text = "                                 ";
            // 
            // CronForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(827, 521);
            this.Controls.Add(this.CronExpression);
            this.Controls.Add(this.Returnbt);
            this.Controls.Add(this.Cancelbt);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "CronForm";
            this.ShowIcon = false;
            this.Text = "Schedule Job";
            this.Load += new System.EventHandler(this.CronForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MinNum)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.HourNum)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DayNum)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MonthNum)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton MinInterval;
        private System.Windows.Forms.RadioButton MinManual;
        private System.Windows.Forms.RadioButton MinOdd;
        private System.Windows.Forms.RadioButton MinEven;
        private System.Windows.Forms.RadioButton MinEvery;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox MinList;
        private System.Windows.Forms.NumericUpDown MinNum;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton HourInterval;
        private System.Windows.Forms.RadioButton HourManual;
        private System.Windows.Forms.RadioButton HourOdd;
        private System.Windows.Forms.RadioButton HourEven;
        private System.Windows.Forms.RadioButton HourEvery;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox HourList;
        private System.Windows.Forms.NumericUpDown HourNum;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton DayInterval;
        private System.Windows.Forms.RadioButton DayManual;
        private System.Windows.Forms.RadioButton DayOdd;
        private System.Windows.Forms.RadioButton DayEven;
        private System.Windows.Forms.RadioButton DayEvery;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox DayList;
        private System.Windows.Forms.NumericUpDown DayNum;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton MonthInterval;
        private System.Windows.Forms.RadioButton MonthManual;
        private System.Windows.Forms.RadioButton MonthOdd;
        private System.Windows.Forms.RadioButton MonthEven;
        private System.Windows.Forms.RadioButton MonthEvery;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox MonthList;
        private System.Windows.Forms.NumericUpDown MonthNum;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.RadioButton WeekManual;
        private System.Windows.Forms.RadioButton WeekEvery;
        private System.Windows.Forms.ListBox WeekList;
        private System.Windows.Forms.ListBox OccurrencesLB;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button Cancelbt;
        private System.Windows.Forms.Button Returnbt;
        private System.Windows.Forms.Label CronExpression;
    }
}