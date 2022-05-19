using saloonAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace saloonAPI.Services
{
    public interface ILikeRepository
    {
        public Like SaveLike(Like like);
        public void DeleteLike(Like like);
        public Like GetLike(int likeId);
        public Like GetLikeByProductAndUser(int productId, string userId);
    }
}
