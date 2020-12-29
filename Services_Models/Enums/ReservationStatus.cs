using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hit.Services.Models.Enums
{
    /// <summary>
    /// Reservation Statuses (based on buch.buchstatus)
    /// </summary>
    public enum ReservationStatus
    {
        Reservation=0,
        CheckedIn=1,
        CheckedOut=2
    }
}
