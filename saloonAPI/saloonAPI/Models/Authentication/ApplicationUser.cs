using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace saloonAPI.Models.Authentication
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime JoinDate { get; set; }
        public virtual ICollection<CustomerTelNumber> TelNumbers { get; set; } = new List<CustomerTelNumber>();
        public virtual ICollection<Appoinment> Appoinments { get; set; } = new List<Appoinment>();
        public virtual ICollection<Like> Likes { get; set; } = new List<Like>();
        public virtual ICollection<ProductComment> ProductComments { get; set; } = new List<ProductComment>();
        public virtual ICollection<UserProduct> UserProducts { get; set; } = new List<UserProduct>();
        public virtual ICollection<UserCoupon> UserCoupons { get; set; } = new List<UserCoupon>();
        public int SettingId { get; set; }
        public virtual CustomerSetting Settings { get; set; }
    }
}
