using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hit.Services.Models.Models.ProtelMappingsToPylon
{
    public class CurrencyModel
    {
        public int Ref { get; set; } //(int, not null)
        public string Name { get; set; } //(decimal(10,5), not null)
    }
}
