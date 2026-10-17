using Asp.netcore_with_angular.Model;
using Microsoft.AspNetCore.Mvc;

namespace ProjectName.Controllers
{
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductRepository _productRepository;

        public ProductController(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet("api/Products")]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productRepository.GetAllProducts();
            return Ok(products);
        }

        [HttpPost("api/Product")]
        public async Task<IActionResult> AddProduct([FromBody] Product vm)
        {
            if (vm == null)
            {
                return BadRequest("Product body is required.");
            }

            if (string.IsNullOrWhiteSpace(vm.ProductName))
            {
                return BadRequest("ProductName is required.");
            }

            await _productRepository.SaveProduct(vm);
            return Ok(vm);
        }
        [HttpPut("api/Product/{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product vm)
        {
            await _productRepository.UpdateProduct(id, vm);
            return Ok(vm);
        }

        [HttpDelete("api/Product/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _productRepository.DeleteProduct(id);
            return Ok();
        }

    }
}
