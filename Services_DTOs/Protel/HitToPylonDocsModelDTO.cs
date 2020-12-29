using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hit.Services.DTOs.Protel
{
    [Table("hit_to_pylon_docs")]
    public class HitToPylonDocsModelDTO
    {
        public int rechnr { get; set; } //(int, not null)
        public int fisccode { get; set; } //(int, not null)
        public int mpehotel { get; set; } //(int, not null)
        public int is_sent { get; set; } //(int, not null)
        public DateTime date_created { get; set; } //(datetime, not null)
    }
}
