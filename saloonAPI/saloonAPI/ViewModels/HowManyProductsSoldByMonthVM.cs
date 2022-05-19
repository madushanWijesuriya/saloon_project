using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace saloonAPI.ViewModels
{
    public class HowManyProductsSoldByMonthVM
    {
        public int ProductsSoldCount { get; set; }
        public int Month { get; set; }
        public string MonthText { get; set; }
        public double ProductsSoldAmount { get; set; }
    }
}
