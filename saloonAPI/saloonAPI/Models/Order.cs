using saloonAPI.Models.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace saloonAPI.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CustomerId { get; set; }
        public virtual ApplicationUser Customer { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public double TotalAmount { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public string DeliveryAddress { get; set; }
        public string MobileNo { get; set; }
    }
}
