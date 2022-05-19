using saloonAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace saloonAPI.Services
{
    public interface ICommentRepository
    {
        public Comment SaveComment(Comment comment);
        public void UpdateComment(Comment comment);
        public void DeleteComment(Comment comment);
        public Comment GetComment(int commentId);

        public ProductComment GetProductComment(int commentId);
        public ProductComment SaveProductComment(ProductComment productComment);
        public void DeleteProductComment(ProductComment productComment);
    }
}
