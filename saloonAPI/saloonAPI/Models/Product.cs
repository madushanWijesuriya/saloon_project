using saloonAPI.Models.Authentication;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace saloonAPI.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required, MaxLength(100)]
        public string Name { get; set; }
        [Range(0, Double.MaxValue)]
        public double Price { get; set; }
        [Range(0, int.MaxValue)]
        public int Quentity { get; set; }
        [MaxLength(100)]
        public string Description { get; set; }
        public virtual ICollection<Like> Likes { get; set; } = new List<Like>();
        public virtual ICollection<ProductComment> Comments { get; set; } = new List<ProductComment>();
        public string CreatedUserId { get; set; }
        public virtual ApplicationUser CreatedUser { get; set; }
        public virtual ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();
    }
}
