using saloonAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace saloonAPI.ViewModels
{
    public class OrderCreateVM
    {
        public List<Product> Products { get; set; }
        public string DeliveryAddress { get; set; }
        public string MobileNo { get; set; }
    }
}
