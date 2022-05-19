using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace saloonAPI.ViewModels
{
    public class AppointmentAmountsByYearVM
    {
        public double Amount { get; set; }
        public int Month { get; set; }
        public string MonthText { get; set; }
        public int Count { get; set; }
    }
}
