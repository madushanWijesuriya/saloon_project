using saloonAPI.Models.Authentication;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace saloonAPI.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? EditedDate { get; set; }
        [Required, MaxLength(150)]
        public string CommentText { get; set; }
    }
}
