using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace saloonAPI.Models
{
    public class ProductImage
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string UniqueName { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
