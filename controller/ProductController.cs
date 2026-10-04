using Asp.netcore_with_angular.Model;
using Microsoft.AspNetCore.Mvc;

namespace ProjectName.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductRepository _productRepository;
        public ProductController(ProductRepository productRepository)
        {
            this._productRepository = productRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productRepository.GetAllProducts();
            return Ok(products);
        }
        [HttpPost]
        public async Task<IActionResult> AddProduct(Product vm)
        {
            await _productRepository.SaveProduct(vm);
            return Ok(vm);
        }

    }
}