using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hit.Services.Models
{
    /// <summary>
    /// Custon Annotation to declare active fields for Jobs
    /// Ex
    /// FieldsPerJobAnnotation("ProtelDB,WebPosDB,mpeHotel,proteluser")
    /// each field seperated by comma (,).
    /// </summary>
    [AttributeUsage(AttributeTargets.Class |AttributeTargets.Struct)]
    public class FieldsPerJobAnnotation: Attribute
    {
        private string ActiveFields;

        public FieldsPerJobAnnotation(string ActiveFields)
        {
            this.ActiveFields = ActiveFields;
        }
    }
}
