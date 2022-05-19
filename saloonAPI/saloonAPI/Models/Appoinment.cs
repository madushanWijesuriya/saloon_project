using saloonAPI.Models.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace saloonAPI.Models
{
    public class Appoinment
    {
        public int Id { get; set; }
        public AppoinmentType Type { get; set; }
        public AppoinmentStatus Status { get; set; }
        public int DurationInMins { get; set; }
        public DateTime AppoinmentDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
