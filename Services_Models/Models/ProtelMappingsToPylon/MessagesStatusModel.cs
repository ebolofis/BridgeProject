using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hit.Services.Models.Models.ProtelMappingsToPylon
{
   
    public class eaMessagesStatusModel
    {
        public int rechnr { get; set; }
        public DateTime datum { get; set; }
        public int fisccode { get; set; } //fiscalcd.ref
        public int   status { get; set; }
        public string url { get; set; }
     public MatchingKindEnum? Kind { get; set; }
    }
}
