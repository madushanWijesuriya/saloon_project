using saloonAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace saloonAPI.Services
{
    public class SqlServiceComment : ICommentRepository
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        public Comment GetComment(int commentId)
        {
            return _context.Comments.FirstOrDefault(c => c.Id == commentId);
        }

        public ProductComment SaveProductComment(ProductComment productComment)
        {
            _context.ProductComments.Add(productComment);
            _context.SaveChanges();

            return _context.ProductComments.Find(productComment.Id);
        }

        public void DeleteComment(Comment comment)
        {
            _context.Comments.Remove(comment);
            _context.SaveChanges();
        }

        public void DeleteProductComment(ProductComment productComment)
        {
            _context.ProductComments.Remove(productComment);
            _context.SaveChanges();
        }

        public Comment SaveComment(Comment comment)
        {
            _context.Comments.Add(comment);
            _context.SaveChanges();

            return _context.Comments.Find(comment.Id);
        }

        public void UpdateComment(Comment comment)
        {
            _context.SaveChanges();
        }

        public ProductComment GetProductComment(int commentId)
        {
            return _context.ProductComments.FirstOrDefault(pc => pc.CommentId == commentId);
        }
    }
}
