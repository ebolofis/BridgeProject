using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hit.Services.DTOs.HitServices
{

    [Table("SqlParameters")]
    public class SqlParametersDTO
    {
        [Key]
        public int Id { get; set; }
        public string SettingsFile { get; set; }
        public string Parameter { get; set; }
        public string Value { get; set; }
        public string OldValue { get; set; }
    }
}
