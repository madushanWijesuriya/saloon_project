using saloonAPI.Models.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace saloonAPI.Models
{
    public class ProductComment
    {
        public int Id { get; set; }
        public int CommentId { get; set; }
        public virtual Comment Comment { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
