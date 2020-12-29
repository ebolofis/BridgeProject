using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hit.Services.Models
{
    /// <summary>
    /// Annotation Model to Create Tabs
    /// </summary>
    public class AnnotationsNameModel
    {
        /// <summary>
        /// Display the caption to Tab Sheet
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Name for Tab Sheet
        /// </summary>
        public string TabSheetName { get; set; }

        /// <summary>
        /// Last Top Component Value
        /// </summary>
        public int LastTop { get; set; }

        /// <summary>
        /// Last Tab Index
        /// </summary>
        public int LastTabIdx { get; set; }
    }

    /// <summary>
    /// Model To Create Component
    /// </summary>
    public class ComponentTypesFromNamesModel
    {
        /// <summary>
        /// Component Name
        /// </summary>
        public string ComponentName { get; set; }

        /// <summary>
        /// Tabs to belong
        /// </summary>
        public string TabName { get; set; }

        /// <summary>
        /// Caption for TabSheet
        /// </summary>
        public string TabDisplayName { get; set; }

        /// <summary>
        /// Type of String
        /// </summary>
        public bool IsString { get; set; }

        /// <summary>
        /// Type of Int
        /// </summary>
        public bool IsInt { get; set; }

        /// <summary>
        /// Type Of Int64
        /// </summary>
        public bool IsInt64 { get; set; }

        /// <summary>
        /// Type of float, Decimal
        /// </summary>
        public bool IsFloat { get; set; }

        /// <summary>
        /// Type of Datetime
        /// </summary>
        public bool IsDateTime { get; set; }

        /// <summary>
        /// Type of boolean for chack box
        /// </summary>
        public bool IsBoolean { get; set; }

        /// <summary>
        /// If is list
        /// </summary>
        public bool IsArray { get; set; }

        /// <summary>
        /// The property iss dictionary. List with two columns
        /// </summary>
        public bool IsDictionary { get; set; }

        /// <summary>
        /// Width for Component, If null or 0 then default
        /// </summary>
        public int? Width { get; set; }

        /// <summary>
        /// Heigth for Component, If null or 0 then default
        /// if > 20 and text box then multiline with autoscroll
        /// </summary>
        public int? Heigth { get; set; }

        /// <summary>
        /// Caption for label
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// Text Hint for component
        /// </summary>
        public string Hlp { get; set; }

        /// <summary>
        /// Display component as combobox with specific values
        /// </summary>
        public string CompoBoxValues { get; set; }

        /// <summary>
        /// Creation Order
        /// </summary>
        public int OrderBy { get; set; }
    }

    /// <summary>
    /// List Of Component per Group Box
    /// </summary>
    public class ComponentListModel
    {
        /// <summary>
        /// Group box Name
        /// </summary>
        public string grpName { get; set; }

        /// <summary>
        /// Last Left Position
        /// </summary>
        public int lastLeft { get; set; }

        /// <summary>
        /// Lst Top Position
        /// </summary>
        public int lastTop { get; set; }
    }

    /// <summary>
    /// Model with File names to bind on a grid
    /// </summary>
    //public class SettingsFileNameModel
    //{
    //    public SettingsFileNameModel(string s)
    //    {
    //        _value = s;
    //    }
    //    public string Value { get { return _value; } set { _value = value; } }
    //    string _value;
    //}

    /// <summary>
    /// List of String to use a datasource to datagridview
    /// </summary>
    public class SettingsStringListSourceModel
    {
        public string StringValue { get; set; }
    }

    /// <summary>
    /// List of Dictionaris to use as datasource to datagridview
    /// </summary>
    public class SettingsDictionaryModel
    {
        /// <summary>
        /// Key Column
        /// </summary>
        public string KeyValue { get; set; }

        /// <summary>
        /// Value Column
        /// </summary>
        public string ValueValue { get; set; }
    }
}
