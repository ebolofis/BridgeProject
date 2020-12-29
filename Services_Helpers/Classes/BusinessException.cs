using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hit.Services.Helpers.Classes
{
    public class BusinessException : Exception
    {
        public string ErrorCode { get; set; } = "";

        public BusinessException(string message) : base(message)
        {

        }

        public BusinessException(string message, string errorCode) : base(message)
        {
            this.ErrorCode = errorCode;
        }
    }
}
