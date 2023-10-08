using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetCore5WebAPI.Data;
using NetCore5WebAPI.Models;
using NetCore5WebAPI.Repositories;

namespace NetCore5WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                return Ok(await _productRepository.GetAllAsync());
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductModel model)
        {
            if (string.IsNullOrEmpty(model.TenHH))
            {
                return BadRequest(new
                {
                    title = "Bad Request",
                    status = 400,
                    message = "TenHH is not empty"
                });
            }

            try
            {
                var maHH = await _productRepository.CreateAsync(model);
                //return CreatedAtAction("GetProductById", new { id = maHH }, model);
                //return await GetProductById(maHH.ToString());
                var product = await _productRepository.GetByIdAsync(maHH);
                return StatusCode(StatusCodes.Status201Created, product);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(string id)
        {
            try
            {
                var product = await _productRepository.GetByIdAsync(Guid.Parse(id));
                if(product != null)
                {
                    return Ok(product);
                }
                return NotFound();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(string id, ProductVM productVM)
        {
            if(id != productVM.MaHH.ToString())
            {
                return BadRequest();
            }

            if (string.IsNullOrEmpty(productVM.TenHH))
            {
                return BadRequest(new
                {
                    title = "Bad Request",
                    status = 400,
                    message = "TenHH is not empty."
                });
            }

            try
            {
                var maHH = await _productRepository.UpdateAsync(productVM);
                if (!maHH.Equals(Guid.Empty))
                {
                    var product = await _productRepository.GetByIdAsync(maHH);
                    return Ok(product);
                }
                return NotFound();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            try
            {
                var isDeleted = await _productRepository.DeleteAsync(Guid.Parse(id));
                if (isDeleted)
                {
                    return NoContent();
                }
                else
                {
                    return NotFound();
                }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
