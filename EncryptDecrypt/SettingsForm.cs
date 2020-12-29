using EncryptDecrypt;
using Hit.Scheduler.Jobs;
using Hit.Services.Core;
using Hit.Services.Helpers.Classes.Classes;
using Hit.Services.Models;
using Hit.Services.Models.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EncryptDecrypt
{
    public partial class Form1 : Form
    {
        public void initSettings()
        {
            configHLP = new ConfigHelper();
            settingsCtlSelection.Items.Clear();
            SettingsFileGrid.Items.Clear();
            FillSettingsCombobox();
            LoadSettingsFilesForInheritedClass();
            GetGroupsAndFieldsNameFromSettingsModel();
        }

        /// <summary>
        /// ActionMode (0 => Create New, 1 => Update One)
        /// </summary>
        private int? ActionMode;

        /// <summary>
        /// Tabs to create
        /// </summary>
        //private List<AnnotationsNameModel> SettingsGroups = new List<AnnotationsNameModel>();
        private List<AnnotationsNameModel> TabsForComponent = new List<AnnotationsNameModel>();

        /// <summary>
        /// Components to create
        /// </summary>
        private List<ComponentTypesFromNamesModel> Components = new List<ComponentTypesFromNamesModel>();

        /// <summary>
        /// Settings Model from Inifile
        /// </summary>
        private SettingsModel FullSettignsModel;

        public ExtAssemblyModel ExtModel = new ExtAssemblyModel();

        /// <summary>
        /// Returns a Settings Model From Ini File
        /// </summary>
        /// <param name="IniFileName"></param>
        /// <returns></returns>
        private SettingsModel ReadModelFromIniFile(string IniFileName)
        {
            try
            {
                return configHLP.ReadSettingsFile(IniFileName, encrStatus.SettingsFilesAreEncrypted);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not read " + IniFileName + " : " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                log.Error("ReadModelFromIniFile \r\n " + ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// List Of Components from Ini file to Create 
        /// </summary>
        private List<ComponentTypesFromNamesModel> IniFileComponents;

        /// <summary>
        /// Returns Controllers name
        /// </summary>
        /// <returns></returns>
        private ExtAssemblyModel GetControllersName()
        {
            ExtAssemblyModel Model = new ExtAssemblyModel();
            FileHelpers fh = new FileHelpers();
            string path = fh.GetHitServicesWAPath();
            try
            {
                Assembly asm = Assembly.LoadFrom(path);
                Model.ClassNames = (from t in asm.GetExportedTypes()
                                    where !t.IsInterface && !t.IsAbstract
                                    where t.IsSubclassOf(asm.GetType("Hit.WebApi.Controllers.HitController")) //&& t.FullName == job.ClassName
                                                                                                              //where typeof(IScheduledJob).IsAssignableFrom(t) && t.FullName == job.ClassName
                                    select t.FullName).ToList();
                Model.ClassNames.Sort();
                Model.Assembly = asm;
                Model.Type = typeof(ScheduledJob);
                return Model;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Reading available Controllers", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                log.Error(ex.ToString());
            }
            return new ExtAssemblyModel();
        }

        /// <summary>
        /// Returns External Names
        /// </summary>
        /// <returns></returns>
        private ExtAssemblyModel GetExternalNames()
        {
            try
            {
                ExtAssemblyModel Model = new ExtAssemblyModel();
                FileHelpers fh = new FileHelpers();
                List<string> path = fh.GetExternalJobsPaths();

                string ipath = fh.GetExtPath();
                Assembly extAsm = Assembly.LoadFrom(ipath);
                Type iType = extAsm.GetType("Hit.Scheduler.Jobs.ScheduledJob");

                foreach (string p in path)
                {
                    Assembly asm = Assembly.LoadFile(p);
                    List<string> LoadTypes = new List<string>();

                    LoadTypes = (from t in asm.GetExportedTypes()
                                 where !t.IsInterface && !t.IsAbstract
                                 where t.IsSubclassOf(iType)
                                 select t.FullName).ToList();

                    if (LoadTypes.Count > 0)
                    {
                        Model.ClassNames = LoadTypes;
                        Model.Assembly = asm;
                        Model.Type = iType;
                    }
                }
                Model.ClassNames.Sort();
                return Model;
            }
            catch (Exception ex)
            {
                log.Error(ex.ToString());
                MessageBox.Show(ex.Message, "Error Reading available jobs");
            }
            return new ExtAssemblyModel();
        }

        /// <summary>
        /// Returns External Assemblies
        /// </summary>
        /// <returns></returns>
        private Assembly GetExtAssembly(string className)
        {
            try
            {
                FileHelpers fh = new FileHelpers();
                List<string> path = fh.GetExternalJobsPaths();

                string ipath = fh.GetExtPath();
                Assembly extAsm = Assembly.LoadFrom(ipath);
                Type iType = extAsm.GetType("Hit.Scheduler.Jobs.ScheduledJob");

                foreach (string p in path)
                {
                    Assembly asm = Assembly.LoadFrom(p);
                    List<string> LoadTypes = (from t in asm.GetExportedTypes()
                                              where !t.IsInterface && !t.IsAbstract
                                              where t.IsSubclassOf(iType)
                                              select t.FullName).ToList();
                    if (LoadTypes.Count > 0)
                    {
                        foreach (string c in LoadTypes)
                        {
                            if (c == className)
                            {
                                return asm;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.ToString());
                MessageBox.Show(ex.Message, "Error Reading available Assemblies");
            }
            return null;
        }

        /// <summary>
        /// collects the lists used as datasource for Array components
        /// </summary>
        private List<List<SettingsStringListSourceModel>> ArrysList = new List<List<SettingsStringListSourceModel>>();

        /// <summary>
        /// Collects the lists used as datasource for Dictionary Components
        /// </summary>
        private List<List<SettingsDictionaryModel>> DictionList = new List<List<SettingsDictionaryModel>>();

        /// <summary>
        /// Fills Select Basic class combo Box
        /// </summary>
        public void FillSettingsCombobox()
        {
            /*Getting Local Jobs*/
            ExtAssemblyModel localClasses = jobsConfig.GetJobs();
            foreach (string item in localClasses.ClassNames)
            {
                JobsAssemblyModel model = new JobsAssemblyModel();
                settingsCtlSelection.Items.Add(item);
                model.JobsOrigin = 1;
                model.ClassName = item;
                model.Assembly = localClasses.Assembly;
                model.Type = localClasses.Type;
                ScheduledJob.JobsOriginList.Add(model);
            }

            /*Getting Controllers Jobs*/
            ExtAssemblyModel LoadTypes = GetControllersName();
            if (LoadTypes.Assembly != null)
            {
                foreach (string item in LoadTypes.ClassNames)
                {
                    JobsAssemblyModel model = new JobsAssemblyModel();
                    settingsCtlSelection.Items.Add(item);
                    model.JobsOrigin = 3;
                    model.ClassName = item;
                    model.Assembly = LoadTypes.Assembly;
                    model.Type = LoadTypes.Type;
                    ScheduledJob.JobsOriginList.Add(model);
                }
            }

            /*Getting External Jobs*/
            ExtAssemblyModel LoadExtTypes = GetExternalNames();
            if (LoadExtTypes.Assembly != null)
            {
                foreach (string item in LoadExtTypes.ClassNames)
                {
                    JobsAssemblyModel model = new JobsAssemblyModel();
                    settingsCtlSelection.Items.Add(item);
                    model.JobsOrigin = 2;
                    model.ClassName = item;
                    model.Assembly = LoadExtTypes.Assembly;
                    model.Type = LoadExtTypes.Type;
                    ScheduledJob.JobsOriginList.Add(model);
                }
            }
        }

        /// <summary>
        /// Get all properties of a class
        /// </summary>
        /// <param name="type">Type object of that class</param>
        /// <returns></returns>
        public IEnumerable<PropertyInfo> GetAllProperties(Type type)
        {
            if (type == null)
            {
                return Enumerable.Empty<PropertyInfo>();
            }

            BindingFlags flags = BindingFlags.Public |
                                 BindingFlags.NonPublic |
                                 BindingFlags.Static |
                                 BindingFlags.Instance |
                                 BindingFlags.DeclaredOnly;

            return type.GetProperties(flags).Union(GetAllProperties(type.BaseType));
        }

        /// <summary>
        /// Get All Groups And Fields Name From SettingsModel....
        /// </summary>
        private void GetGroupsAndFieldsNameFromSettingsModel()
        {
            //Dictionary<string, int> tmpDict = new Dictionary<string, int>();
            //tmpDict.Add("James", 44247);
            //tmpDict.Add("Markous", 43234);
            //tmpDict.Add("Maria", 5675);
            //tmpDict.Add("Helen", 1231);
            //tmpDict.Add("Annie", 97897);

            //string js = JsonConvert.SerializeObject(tmpDict).ToString();

            //Initialize Tab List
            //SettingsGroups = new List<AnnotationsNameModel>();

            //Initialize Components List
            Components = new List<ComponentTypesFromNamesModel>();

            //Reflection type
            Type t = typeof(SettingsModel);

            //Reads Settings Annotation nd gets property values
            IEnumerable<PropertyInfo> propInfo = GetAllProperties(typeof(SettingsModel));
            foreach (PropertyInfo item in propInfo)
            {
                ComponentTypesFromNamesModel fld = Components.Find(f => f.ComponentName == item.Name);
                if (fld == null)
                {
                    fld = new ComponentTypesFromNamesModel();
                    fld.ComponentName = item.Name;
                    Components.Add(fld);
                }

                if (item.CustomAttributes != null && item.CustomAttributes.Count() > 0)
                {
                    foreach (CustomAttributeData custAtt in item.CustomAttributes)
                    {
                        foreach (CustomAttributeNamedArgument nmAtt in custAtt.NamedArguments)
                        {
                            switch (nmAtt.MemberName)
                            {
                                case "Region":
                                    fld.TabDisplayName = nmAtt.TypedValue.Value.ToString();
                                    fld.TabName = "Settings" + Regex.Replace(nmAtt.TypedValue.Value.ToString(), "[^a-zA-Z0-9_]+", "");
                                    break;
                                case "Lebel":
                                    fld.Label = nmAtt.TypedValue.Value.ToString();
                                    break;
                                case "Help":
                                    fld.Hlp = nmAtt.TypedValue.Value.ToString();
                                    break;
                                case "Values":
                                    fld.CompoBoxValues = nmAtt.TypedValue.Value.ToString();
                                    break;
                                case "Width":
                                    fld.Width = int.Parse(nmAtt.TypedValue.Value.ToString());
                                    break;
                                case "Height":
                                    fld.Heigth = int.Parse(nmAtt.TypedValue.Value.ToString());
                                    break;
                                case "OrderBy":
                                    fld.OrderBy = int.Parse(nmAtt.TypedValue.Value.ToString());
                                    break;
                                case "Order":
                                    fld.OrderBy = int.Parse(nmAtt.TypedValue.Value.ToString());
                                    break;
                            }
                        }
                    }
                }
            }

            foreach (var item in t.GetProperties())
            {
                if (item.CustomAttributes != null && item.CustomAttributes.Count() > 0)
                {
                    var fld = propInfo.ToList().Find(f => f.Name == item.Name);
                    ComponentTypesFromNamesModel compObj = Components.Find(f => f.ComponentName == item.Name);
                    if (fld != null && compObj != null)
                    {
                        compObj.IsDictionary = false;
                        compObj.IsArray = false;

                        if (fld.PropertyType.FullName.Contains("String") &&
                            !fld.PropertyType.FullName.Contains("List`1") &&
                            !fld.PropertyType.FullName.Contains("Dictionary"))
                        {
                            compObj.IsString = true;
                        }
                        else
                        {
                            compObj.IsString = false;
                        }

                        if (fld.PropertyType.FullName.Contains("Int64") &&
                            !fld.PropertyType.FullName.Contains("List`1") &&
                            !fld.PropertyType.FullName.Contains("Dictionary"))
                        {
                            compObj.IsInt64 = true;
                        }
                        else
                        {
                            compObj.IsInt64 = false;
                        }

                        if (fld.PropertyType.FullName.Contains("Int32") &&
                            !fld.PropertyType.FullName.Contains("List`1") &&
                            !fld.PropertyType.FullName.Contains("Dictionary"))
                        {
                            compObj.IsInt = true;
                        }
                        else
                        {
                            compObj.IsInt = false;
                        }

                        if (fld.PropertyType.FullName.Contains("Decimal") &&
                            !fld.PropertyType.FullName.Contains("List`1") &&
                            !fld.PropertyType.FullName.Contains("Dictionary"))
                        {
                            compObj.IsFloat = true;
                        }
                        else
                        {
                            compObj.IsFloat = false;
                        }

                        if (fld.PropertyType.FullName.Contains("DateTime") &&
                            !fld.PropertyType.FullName.Contains("List`1") &&
                            !fld.PropertyType.FullName.Contains("Dictionary"))
                        {
                            compObj.IsDateTime = true;
                        }
                        else
                        {
                            compObj.IsDateTime = false;
                        }

                        if (fld.PropertyType.FullName.Contains("Boolean") &&
                            !fld.PropertyType.FullName.Contains("List`1") &&
                            !fld.PropertyType.FullName.Contains("Dictionary"))
                        {
                            compObj.IsBoolean = true;
                        }
                        else
                        {
                            compObj.IsBoolean = false;
                        }

                        if (fld.PropertyType.FullName.Contains("List`1"))
                        {
                            compObj.IsArray = true;
                            if (fld.PropertyType.FullName.Contains("String"))
                                compObj.IsString = true;
                            else
                            if (fld.PropertyType.FullName.Contains("Int64"))
                                compObj.IsInt64 = true;
                            else
                            if (fld.PropertyType.FullName.Contains("Int32"))
                                compObj.IsInt = true;
                            else
                            if (fld.PropertyType.FullName.Contains("Decimal"))
                                compObj.IsFloat = true;
                            else
                            if (fld.PropertyType.FullName.Contains("DateTime"))
                                compObj.IsDateTime = true;
                            else
                            if (fld.PropertyType.FullName.Contains("Boolean"))
                                compObj.IsBoolean = true;
                        }
                        else if (fld.PropertyType.FullName.Contains("Dictionary"))
                        {
                            compObj.IsDictionary = true;
                            if (fld.PropertyType.FullName.Contains(",[System.String"))
                                compObj.IsString = true;
                            else
                            if (fld.PropertyType.FullName.Contains(",[System.Int64"))
                                compObj.IsInt64 = true;
                            else
                            if (fld.PropertyType.FullName.Contains(",[System.Int32"))
                                compObj.IsInt = true;
                            else
                            if (fld.PropertyType.FullName.Contains(",[System.Decimal"))
                                compObj.IsFloat = true;
                            else
                            if (fld.PropertyType.FullName.Contains(",[System.DateTime"))
                                compObj.IsDateTime = true;
                            else
                            if (fld.PropertyType.FullName.Contains(",[System.Boolean"))
                                compObj.IsBoolean = true;
                        }

                    }
                }
            }
        }

        /// <summary>
        /// Loads File Names For Settings files
        /// </summary>
        private void LoadSettingsFilesForInheritedClass()
        {
            try
            {
                SettingsFileGrid.Items.Clear();
                List<string> res = new List<string>();
                res = jobsConfig.GetSettingsfiles();
                foreach (string item in res)
                {
                    SettingsFileGrid.Items.Add(item);
                }
            }
            finally
            {
            }
        }

        /// <summary>
        /// Clears all group boxes created for Settings File
        /// </summary>
        private void ClearAllSettingsGroupBox()
        {
            List<string> tabsName = new List<string>();
            //Parent Panel for all group box settings
            for (int i = 0; i < SettingsTabComp.TabPages.Count; i++)
            {
                tabsName.Add(SettingsTabComp.TabPages[i].Name);
            }
            foreach (string item in tabsName)
            {
                Control[] comb_pnl = Controls.Find(item, true);
                if (comb_pnl != null && comb_pnl.Count() > 0)
                {
                    Controls.Remove(comb_pnl[0]);
                    comb_pnl[0].Dispose();
                }
            }
        }

        /// <summary>
        /// Create New Tabs
        /// </summary>
        private void CreateTabs()
        {
            TabsForComponent.Clear();
            TabsForComponent = IniFileComponents.Select(s => new AnnotationsNameModel { TabSheetName = s.TabName, DisplayName = s.TabDisplayName, LastTop = 5, LastTabIdx = 1 }).Distinct().ToList();
            foreach (AnnotationsNameModel item in TabsForComponent)
            {
                Control[] pnl = SettingsTabComp.Controls.Find(item.TabSheetName, true);
                if (pnl == null || pnl.Count() == 0)
                {
                    TabPage tp = new TabPage(item.DisplayName);
                    tp.Name = item.TabSheetName;
                    tp.Parent = SettingsTabComp;
                }
                pnl = SettingsTabComp.Controls.Find(item.TabSheetName, true);
                //Creates new Control
                Panel pg = new Panel();
                pg.Name = "SettingsPanel_" + item.TabSheetName;
                pg.Parent = pnl[0];
                pg.Dock = DockStyle.Fill;
                pg.AutoScroll = true;
            }
        }

        /// <summary>
        /// Gets Values from Settings Model
        /// </summary>
        /// <param name="PropertyName"></param>
        /// <returns></returns>
        private object GetValueFromModel(string PropertyName)
        {
            PropertyInfo propInfoObj = FullSettignsModel.GetType().GetProperty(PropertyName);
            if (propInfoObj != null)
            {
                return FullSettignsModel
                            .GetType()
                            .InvokeMember(propInfoObj.Name,
                                        BindingFlags.GetProperty,
                                        null,
                                        FullSettignsModel,
                                        null);
            }
            else
                return null;
        }


        /// <summary>
        /// Clear Values to a comboboc control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBoxCtl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                (sender as ComboBox).Text = "";
                (sender as ComboBox).SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Clear Values for datetime picker
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DateTimeCtl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                (sender as DateTimePicker).Format = DateTimePickerFormat.Custom;
                (sender as DateTimePicker).CustomFormat = " ";
            }
        }

        /// <summary>
        /// Enables values on select datetime
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DateTimeCtl_ValueChanged(object sender, EventArgs e)
        {
            (sender as DateTimePicker).Format = DateTimePickerFormat.Short;
        }

        /// <summary>
        /// Creates group boxes and pannels for each settings category
        /// </summary>
        private void CreateSettingsComponents()
        {
            if (IniFileComponents == null || IniFileComponents.Count < 1)
                return;

            ClearAllSettingsGroupBox();
            CreateTabs();

            //List<ComponentListModel> comps = new List<ComponentListModel>();
            int maxH = SettingsTabComp.TabPages[0].Height - 20;
            int maxW = SettingsTabComp.TabPages[0].Width - 20;
            int crL = 5;
            int? curH = 0;

            Control[] pnl = null;
            AnnotationsNameModel tab;

            IniFileComponents.OrderBy(o => o.TabName).ThenBy(o => o.OrderBy);
            //var tmp = IniFileComponents.OrderBy(o => o.TabName).ThenBy(o => o.OrderBy);
            List<ComponentTypesFromNamesModel> tmp = IniFileComponents.OrderBy(o => o.TabName).ThenBy(o => o.OrderBy).ToList();

            foreach (ComponentTypesFromNamesModel item in tmp /*IniFileComponents*/)
            {

                pnl = Controls.Find("SettingsPanel_" + item.TabName, true);
                tab = TabsForComponent.Find(f => f.TabSheetName == item.TabName);

                if (pnl != null && tab != null)
                {
                    Label lb = new Label();
                    lb.Name = "Settings_Lbl_" + item.ComponentName;
                    lb.AutoSize = true;
                    lb.Text = (string.IsNullOrEmpty(item.Label) ? item.ComponentName : item.Label);
                    lb.Parent = pnl[0];
                    if (!string.IsNullOrEmpty(item.Hlp))
                    {
                        ToolTip tt = new ToolTip();
                        tt.InitialDelay = 50;
                        tt.SetToolTip(lb, item.Hlp);
                    }


                    crL = 5;
                    lb.Left = crL;
                    crL = crL + lb.Width + 5;
                    lb.Top = tab.LastTop;

                    if (!string.IsNullOrEmpty(item.CompoBoxValues))
                    {
                        string[] cmpItems = item.CompoBoxValues.Split(',');
                        ComboBox cb = new ComboBox();
                        cb.Name = "Settings_Ctl_" + item.ComponentName;
                        cb.Parent = pnl[0];
                        cb.Width = (item.Width == null || item.Width <= 0) ? 120 : item.Width ?? 120;
                        cb.Left = crL;
                        crL = crL + cb.Width + 5;
                        cb.Top = tab.LastTop;
                        if ((item.Heigth == null || item.Heigth <= 0))
                            curH = 20;
                        else
                            curH = item.Heigth;
                        cb.Height = curH ?? 0;
                        tab.LastTop = tab.LastTop + cb.Height + 5;
                        cb.Enabled = true;
                        cb.TabStop = true;
                        cb.TabIndex = tab.LastTabIdx;
                        tab.LastTabIdx++;
                        if (!string.IsNullOrEmpty(item.Hlp))
                        {
                            ToolTip tt = new ToolTip();
                            tt.InitialDelay = 50;
                            tt.SetToolTip(cb, item.Hlp);
                        }

                        foreach (string cmbVal in cmpItems)
                        {
                            cb.Items.Add(cmbVal);
                        }
                        cb.DropDownStyle = ComboBoxStyle.DropDownList;
                        object tmpVal = GetValueFromModel(item.ComponentName);
                        if (tmpVal != null)
                        {
                            if (item.IsString)
                            {
                                if (!string.IsNullOrEmpty(tmpVal.ToString()))
                                    cb.Text = tmpVal.ToString();
                                else
                                {
                                    cb.Text = cmpItems[0];
                                    cb.SelectedIndex = 0;
                                }
                            }
                            else
                            {
                                cb.SelectedIndex = (int)tmpVal;
                            }
                        }
                        else
                        {
                            cb.Text = cmpItems[0];
                            cb.SelectedIndex = 0;
                        }

                        cb.KeyDown += ComboBoxCtl_KeyDown;

                    }
                    else if (item.IsArray)
                    {
                        DataGridView gd = new DataGridView();
                        gd.Name = "Settings_Ctl_" + item.ComponentName;
                        gd.Parent = pnl[0];
                        gd.Width = (item.Width == null || item.Width <= 0) ? 70 : item.Width ?? 120;
                        gd.Left = crL;
                        crL = crL + gd.Width + 5;
                        gd.Top = tab.LastTop;
                        if ((item.Heigth == null || item.Heigth <= 0))
                            curH = 20;
                        else
                            curH = item.Heigth;
                        gd.Height = curH ?? 0;
                        tab.LastTop = tab.LastTop + gd.Height + 5;
                        gd.Enabled = true;
                        gd.ReadOnly = false;
                        object tmpVal = GetValueFromModel(item.ComponentName);
                        //List<SettingsStringListSourceModel> ds = new List<SettingsStringListSourceModel>();
                        ArrysList.Add(new List<SettingsStringListSourceModel>());
                        int maxArr = ArrysList.Count - 1;
                        if (tmpVal != null)
                        {
                            if (item.IsInt)
                            {
                                List<Int32?> List = ((IList<Int32?>)tmpVal).ToList();
                                foreach (Int32? strVal in List)
                                {
                                    ArrysList[maxArr].Add(new SettingsStringListSourceModel { StringValue = (strVal ?? 0).ToString() });
                                }
                            }
                            else if (item.IsInt64)
                            {
                                List<Int64?> List = ((IList<Int64?>)tmpVal).ToList();
                                foreach (Int64? strVal in List)
                                {
                                    ArrysList[maxArr].Add(new SettingsStringListSourceModel { StringValue = (strVal ?? 0).ToString() });
                                }
                            }
                            else if (item.IsString)
                            {
                                List<string> List = ((IList<string>)tmpVal).ToList();
                                foreach (string strVal in List)
                                {
                                    ArrysList[maxArr].Add(new SettingsStringListSourceModel { StringValue = strVal.ToString() });
                                }
                            }
                            else if (item.IsDateTime)
                            {
                                List<DateTime?> List = ((IList<DateTime?>)tmpVal).ToList();
                                foreach (DateTime? strVal in List)
                                {
                                    ArrysList[maxArr].Add(new SettingsStringListSourceModel { StringValue = (strVal ?? DateTime.Now).ToString() });
                                }
                            }
                            else if (item.IsBoolean)
                            {
                                List<bool?> List = ((IList<bool?>)tmpVal).ToList();
                                foreach (bool? strVal in List)
                                {
                                    ArrysList[maxArr].Add(new SettingsStringListSourceModel { StringValue = (strVal ?? false).ToString() });
                                }
                            }

                        }
                        gd.DataSource = new BindingList<SettingsStringListSourceModel>(ArrysList[maxArr]);
                        gd.Columns[0].HeaderText = "Value";
                        gd.TabStop = true;
                        gd.TabIndex = tab.LastTabIdx;
                        tab.LastTabIdx++;
                        if (!string.IsNullOrEmpty(item.Hlp))
                        {
                            ToolTip tt = new ToolTip();
                            tt.InitialDelay = 50;
                            tt.SetToolTip(gd, item.Hlp);
                        }
                        gd.AllowUserToAddRows = true;
                    }
                    else if (item.IsDictionary)
                    {
                        DataGridView gd = new DataGridView();
                        gd.Name = "Settings_Ctl_" + item.ComponentName;
                        gd.Parent = pnl[0];
                        gd.Width = (item.Width == null || item.Width <= 0) ? 70 : item.Width ?? 120;
                        gd.Left = crL;
                        crL = crL + gd.Width + 5;
                        gd.Top = tab.LastTop;
                        if ((item.Heigth == null || item.Heigth <= 0))
                            curH = 20;
                        else
                            curH = item.Heigth;
                        gd.Height = curH ?? 0;
                        tab.LastTop = tab.LastTop + gd.Height + 5;
                        gd.Enabled = true;
                        gd.ReadOnly = false;
                        object tmpVal = GetValueFromModel(item.ComponentName);
                        //List<SettingsDictionaryModel> ds = new List<SettingsDictionaryModel>();
                        DictionList.Add(new List<SettingsDictionaryModel>());
                        int maxIdx = DictionList.Count - 1;
                        if (tmpVal != null)
                        {
                            if (item.IsInt)
                            {
                                foreach (var itVal in (Dictionary<string, int>)tmpVal)
                                {
                                    DictionList[maxIdx].Add(new SettingsDictionaryModel { KeyValue = itVal.Key, ValueValue = itVal.Value.ToString() });
                                }
                            }
                            else if (item.IsInt64)
                            {
                                foreach (var itVal in (Dictionary<string, Int64>)tmpVal)
                                {
                                    DictionList[maxIdx].Add(new SettingsDictionaryModel { KeyValue = itVal.Key, ValueValue = itVal.Value.ToString() });
                                }
                            }
                            else if (item.IsString)
                            {
                                foreach (var itVal in (Dictionary<string, string>)tmpVal)
                                {
                                    DictionList[maxIdx].Add(new SettingsDictionaryModel { KeyValue = itVal.Key, ValueValue = itVal.Value.ToString() });
                                }
                            }
                            else if (item.IsDateTime)
                            {
                                foreach (var itVal in (Dictionary<string, DateTime>)tmpVal)
                                {
                                    DictionList[maxIdx].Add(new SettingsDictionaryModel { KeyValue = itVal.Key, ValueValue = itVal.Value.ToString() });
                                }
                            }
                            else if (item.IsBoolean)
                            {
                                foreach (var itVal in (Dictionary<string, bool>)tmpVal)
                                {
                                    DictionList[maxIdx].Add(new SettingsDictionaryModel { KeyValue = itVal.Key, ValueValue = itVal.Value.ToString() });
                                }
                            }
                        }
                        gd.DataSource = new BindingList<SettingsDictionaryModel>(DictionList[maxIdx]);
                        gd.Columns[0].HeaderText = "Key";
                        gd.Columns[1].HeaderText = "Value";
                        gd.TabStop = true;
                        gd.TabIndex = tab.LastTabIdx;
                        tab.LastTabIdx++;
                        if (!string.IsNullOrEmpty(item.Hlp))
                        {
                            ToolTip tt = new ToolTip();
                            tt.InitialDelay = 50;
                            tt.SetToolTip(gd, item.Hlp);
                        }
                        gd.AllowUserToAddRows = true;
                    }
                    else if (item.IsString && string.IsNullOrEmpty(item.CompoBoxValues))
                    {
                        TextBox tx = new TextBox();
                        tx.Name = "Settings_Ctl_" + item.ComponentName;
                        tx.Parent = pnl[0];
                        tx.Width = (item.Width == null || item.Width <= 0) ? 120 : item.Width ?? 120;
                        tx.Left = crL;
                        crL = crL + tx.Width + 5;
                        tx.Top = tab.LastTop;
                        if ((item.Heigth == null || item.Heigth <= 0))
                            curH = 20;
                        else
                            curH = item.Heigth;
                        if (curH > 20)
                        {
                            tx.Multiline = true;
                            tx.ScrollBars = ScrollBars.Both;
                        }
                        tx.Height = curH ?? 0;
                        tab.LastTop = tab.LastTop + tx.Height + 5;
                        object tmpVal = GetValueFromModel(item.ComponentName);
                        if (tmpVal != null)
                            tx.Text = tmpVal.ToString();
                        else
                            tx.Text = "";
                        tx.TabStop = true;
                        tx.TabIndex = tab.LastTabIdx;
                        tab.LastTabIdx++;
                        if (!string.IsNullOrEmpty(item.Hlp))
                        {
                            ToolTip tt = new ToolTip();
                            tt.InitialDelay = 50;
                            tt.SetToolTip(tx, item.Hlp);
                        }
                        //tx.Enabled = item.TabName == "General" ? false : true;
                        tx.ReadOnly = item.TabName == "SettingsGeneral" ? true : false;
                        string tmpDB = item.ComponentName.Substring(item.ComponentName.Length - 2, 2);
                        if (tmpDB == "DB")
                        {
                            tx.BackColor = Color.Black;
                            tx.Leave += DBConString_Leave;
                            tx.Enter += DBConString_Enter;
                        }
                    }
                    else if (item.IsFloat || item.IsInt || item.IsInt64)
                    {
                        NumericUpDown nm = new NumericUpDown();
                        nm.Minimum = -100000000;
                        nm.Maximum = 1000000000;
                        nm.Name = "Settings_Ctl_" + item.ComponentName;
                        nm.Value = 0;
                        if (item.IsFloat)
                            nm.DecimalPlaces = 2;
                        else
                            nm.DecimalPlaces = 0;
                        nm.Parent = pnl[0];
                        nm.Width = (item.Width == null || item.Width <= 0) ? 70 : item.Width ?? 70;
                        nm.Left = crL;
                        crL = crL + nm.Width + 5;
                        nm.Top = tab.LastTop;
                        if ((item.Heigth == null || item.Heigth <= 0))
                            curH = 20;
                        else
                            curH = item.Heigth;
                        nm.Height = curH ?? 0;
                        tab.LastTop = tab.LastTop + nm.Height + 5;
                        nm.Enabled = true;
                        nm.ReadOnly = false;
                        object tmpVal = GetValueFromModel(item.ComponentName);
                        if (tmpVal != null)
                            nm.Value = decimal.Parse(tmpVal.ToString());
                        else
                            nm.Value = 0;
                        nm.TabStop = true;
                        nm.TabIndex = tab.LastTabIdx;
                        tab.LastTabIdx++;
                        if (!string.IsNullOrEmpty(item.Hlp))
                        {
                            ToolTip tt = new ToolTip();
                            tt.InitialDelay = 50;
                            tt.SetToolTip(nm, item.Hlp);
                        }
                    }
                    else if (item.IsDateTime)
                    {
                        DateTimePicker dt = new DateTimePicker();
                        dt.Name = "Settings_Ctl_" + item.ComponentName;
                        dt.Parent = pnl[0];
                        dt.Width = (item.Width == null || item.Width <= 0) ? 70 : item.Width ?? 80;
                        dt.Left = crL;
                        crL = crL + dt.Width + 5;
                        dt.Top = tab.LastTop;
                        if ((item.Heigth == null || item.Heigth <= 0))
                            curH = 20;
                        else
                            curH = item.Heigth;
                        dt.Height = curH ?? 0;
                        tab.LastTop = tab.LastTop + dt.Height + 5;
                        dt.Enabled = true;
                        dt.Format = DateTimePickerFormat.Custom;

                        dt.KeyDown += DateTimeCtl_KeyDown;
                        dt.ValueChanged += DateTimeCtl_ValueChanged;

                        object tmpVal = GetValueFromModel(item.ComponentName);
                        if (tmpVal != null)
                        {
                            dt.Format = DateTimePickerFormat.Short;
                            dt.Value = DateTime.Parse(tmpVal.ToString());
                        }
                        else
                        {
                            dt.Format = DateTimePickerFormat.Custom;
                            dt.CustomFormat = " ";
                        }
                        dt.TabStop = true;
                        dt.TabIndex = tab.LastTabIdx;
                        tab.LastTabIdx++;
                        if (!string.IsNullOrEmpty(item.Hlp))
                        {
                            ToolTip tt = new ToolTip();
                            tt.InitialDelay = 50;
                            tt.SetToolTip(dt, item.Hlp);
                        }
                    }
                    else if (item.IsBoolean)
                    {
                        CheckBox ch = new CheckBox();
                        ch.Text = "";
                        ch.Name = "Settings_Ctl_" + item.ComponentName;
                        ch.Parent = pnl[0];
                        ch.Width = (item.Width == null || item.Width <= 0) ? 70 : item.Width ?? 80;
                        ch.Left = crL;
                        crL = crL + ch.Width + 5;
                        ch.Top = tab.LastTop;
                        if ((item.Heigth == null || item.Heigth <= 0))
                            curH = 20;
                        else
                            curH = item.Heigth;
                        ch.Height = curH ?? 0;
                        tab.LastTop = tab.LastTop + ch.Height + 5;
                        ch.Enabled = true;

                        object tmpVal = GetValueFromModel(item.ComponentName);
                        if (tmpVal != null)
                            ch.Checked = Boolean.Parse(tmpVal.ToString());
                        else
                            ch.Checked = false;
                        ch.TabStop = true;
                        ch.TabIndex = tab.LastTabIdx;
                        tab.LastTabIdx++;
                        if (!string.IsNullOrEmpty(item.Hlp))
                        {
                            ToolTip tt = new ToolTip();
                            tt.InitialDelay = 50;
                            tt.SetToolTip(ch, item.Hlp);
                        }
                    }

                }
            }
        }

        /// <summary>
        /// Load Settings file to local var
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SettingsLoadFiles_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(SettingsFileGrid.Text))
            {
                FullSettignsModel = ReadModelFromIniFile(SettingsFileGrid.Text);
                if (FullSettignsModel == null)
                    return;
                if (string.IsNullOrEmpty(FullSettignsModel.SettingsFile))
                    FullSettignsModel.SettingsFile = SettingsFileGrid.Text;
                if (string.IsNullOrEmpty(FullSettignsModel.FullClassName))
                {
                    MessageBox.Show("Class Name not exists", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                CreateComponentsFromClassName(FullSettignsModel.FullClassName);
                ActionMode = 1;
            }
        }

        /// <summary>
        /// Gets components from basic class and creates components to form
        /// </summary>
        /// <param name="ClassName"></param>
        private void CreateComponentsFromClassName(string ClassName)
        {
            FileHelpers fh = new FileHelpers();
            string settingsValue = "";
            try
            {
                Assembly asm;
                List<IEnumerable<CustomAttributeData>> LoadTypes = new List<IEnumerable<CustomAttributeData>>();
                foreach (JobsAssemblyModel model in ScheduledJob.JobsOriginList)
                {
                    if (model.ClassName == ClassName)
                    {
                        if (model.JobsOrigin == 3)
                        {
                            //string path = fh.GetHitServicesWAPath();
                            //asm = Assembly.LoadFrom(path);
                            asm = model.Assembly;
                            LoadTypes = (from t in asm.GetExportedTypes()
                                         where !t.IsInterface && !t.IsAbstract
                                         where t.FullName == ClassName
                                         select t.CustomAttributes).ToList();
                        }
                        else if (model.JobsOrigin == 2)
                        {
                            //asm = GetExtAssembly(ClassName);
                            asm = model.Assembly;
                            if (asm != null)
                            {
                                LoadTypes = (from t in asm.GetExportedTypes()
                                             where !t.IsInterface && !t.IsAbstract
                                             where t.FullName == ClassName
                                             select t.CustomAttributes).ToList();
                            }
                        }
                        else
                        {
                            //asm = typeof(Hit.Services.Scheduler.Core.HangfireBootstrapper).Assembly;
                            asm = model.Assembly;
                            LoadTypes = (from t in asm.GetExportedTypes()
                                         where !t.IsInterface && !t.IsAbstract
                                         where t.FullName == ClassName
                                         select t.CustomAttributes).ToList();
                        }
                    }
                }

                if (LoadTypes != null && LoadTypes.Count > 0)
                {
                    foreach (CustomAttributeData item in LoadTypes[0])
                    {
                        if (item.AttributeType.Name.IndexOf("DisplayColumnAttribute") > -1)
                        {
                            if (item.ConstructorArguments != null && item.ConstructorArguments.Count > 0)
                            {
                                settingsValue = item.ConstructorArguments[0].Value.ToString();
                            }
                        }
                        else if (item.AttributeType.Name.IndexOf("DescribeAttribute") > -1)
                        {
                            if (item.NamedArguments != null && item.NamedArguments.Count > 0)
                            {
                                foreach (CustomAttributeNamedArgument attrName in item.NamedArguments)
                                {
                                    if (attrName.MemberName == "Type" && string.IsNullOrEmpty(FullSettignsModel.ClassType))
                                    {
                                        FullSettignsModel.ClassType = attrName.TypedValue.Value.ToString() ?? "";
                                    }
                                    else if (attrName.MemberName == "Description" && string.IsNullOrEmpty(FullSettignsModel.ClassDescription))
                                    {
                                        FullSettignsModel.ClassDescription = attrName.TypedValue.Value.ToString() ?? "";
                                    }
                                }
                            }
                        }
                        //if (LoadTypes[0].FirstOrDefault().ConstructorArguments != null && LoadTypes[0].FirstOrDefault().ConstructorArguments.Count > 0)
                        //    settingsValue = LoadTypes[0].FirstOrDefault().ConstructorArguments[0].Value.ToString();
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Reading available Controllers", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                log.Error(ex.ToString());
            }

            string[] settingsFields = new string[] { };
            if (!string.IsNullOrEmpty(settingsValue))
            {
                settingsFields = settingsValue.Split(',');
            }

            bool bType = false;
            bool bDescr = false;
            bool bSetFile = false;
            foreach (string item in settingsFields)
            {
                if (item == "ClassType")
                    bType = true;
                if (item == "ClassDescription")
                    bDescr = true;
                if (item == "SettingsFile")
                    bSetFile = true;
            }
            if (!bType)
            {
                Array.Resize(ref settingsFields, settingsFields.Length + 1);
                settingsFields[settingsFields.Length - 1] = "ClassType";
            }
            if (!bDescr)
            {
                Array.Resize(ref settingsFields, settingsFields.Length + 1);
                settingsFields[settingsFields.Length - 1] = "ClassDescription";
            }
            if (!bSetFile)
            {
                Array.Resize(ref settingsFields, settingsFields.Length + 1);
                settingsFields[settingsFields.Length - 1] = "SettingsFile";
            }

            IniFileComponents = new List<ComponentTypesFromNamesModel>();
            foreach (string item in settingsFields)
            {
                var fld = Components.Find(f => f.ComponentName == item);
                if (fld != null)
                {
                    IniFileComponents.Add(fld);
                }
            }
            CreateSettingsComponents();

        }

        /// <summary>
        /// Show's a menu with option to Create and copy a setting file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SettingsCreateFileName_Click(object sender, EventArgs e)
        {
            SettingsCnNewSettings.Show(SettingsCreateFileName, new Point(0, SettingsCreateFileName.Height));
        }

        /// <summary>
        /// Checks if file name contains special characters
        /// </summary>
        /// <param name="flName"></param>
        /// <returns></returns>
        private bool FileNameIsValid(string flName)
        {
            var regExpr = new Regex("^[a-zA-Z0-9_-]*$");
            return regExpr.IsMatch(flName);
        }

        /// <summary>
        /// Creates new inifile name based on a master class
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SettingsCnCreate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(settingsCtlSelection.Text))
            {
                MessageBox.Show("Select a base Class", "Class Name", MessageBoxButtons.OK, MessageBoxIcon.Information);
                settingsCtlSelection.Focus();
                return;
            }
            if (string.IsNullOrEmpty(SettingsNewFileName.Text))
            {
                MessageBox.Show("Select file name from ini file", "File Name", MessageBoxButtons.OK, MessageBoxIcon.Information);
                SettingsNewFileName.Focus();
                return;
            }
            if (!FileNameIsValid(SettingsNewFileName.Text))
            {
                MessageBox.Show("File name contains special characters. \r\nAvailable characters     a to z, A to Z, 0 to 9, _ and -", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SettingsNewFileName.Focus();
                return;
            }
            int fld = SettingsFileGrid.FindStringExact(SettingsNewFileName.Text);
            if (fld > -1)
            {
                MessageBox.Show("File name already exists", "File Exists", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            FullSettignsModel = new SettingsModel();
            FullSettignsModel.FullClassName = settingsCtlSelection.Text;
            CreateComponentsFromClassName(FullSettignsModel.FullClassName);
            ActionMode = 1;
            Control[] setFile = Controls.Find("Settings_Ctl_SettingsFile", true);
            if (setFile != null)
                ((TextBox)setFile[0]).Text = SettingsNewFileName.Text;
            FullSettignsModel.SettingsFile = SettingsNewFileName.Text;
            int idx = SettingsFileGrid.FindStringExact(SettingsNewFileName.Text);
            if (idx < 0)
                SettingsFileGrid.Items.Add(SettingsNewFileName.Text);
            SettingsFileGrid.Text = SettingsNewFileName.Text;
            SettingsFileGrid.SelectedText = SettingsNewFileName.Text;
            settingsBtnSaveNew_Click(settingsBtnSaveNew, e);
            initJobs();
            initApiSettings();
        }

        /// <summary>
        /// Copy a settings file to another
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SettingsCnCopyFrom_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(SettingsNewFileName.Text))
            {
                MessageBox.Show("Select file name from ini file", "File Name", MessageBoxButtons.OK, MessageBoxIcon.Information);
                SettingsNewFileName.Focus();
                return;
            }
            if (string.IsNullOrEmpty(SettingsFileGrid.Text) || IniFileComponents == null)
            {
                MessageBox.Show("Select a file settings and load values before copy", "Load File", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!FileNameIsValid(SettingsNewFileName.Text))
            {
                MessageBox.Show("File name contains special characters. \r\nAvailable characters     a to z, A to Z, 0 to 9, _ and -", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SettingsNewFileName.Focus();
                return;
            }

            Control[] setFile = Controls.Find("Settings_Ctl_SettingsFile", true);
            if (setFile != null)
                ((TextBox)setFile[0]).Text = SettingsNewFileName.Text;
            FullSettignsModel.SettingsFile = SettingsNewFileName.Text;
            int fld = SettingsFileGrid.FindStringExact(SettingsNewFileName.Text);
            if (fld < 0)
                SettingsFileGrid.Items.Add(SettingsNewFileName.Text);
            SettingsFileGrid.Text = SettingsNewFileName.Text;
            SettingsFileGrid.SelectedText = SettingsNewFileName.Text;
            ActionMode = 1;
            settingsBtnSaveNew_Click(settingsBtnSaveNew, e);
            initJobs();
            initApiSettings();
        }

        /// <summary>
        /// Sets Values to Model from Form Componets
        /// </summary>
        private bool SetValuesToModel()
        {
            bool res = true;
            try
            {
                List<string> lstTabs = IniFileComponents.Select(s => s.TabName).Distinct().ToList();

                //foreach (AnnotationsNameModel tab in TabsForComponent)
                foreach (string tabName in lstTabs)
                {
                    Control[] pnl = Controls.Find("SettingsPanel_" + tabName, true);
                    if (pnl[0] != null)
                    {
                        foreach (Control item in pnl[0].Controls)
                        {
                            if (item.Name.IndexOf("_Ctl_") > -1)
                            {
                                string compName = item.Name;
                                while (compName.IndexOf('_') > -1)
                                {
                                    compName = compName.Substring(compName.IndexOf('_') + 1, compName.Length - compName.IndexOf('_') - 1);
                                }
                                PropertyInfo propInfo = FullSettignsModel.GetType().GetProperty(compName);
                                var fld = IniFileComponents.Find(f => f.ComponentName == compName);
                                if (fld != null)
                                {
                                    if (fld.IsArray)
                                    {
                                        Control[] gd = Controls.Find("Settings_Ctl_" + fld.ComponentName, true);
                                        if (gd != null)
                                        {
                                            object ds = ((DataGridView)gd[0]).DataSource;
                                            if (fld.IsString)
                                            {
                                                List<string> iList = new List<string>();
                                                foreach (SettingsStringListSourceModel itList in (BindingList<SettingsStringListSourceModel>)ds)
                                                {
                                                    iList.Add(itList.StringValue);
                                                }
                                                if (iList.Count <= 0)
                                                    propInfo.SetValue(FullSettignsModel, null, null);
                                                else
                                                    propInfo.SetValue(FullSettignsModel, iList, null);
                                            }
                                            else if (fld.IsInt)
                                            {
                                                List<Int32?> iList = new List<Int32?>();
                                                foreach (SettingsStringListSourceModel itList in (BindingList<SettingsStringListSourceModel>)ds)
                                                {
                                                    iList.Add(Int32.Parse(itList.StringValue));
                                                }
                                                if (iList.Count <= 0)
                                                    propInfo.SetValue(FullSettignsModel, null, null);
                                                else
                                                    propInfo.SetValue(FullSettignsModel, iList, null);
                                            }
                                            else if (fld.IsInt64)
                                            {
                                                List<Int64?> iList = new List<Int64?>();
                                                foreach (SettingsStringListSourceModel itList in (BindingList<SettingsStringListSourceModel>)ds)
                                                {
                                                    iList.Add(Int64.Parse(itList.StringValue));
                                                }
                                                if (iList.Count <= 0)
                                                    propInfo.SetValue(FullSettignsModel, null, null);
                                                else
                                                    propInfo.SetValue(FullSettignsModel, iList, null);
                                            }
                                            else if (fld.IsDateTime)
                                            {
                                                List<DateTime?> iList = new List<DateTime?>();
                                                foreach (SettingsStringListSourceModel itList in (BindingList<SettingsStringListSourceModel>)ds)
                                                {
                                                    iList.Add(DateTime.Parse(itList.StringValue));
                                                }
                                                if (iList.Count <= 0)
                                                    propInfo.SetValue(FullSettignsModel, null, null);
                                                else
                                                    propInfo.SetValue(FullSettignsModel, iList, null);
                                            }
                                            else if (fld.IsBoolean)
                                            {
                                                List<bool?> iList = new List<bool?>();
                                                foreach (SettingsStringListSourceModel itList in (BindingList<SettingsStringListSourceModel>)ds)
                                                {
                                                    iList.Add(bool.Parse(itList.StringValue));
                                                }
                                                if (iList.Count <= 0)
                                                    propInfo.SetValue(FullSettignsModel, null, null);
                                                else
                                                    propInfo.SetValue(FullSettignsModel, iList, null);
                                            }
                                        }
                                    }
                                    else if (fld.IsDictionary)
                                    {
                                        Control[] gd = Controls.Find("Settings_Ctl_" + fld.ComponentName, true);
                                        if (gd != null)
                                        {
                                            object ds = ((DataGridView)gd[0]).DataSource;
                                            if (fld.IsString)
                                            {
                                                Dictionary<string, string> iDict = new Dictionary<string, string>();
                                                foreach (SettingsDictionaryModel itList in (BindingList<SettingsDictionaryModel>)ds)
                                                {
                                                    iDict.Add(itList.KeyValue, itList.ValueValue);
                                                }
                                                if (iDict.Count <= 0)
                                                    propInfo.SetValue(FullSettignsModel, null, null);
                                                else
                                                    propInfo.SetValue(FullSettignsModel, iDict, null);
                                            }
                                            else if (fld.IsInt)
                                            {
                                                Dictionary<string, Int32?> iDict = new Dictionary<string, Int32?>();
                                                foreach (SettingsDictionaryModel itList in (BindingList<SettingsDictionaryModel>)ds)
                                                {
                                                    iDict.Add(itList.KeyValue, Int32.Parse(itList.ValueValue));
                                                }
                                                if (iDict.Count <= 0)
                                                    propInfo.SetValue(FullSettignsModel, null, null);
                                                else
                                                    propInfo.SetValue(FullSettignsModel, iDict, null);
                                            }
                                            else if (fld.IsInt64)
                                            {
                                                Dictionary<string, Int64?> iDict = new Dictionary<string, Int64?>();
                                                foreach (SettingsDictionaryModel itList in (BindingList<SettingsDictionaryModel>)ds)
                                                {
                                                    iDict.Add(itList.KeyValue, Int64.Parse(itList.ValueValue));
                                                }
                                                if (iDict.Count <= 0)
                                                    propInfo.SetValue(FullSettignsModel, null, null);
                                                else
                                                    propInfo.SetValue(FullSettignsModel, iDict, null);
                                            }
                                            else if (fld.IsDateTime)
                                            {
                                                Dictionary<string, DateTime?> iDict = new Dictionary<string, DateTime?>();
                                                foreach (SettingsDictionaryModel itList in (BindingList<SettingsDictionaryModel>)ds)
                                                {
                                                    iDict.Add(itList.KeyValue, DateTime.Parse(itList.ValueValue));
                                                }
                                                if (iDict.Count <= 0)
                                                    propInfo.SetValue(FullSettignsModel, null, null);
                                                else
                                                    propInfo.SetValue(FullSettignsModel, iDict, null);
                                            }
                                            else if (fld.IsBoolean)
                                            {
                                                Dictionary<string, bool?> iDict = new Dictionary<string, bool?>();
                                                foreach (SettingsDictionaryModel itList in (BindingList<SettingsDictionaryModel>)ds)
                                                {
                                                    iDict.Add(itList.KeyValue, bool.Parse(itList.ValueValue));
                                                }
                                                if (iDict.Count <= 0)
                                                    propInfo.SetValue(FullSettignsModel, null, null);
                                                else
                                                    propInfo.SetValue(FullSettignsModel, iDict, null);
                                            }
                                        }
                                    }
                                    else if (!string.IsNullOrEmpty(fld.CompoBoxValues))
                                    {
                                        Control[] cb = Controls.Find("Settings_Ctl_" + fld.ComponentName, true);
                                        if (cb != null)
                                        {
                                            if (fld.IsString)
                                                propInfo.SetValue(FullSettignsModel, ((ComboBox)cb[0]).Text, null);
                                            else
                                                propInfo.SetValue(FullSettignsModel, ((ComboBox)cb[0]).SelectedIndex, null);
                                        }
                                    }
                                    else if (fld.IsString)
                                        //propInfo.SetValue(FullSettignsModel, Convert.ChangeType((item as TextBox).Text, propInfo.PropertyType), null);
                                        propInfo.SetValue(FullSettignsModel, ((TextBox)item).Text, null);
                                    else if (fld.IsInt64)
                                    {
                                        if (((NumericUpDown)item).Value == null)
                                            propInfo.SetValue(FullSettignsModel, Int64.Parse(((NumericUpDown)item).Value.ToString()), null);
                                        else
                                            //propInfo.SetValue(FullSettignsModel, Convert.ChangeType((item as NumericUpDown).Value, propInfo.PropertyType), null);
                                            propInfo.SetValue(FullSettignsModel, Int64.Parse(((NumericUpDown)item).Value.ToString()), null);
                                    }
                                    else if (fld.IsFloat)
                                    {
                                        if (((NumericUpDown)item).Value == null)
                                            propInfo.SetValue(FullSettignsModel, null, null);
                                        else
                                            propInfo.SetValue(FullSettignsModel, ((NumericUpDown)item).Value, null);

                                    }
                                    else if (fld.IsInt)
                                    {
                                        if (((NumericUpDown)item).Value == null)
                                            propInfo.SetValue(FullSettignsModel, null, null);
                                        else
                                            propInfo.SetValue(FullSettignsModel, Int32.Parse(((NumericUpDown)item).Value.ToString()), null);

                                    }
                                    else if (fld.IsDateTime)
                                    {
                                        if ((item as DateTimePicker).Text.Trim().Length == 0)
                                            propInfo.SetValue(FullSettignsModel, null, null);
                                        else
                                            //propInfo.SetValue(FullSettignsModel, Convert.ChangeType((item as DateTimePicker).Value, propInfo.PropertyType), null);
                                            propInfo.SetValue(FullSettignsModel, ((DateTimePicker)item).Value, null);
                                    }
                                    else if (fld.IsBoolean)
                                    {
                                        Control[] chk = Controls.Find("Settings_Ctl_" + fld.ComponentName, true);
                                        if (chk != null)
                                            propInfo.SetValue(FullSettignsModel, ((CheckBox)chk[0]).Checked, null);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                res = false;
                MessageBox.Show("Can not save values to file : " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                log.Error("SetValuesToModel \r\n" + ex.ToString());
            }
            return res;
        }

        /// <summary>
        /// Saves new settings files
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void settingsBtnSaveNew_Click(object sender, EventArgs e)
        {
            string fileName = "";
            if (ActionMode == null)
                return;
            if (ActionMode == 0)
                fileName = SettingsNewFileName.Text;
            else if (ActionMode == 1)
                fileName = SettingsFileGrid.Text;

            if (!string.IsNullOrEmpty(fileName) && FullSettignsModel != null && (ActionMode == 1 ? true : (FullSettignsModel.FullClassName == settingsCtlSelection.Text)))
            {
                if (!SetValuesToModel())
                    return;
                try
                {
                    configHLP.WriteSettingsFile(fileName, FullSettignsModel, encrStatus.SettingsFilesAreEncrypted);
                    int fld = SettingsFileGrid.FindStringExact(fileName);
                    if (fld < 0)
                        SettingsFileGrid.Items.Add(fileName);
                    ActionMode = 1;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Can not save file : " + fileName + "  " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    log.Error(ex.ToString());
                    return;
                }
                MessageBox.Show("Operation completed", "Save file", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                string sMess = "";
                if (string.IsNullOrEmpty(fileName))
                    sMess = "File name missing";
                else if (FullSettignsModel == null)
                    sMess = "Settings values missing";
                else if (FullSettignsModel.FullClassName != settingsCtlSelection.Text)
                    sMess = "Different class between file and selection";
                MessageBox.Show(sMess, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        /// <summary>
        /// Deletes a file settings
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SettingsDeleteFile_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(SettingsFileGrid.Text))
            {
                //List<string> jobsSettingFiles = jobsConfig.GetSettingsfiles();
                var fld = jobsConfig.Scheduler.Jobs.Find(f => f.Settings == SettingsFileGrid.Text);
                if (fld != null)
                {
                    MessageBox.Show("The file " + SettingsFileGrid.Text + " is in use on Jobs", "In use", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                var fld1 = apiConfig.ApiSettings.Find(f => f.Settings == SettingsFileGrid.Text);
                if (fld1 != null)
                {
                    MessageBox.Show("The file " + SettingsFileGrid.Text + " is in use on Api's controller", "In use", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                DialogResult result = MessageBox.Show("Do you want to delete file " + SettingsFileGrid.Text + "?", "Confirmation", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        configHLP.DeleteSettingsFile(SettingsFileGrid.Text);
                        int idx = SettingsFileGrid.FindStringExact(SettingsNewFileName.Text);
                        if (idx > -1)
                            SettingsFileGrid.Items.RemoveAt(idx);
                        SettingsFileGrid.SelectedIndex = 0;
                        SettingsLoadFiles_Click(SettingsLoadFiles, e);
                        initJobs();
                        initApiSettings();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Can not delete file " + SettingsFileGrid.Text + " : " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        log.Error(ex.ToString());
                    }
                }
            }
        }

        /// <summary>
        /// Reload Settings Tabs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SettingsBtnReset_Click(object sender, EventArgs e)
        {
            initSettings();
            ClearAllSettingsGroupBox();
            SettingsFileGrid.SelectedIndex = -1;
            SettingsFileGrid.Text = "";
            settingsCtlSelection.Text = "";
            settingsCtlSelection.SelectedIndex = -1;
            SettingsNewFileName.Text = "";

        }
    }
}
