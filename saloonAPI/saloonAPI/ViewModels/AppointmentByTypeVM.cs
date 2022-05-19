using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace saloonAPI.ViewModels
{
    public class AppointmentByTypeVM
    {
        public AppoinmentType Type { get; set; }
        public string TypeText { get; set; }
        public int Count { get; set; }
    }
}
