using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hit.Services.DTOs.Protel
{
    [Table("rechhist")]
    public class RechhistDTO
    {
        [Column("ref")]
        public int Ref { get; set; } //(int, not null)
        public int mpehotel { get; set; } //(int, not null)
        public int rechnr { get; set; } //(int, not null)
        public int resno { get; set; } //(int, not null)
        public int invindex { get; set; } //(int, not null)
        public int voidinv { get; set; } //(int, not null)
        public int fisccode { get; set; } //(int, not null)
        public int kdnr { get; set; } //(int, not null)
        public int formnr { get; set; } //(int, not null)
        public int archive { get; set; } //(int, not null)
        [Column("void")]
        public int Void { get; set; } //(int, not null)
        public int crystal { get; set; } //(int, not null)
        public int deposit { get; set; } //(int, not null)
        public int autogen { get; set; } //(int, not null)
        public int copy { get; set; } //(int, not null)
        public string name { get; set; } //(varchar(80), not null)
        public string username { get; set; } //(varchar(80), not null)
        public DateTime datum { get; set; } //(datetime, not null)
        public decimal sum_zahl { get; set; } //(decimal(19,2), not null)
        public decimal sum_belast { get; set; } //(decimal(19,2), not null)
    }
}
