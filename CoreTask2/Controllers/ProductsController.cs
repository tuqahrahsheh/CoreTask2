using CoreTask2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreTask2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly MyDbContext _db;

        public ProductsController(MyDbContext db)
        {
            _db = db;
        }

        [HttpGet("getAllProducts")]
        public IActionResult GetAllProducts()
        {
            var products = _db.Products.ToList();
            return Ok(products);
        }

        [HttpGet("GetProductById")]
        public IActionResult GetProductById([FromQuery] int id)
        {
            if (id <= 0)
                return BadRequest("Invalid ID");

            var product = _db.Products.Find(id);
            if (product == null)
                return NotFound("Product not found");

            return Ok(product);
        }

        [HttpGet("GetProductByName")]
        public IActionResult GetProductByName([FromQuery] string name)
        {
            if (string.IsNullOrEmpty(name))
                return BadRequest("Name cannot be null or empty");

            var product = _db.Products.FirstOrDefault(p => p.ProductName == name);
            if (product == null)
                return NotFound("Product not found");

            return Ok(product);
        }

        [HttpDelete("DeleteProduct")]
        public IActionResult DeleteProduct([FromQuery] int id)
        {
            if (id <= 0)
                return BadRequest("Invalid ID");

            var product = _db.Products.Find(id);
            if (product == null)
                return NotFound("Product not found");

            _db.Products.Remove(product);
            _db.SaveChanges();
            return NoContent();
        }
    }
}