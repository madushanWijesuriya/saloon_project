using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace saloonAPI.ViewModels
{
    public class EditProduct
    {
        [Required, MaxLength(100)]
        public string Name { get; set; }
        [Range(0, Double.MaxValue), Required]
        public double Price { get; set; }
        [Range(0, int.MaxValue), Required]
        public int Quentity { get; set; }
        [MaxLength(100)]
        public string Description { get; set; }
        public string CreatedUserId { get; set; }
    }
}
