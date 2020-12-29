using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Hit.Services.Models.CustomAnotations;

namespace Hit.Services.Models.Models
{
  
  


    public class EmailSendModel
    {
        [EmailAddress]
        /// <summary>
        /// From email address
        /// </summary>
        public string From { get; set; }


        /// <summary>
        /// List of email addresses to send email
        /// </summary>
        [Required]
        [EnsureOneElement(ErrorMessageResourceType = typeof(Hit.Services.Resources.Errors), ErrorMessageResourceName = "TOEMAILISREQUIRED")]
        public List<string> To { get; set; }

        /// <summary>
        /// Email subject
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Email body
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// true: on email failure throw exception
        /// </summary>
        public bool ThrowException { get; set; } = true;
    }

}
