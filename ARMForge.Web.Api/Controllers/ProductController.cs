using ARMForge.Business.Interfaces;
using ARMForge.Kernel.Entities;
using ARMForge.Types.DTOs;
using ARMForge.Types.Mapping;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ARMForge.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            var productDtos = products.Select(p => ProductMapper.ToDto(p));
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null) return NotFound();
            return Ok(ProductMapper.ToDto(product));
        }

        [HttpPost]
        public async Task<ActionResult<ProductDto>> AddProduct([FromBody] ProductCreateDto productDto)
        {
            var product = ProductMapper.ToEntity(productDto);
            var addedProduct = await _productService.AddProductAsync(product);
            return CreatedAtAction(nameof(GetProduct), new { id = addedProduct.Id }, ProductMapper.ToDto(addedProduct));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductUpdateDto productDto)
        {
            if (id != productDto.Id) return BadRequest();

            var existingProduct = await _productService.GetProductByIdAsync(id);
            if (existingProduct == null) return NotFound();

            ProductMapper.ToEntity(productDto, existingProduct);
            var updatedProduct = await _productService.UpdateProductAsync(existingProduct);
            if (updatedProduct == null) return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await _productService.DeleteProductAsync(id);
            if (!result) return NotFound();

            return NoContent();
        }
    }
}
