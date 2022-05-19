using saloonAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace saloonAPI.Services
{
    public class SqlServiceLike : ILikeRepository
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();
        public void DeleteLike(Like like)
        {
            _context.Likes.Remove(like);
            _context.SaveChanges();
        }

        public Like SaveLike(Like like)
        {
            _context.Likes.Add(like);
            _context.SaveChanges();

            return _context.Likes.Find(like.Id);
        }

        public Like GetLike(int likeId)
        {
            return _context.Likes.FirstOrDefault(l => l.Id == likeId);
        }

        public Like GetLikeByProductAndUser(int productId, string userId)
        {
            return _context.Likes.FirstOrDefault(l => l.ProductId == productId && l.UserId == userId);
        }
    }
}
