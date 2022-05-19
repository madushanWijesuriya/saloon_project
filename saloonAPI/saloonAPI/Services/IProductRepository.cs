using saloonAPI.Models;
using saloonAPI.Models.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace saloonAPI.Services
{
    public interface IProductRepository
    {
        public List<Product> GetAllProducts();
        public Product SaveProduct(Product product);
        public Product GetProduct(int ProductId);
        public void UpdateProduct(Product product);
        public void DeleteProduct(Product product);
    }
}
