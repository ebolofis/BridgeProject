using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hit.Services.Models.Models
{
    /// <summary>
    /// Model describing a user's login 
    /// </summary>
    public class LoginModel
    {

        [MaxLength(50)]
        [MinLength(3)]
        [Required]
        public string Username { get; set; }

        [MaxLength(50)]
        [MinLength(5)]
        [Required]
        public string Password { get; set; }


        /// <summary>
        /// List of controllers (comma separated) user has access
        /// </summary>
        public string AccessTo { get; set; }


        [MaxLength(250)]
        public string Notes { get; set; }

        public LoginModel Clone()
        {
            return (LoginModel)this.MemberwiseClone();
        }
    }
}
