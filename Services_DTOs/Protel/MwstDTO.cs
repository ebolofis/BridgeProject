using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hit.Services.DTOs.Protel
{
    [Table("mwst")]
    public class MwstDTO
    {
        public decimal satz { get; set; } //(decimal(10,5), not null)
        public int satznr { get; set; } //(int, not null)
        public int kennz { get; set; } //(int, not null)
        public string kennz2 { get; set; } //(varchar(50), not null)
        public string account { get; set; } //(varchar(15), not null)
        public int visbi { get; set; } //(int, null)
    }

}
