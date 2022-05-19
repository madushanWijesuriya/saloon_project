using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace saloonAPI.ViewModels
{
    public class HowManyUsersPerMonth
    {
        public int UsersCount { get; set; }
        public int Month { get; set; }
        public string MonthText { get; set; }
    }
}
