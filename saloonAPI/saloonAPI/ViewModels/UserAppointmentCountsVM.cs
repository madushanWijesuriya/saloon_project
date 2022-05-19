using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace saloonAPI.ViewModels
{
    public class UserAppointmentCountsVM
    {
        public string Email { get; set; }
        public int AppointmentCount { get; set; }
        public DateTime JoinDate { get; set; }
        public string JoinDateFormatted { get; set; }
    }
}
