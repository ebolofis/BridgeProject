using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hit.Services.Models.Enums
{
    /// <summary>
    /// specifies the type of a main courant payment
    /// </summary>
    public enum payment_kind {
        payment = 0,

        advance_payment = 1,

        paidout = 2,

        advance = 3
    }
}
