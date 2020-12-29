using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NCrontab;
using CronExpressionDescriptor;

namespace Hit.Services.EncryptDecrypt
{
    public partial class CronForm : Form
    {
        public CronForm()
        {
            InitializeComponent();
        }

        public CronForm(string Id, string cron)
        {
            InitializeComponent();

            this.Id = Id;
            if (cron == null || cron == "") cronInit = "* * * * *";

            basicText = "Schedule Job '" + Id+"' : " ;
            this.cronInit = cron;
           
        }

        private List<string> expresion= new List<string>() { "*", "*" , "*" , "*" , "*" };
        string Id, basicText,cron,cronInit, cronDescr;
        bool init = true;

        private void CronForm_Load(object sender, EventArgs e)
        {
            FillLists();
            cron = cronInit;
            expresion = cronInit.Split(' ').ToList();
            applyExpresion();
            init = false;
            ParseCron();
        }



        #region "Init"
        private void FillLists()
        {
            //mins
            List<CustomListItem> mins = new List<CustomListItem>();
            for (int i = 0; i <= 59; i++)
            {
                mins.Add(new CustomListItem() { Text = i.ToString().PadLeft(2, '0'), Value = i.ToString() });
            }
            MinList.DataSource = mins;
            MinList.DisplayMember = "Text";
            MinList.ValueMember = "Value";
            MinList.SelectedIndex = -1;

            //hours
            List<CustomListItem> hours = new List<CustomListItem>();
            for (int i = 0; i <= 23; i++)
            {
                hours.Add(new CustomListItem() { Text = i.ToString().PadLeft(2, '0'), Value = i.ToString() });
            }
            HourList.DataSource = hours;
            HourList.DisplayMember = "Text";
            HourList.ValueMember = "Value";
            HourList.SelectedIndex = -1;

            //Month's days
            List<CustomListItem> days = new List<CustomListItem>();
            for (int i = 1; i <= 31; i++)
            {
                days.Add(new CustomListItem() { Text=i.ToString().PadLeft(2, '0'), Value=i.ToString() });
            }
            DayList.DataSource = days;
            DayList.DisplayMember = "Text";
            DayList.ValueMember = "Value";
            DayList.SelectedIndex = -1;


            //week days
            var americanCulture = new CultureInfo("en-US");
            var weekdayList = americanCulture.DateTimeFormat.DayNames.Take(7).Select((item, index) => new CustomListItem
            {
                Text = item,
                Value = index.ToString()
            });
            WeekList.DataSource = weekdayList.ToList();
            WeekList.DisplayMember = "Text";
            WeekList.ValueMember = "Value";
            WeekList.SelectedIndex = -1;

            //months 
            var monthList = americanCulture.DateTimeFormat.MonthNames.Take(12).Select((item, index) => new CustomListItem
            {
                Text = item,
                Value = Convert.ToString(index + 1)
            });
            MonthList.DataSource = monthList.ToList();
            MonthList.DisplayMember = "Text";
            MonthList.ValueMember = "Value";
            MonthList.SelectedIndex = -1;

        }

       

        /// <summary>
        /// When an '*Every' RadioButton is checked then disable the respective '*List' and '*Num' control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EveryCntrl_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton c = sender as RadioButton;
            if (c.Checked)
            {
                string section = c.Name.Replace("Every", "").Replace("Odd", "").Replace("Even", "");//get Min or Hour or Hour or Month or Week
                ListBox listbox = this.Controls.Find(section + "List", true).FirstOrDefault() as ListBox;
                if (listbox != null) listbox.Enabled = !c.Checked;
                NumericUpDown numUpDown = this.Controls.Find(section + "Num", true).FirstOrDefault() as NumericUpDown;
                if (numUpDown != null) numUpDown.Enabled = !c.Checked;
            }
           if(!init) ParseCron();

        }

