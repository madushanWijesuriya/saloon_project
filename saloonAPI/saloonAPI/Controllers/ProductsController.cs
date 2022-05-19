using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using saloonAPI.Models;
using saloonAPI.Services;
using saloonAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;

namespace saloonAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _sqlService;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductsController(IProductRepository dataAccessRepository, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _sqlService = dataAccessRepository;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public ActionResult<IList<ProductDto>> GetAllProducts()
        {
            List<Product> products = _sqlService.GetAllProducts();
            var vmProducts = _mapper.Map<List<ProductDto>>(products);
            return Ok(vmProducts);
        }

        [HttpPost, Authorize(Roles = "Admin")]
        public ActionResult<Product> AddNewProduct([FromForm] AddProduct product)
        {
            if (!User.Identity.IsAuthenticated)
                throw new AuthenticationException();
            var userId = User.Claims.FirstOrDefault(c => c.Type == "userId");
            product.CreatedUserId = userId.Value;

            List<(string fileName, string fullUrl)> fileNames = UploadFiles(product.ProductImages);

            var mappedProduct = _mapper.Map<Product>(product);

            Product newProduct = _sqlService.SaveProduct(mappedProduct);

            if (fileNames is not null && fileNames.Count > 0)
            {
                foreach (var (fileName, fullUrl) in fileNames)
                {
                    mappedProduct.ProductImages.Add(new ProductImage
                    {
                        Url = fullUrl,
                        UniqueName = fileName,
                        CreatedDate = DateTime.Now,
                        ProductId = newProduct.Id
                    });
                }
            }

            _sqlService.UpdateProduct(newProduct);

            var ProductToReturn = _mapper.Map<ProductDto>(newProduct);

            return Created("Product", ProductToReturn);
        }

        private List<(string fileName, string fullUrl)> UploadFiles(List<IFormFile> imagesList)
        {
            if (imagesList is null || imagesList.Count == 0)
            {
                return new List<(string, string)>();
            }

            List<(string fileName, string fullUrl)> uniqueFileNames = new();

            foreach (IFormFile image in imagesList)
            {
                string uniqueFileName = null;

                string uploadsFolder = Path.Combine(_webHostEnvironment.ContentRootPath, "Images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + image.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    image.CopyTo(fileStream);
                }
                uniqueFileNames.Add((fileName: uniqueFileName, fullUrl: filePath));
            }

            return uniqueFileNames;
        }

        [HttpPut("{productId}"), Authorize(Roles = "Admin")]
        public ActionResult UpdateProduct(int productId, EditProduct product)
        {
            Product updatingProduct = _sqlService.GetProduct(productId);

            if (updatingProduct is null)
            {
                return NotFound();
            }

            _mapper.Map(product, updatingProduct);

            _sqlService.UpdateProduct(updatingProduct);

            return NoContent();
        }

        [HttpDelete("{productId}")]
        public ActionResult DeleteProduct(int productId)
        {
            Product deletingProduct = _sqlService.GetProduct(productId);

            if (deletingProduct is null)
            {
                return NotFound();
            }

            _sqlService.DeleteProduct(deletingProduct);

            return NoContent();
        }
    }
}
