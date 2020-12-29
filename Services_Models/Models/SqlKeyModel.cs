using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hit.Services.Models.Models
{
    /// <summary>
    /// Class that keeps the required info to 'Open SYMMETRIC KEY'. 
    /// It also carries the list of encrypted columns.
    /// </summary>
    public class SqlKeyModel
    {
        /// <summary>
        /// name of symetric key
        /// </summary>
        public string SymmetricKey { get; set; }

        /// <summary>
        /// name of certificate
        /// </summary>
        public string Certificate { get; set; }

        /// <summary>
        /// password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// list of encrypted columns
        /// </summary>
        public List<string> EncryptedColumns { get; set; }
    }
}
