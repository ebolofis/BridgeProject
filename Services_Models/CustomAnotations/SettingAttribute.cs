using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hit.Services.Models.CustomAnotations
{
    /// <summary>
    /// Attribute that describes a method in the SettingsModel class
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false)]
    public class SettingAttribute : Attribute
    {
        /// <summary>
        /// Method's region (ex: Connection Strings,Protel,... )
        /// </summary>
        public string Region { get; set; }

        /// <summary>
        /// Method's Lebel
        /// </summary>
        public string Lebel { get; set; }

        /// <summary>
        /// Method's Description
        /// </summary>
        public string Help { get; set; }

        /// <summary>
        /// Control's values (ex: Post,Get). If Values!="" then the control must be ComboBox 
        /// </summary>
        public string Values { get; set; }


        /// <summary>
        /// Control's position in the windows form
        /// </summary>
        public int Order { get; set; } = 0;

        /// <summary>
        /// Control's width
        /// </summary>
        public int Width { get; set; } = 200;

        /// <summary>
        /// Control's height (For textboxes only. if Height>20 then textbox must be multiline )
        /// </summary>
        public int Height { get; set; } = 20;
     
    }
}
