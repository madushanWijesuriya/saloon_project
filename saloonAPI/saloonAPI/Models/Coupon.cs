using saloonAPI.Models.Authentication;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace saloonAPI.Models
{
    public class Coupon
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ExpireDate { get; set; }
        [Range(0, int.MaxValue)]
        public int Points { get; set; }
        public string CreatedUserId { get; set; }
        public virtual ApplicationUser CreatedUser { get; set; }
    }
}