        /// <summary>
        /// When an '*Manual' RadioButton is checked then anable the respective '*List' and disable the respective '*Num' control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListCtrl_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton c = sender as RadioButton;
            if (c.Checked)
            {
                string section = c.Name.Replace("Manual", "");//get Min or Hour or Hour or Month or Week
                ListBox listbox = this.Controls.Find(section + "List", true).FirstOrDefault() as ListBox;
                if (listbox != null) listbox.Enabled = true;
                NumericUpDown numUpDown = this.Controls.Find(section + "Num", true).FirstOrDefault() as NumericUpDown;
                if (numUpDown != null) numUpDown.Enabled = false;
            }
            if (!init) ParseCron();
        }

        private void IntervalCntrl_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton c = sender as RadioButton;
           
                string section = c.Name.Replace("Interval", "");//get Min or Hour or Hour or Month or Week
                ListBox listbox = this.Controls.Find(section + "List", true).FirstOrDefault() as ListBox;
                if (listbox != null) listbox.Enabled = !c.Checked;
                NumericUpDown numUpDown = this.Controls.Find(section + "Num", true).FirstOrDefault() as NumericUpDown;
                if (numUpDown != null) numUpDown.Enabled = c.Checked;
            if (!init) ParseCron();
        }


        private void ListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!init) ParseCron();
        }

        private void UpDown_ValueChanged(object sender, EventArgs e)
        {
            if (!init) ParseCron();
        }

        #endregion


        /// <summary>
        /// Apply a cron expression to form's controls
        /// </summary>
        private void applyExpresion()
        {
            setExpresion("Min", 0);
            setExpresion("Hour", 1);
            setExpresion("Day", 2);
            setExpresion("Month", 3);
            setExpresion("Week", 4);
        }

        private void setExpresion(string cntrlPrefix, int listIndex)
        {
            RadioButton rb=null;
            if (expresion[listIndex] == "*")
                rb = this.Controls.Find(cntrlPrefix + "Every", true).FirstOrDefault() as RadioButton;  

            else if (expresion[listIndex] =="*/2")
                rb = this.Controls.Find(cntrlPrefix + "Even", true).FirstOrDefault() as RadioButton;

            else if (expresion[listIndex] == "1-59/2" || expresion[listIndex] == "1-23/2" || expresion[listIndex] == "1-31/2" || expresion[listIndex] == "1-11/2")
                rb = this.Controls.Find(cntrlPrefix + "Odd", true).FirstOrDefault() as RadioButton;

            else if (expresion[listIndex].StartsWith("*/"))
            {
                rb = this.Controls.Find(cntrlPrefix + "Interval", true).FirstOrDefault() as RadioButton;
                NumericUpDown lb = this.Controls.Find(cntrlPrefix + "Num", true).FirstOrDefault() as NumericUpDown;
                lb.Value = Convert.ToDecimal( expresion[listIndex].Replace("*/", ""));
            }

            else
            {
                rb = this.Controls.Find(cntrlPrefix + "Manual", true).FirstOrDefault() as RadioButton;
                List<string> list = expresion[listIndex].Split(',').ToList();
                ListBox lb = this.Controls.Find(cntrlPrefix + "List", true).FirstOrDefault() as ListBox;
                foreach(var item in list)
                {
                    int index = lb.FindString(item.PadLeft(2, '0'));
                    if (index == -1 && listIndex == 3 ) index =Convert.ToInt32( item)-1;
                    if (index == -1 && listIndex == 4) index = Convert.ToInt32(item) ;
                    // Determine if a valid index is returned. Select the item if it is valid.
                    if (index != -1) lb.SetSelected(index, true);
                }
            }
                


            rb.Checked = true;
        }


        /// <summary>
        /// create cron expresion
        /// </summary>
        private void ConstructExpresion()
        {
            expresion[0] = createExpresion("Min", 0,59);
            expresion[1] = createExpresion("Hour", 1,23);
            expresion[2] = createExpresion("Day", 2,31);
            expresion[3] = createExpresion("Month", 3,11);
            expresion[4] = createExpresion("Week", 4,0);
        }

        private void Returnbt_Click(object sender, EventArgs e)
        {
            CronSchedule.Cron = cron;
            CronSchedule.Description = cronDescr;
            this.Close();
        }

        private void Cancelbt_Click(object sender, EventArgs e)
        {
            CronSchedule.Cron = null;
            CronSchedule.Description = null;
            this.Close();
        }

        /// <summary>
        /// create the expresion for one part of cron expresion
        /// </summary>
        /// <param name="cntrlPrefix"></param>
        /// <param name="listIndex"></param>
        /// <param name="maxListVal"></param>
        /// <returns></returns>
        private string createExpresion(string cntrlPrefix,int listIndex, int maxListVal)
        {
            //Every min, hour,...
            RadioButton rb = this.Controls.Find(cntrlPrefix + "Every", true).FirstOrDefault() as RadioButton;
            if (rb.Checked) return "*";

            //Even min, hour,...
            rb = this.Controls.Find(cntrlPrefix + "Even", true).FirstOrDefault() as RadioButton;
            if (rb != null && rb.Checked)
            {
                if (listIndex != 2 && listIndex != 3)
                    return "*/2";
                else
                    return "2-" + maxListVal.ToString() + "/2";

            }

            //Odd min, hour,...
            rb = this.Controls.Find(cntrlPrefix + "Odd", true).FirstOrDefault() as RadioButton;
            if (rb != null && rb.Checked) return "1-" + maxListVal.ToString() + "/2";

            //Odd min, hour,...
            rb = this.Controls.Find(cntrlPrefix + "Interval", true).FirstOrDefault() as RadioButton;
            if (rb != null && rb.Checked)
            {
                NumericUpDown lb = this.Controls.Find(cntrlPrefix + "Num", true).FirstOrDefault() as NumericUpDown;
                return "*/"+lb.Value.ToString();
            }

            //Manual selected mins, hours,...
            rb = this.Controls.Find(cntrlPrefix + "Manual", true).FirstOrDefault() as RadioButton;
            if (rb != null && rb.Checked)
            {
                string str = "";
                ListBox lb = this.Controls.Find(cntrlPrefix + "List", true).FirstOrDefault() as ListBox;
                foreach(var item in lb.SelectedItems)
                {
                    CustomListItem citm = (CustomListItem)item;
                    if (str != "") str = str + ",";
                    str = str +Convert.ToInt32(citm.Value).ToString();
                }
                if (str == "") str = "*";
               
                return str;
            }

            return "*";
        }

        /// <summary>
        /// Create cron expression and show occurrences and description
        /// </summary>
        private void ParseCron()
        {
            ConstructExpresion();
            cron = string.Join(" ", expresion.ToArray());
            this.Text = basicText+cron;
            var s = CrontabSchedule.Parse(cron);
            var start = DateTime.Now;
            var end = start.AddMonths(25);
            OccurrencesLB.DataSource = null;
            OccurrencesLB.Items.Clear();
            var occurrences = s.GetNextOccurrences(start, end).Take(500).Select(x => x.ToString("  ddd, dd MMM yyyy  HH:mm")).ToList();
            foreach(string item in occurrences)
            {
                OccurrencesLB.Items.Add(item);
            }

            cronDescr= CronExpressionDescriptor.ExpressionDescriptor.GetDescription(cron);
            CronExpression.Text = cronDescr;
        }

      
    }


    public class CustomListItem
    {
      public  string Text { get; set; }
      public string Value { get; set; }
    }

    public static class CronSchedule
    {
        public static string Cron { get; set; }
        public static string Description { get; set; }
    }
}
