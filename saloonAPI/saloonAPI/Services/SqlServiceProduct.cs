using saloonAPI.Models;
using saloonAPI.Models.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace saloonAPI.Services
{
    public class SqlServiceProduct : IProductRepository
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();
        
        public List<Product> GetAllProducts ()
        {
            return _context.Products.ToList();
        }

        public Product SaveProduct(Product product)
        {
            _context.Add(product);
            _context.SaveChanges();

            return _context.Products.Find(product.Id);
        }

        public Product GetProduct(int productId)
        {
            return _context.Products.FirstOrDefault(p => p.Id == productId);
        }

        public void UpdateProduct(Product product)
        {
            _context.SaveChanges();
        }

        public void DeleteProduct(Product product)
        {
            _context.Remove(product);
            _context.SaveChanges();
        }
    }
}
