using CoreTask2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoreTask2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class categoriesController : ControllerBase
    {

        private readonly MyDbContext _db;
        

        public categoriesController(MyDbContext db)
        {
            _db = db;
        }

        [HttpGet("getAllCategories")]
        public IActionResult GetAllCategories()
        {
            var categories = _db.Categories.ToList();
            return Ok(categories);
        }

        [HttpGet("GetCategoryById/{id:int:min(5)}")]
        public IActionResult GetCategoryById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid ID");

            var category = _db.Categories.Find(id);
            if (category == null)
                return NotFound("Category not found");

            return Ok(category);
        }

        [HttpGet("GetCategoryByName/{name}")]
        public IActionResult GetCategoryByName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return BadRequest("Name cannot be null or empty");

            var category = _db.Categories.FirstOrDefault(c => c.CategoryName == name);
            if (category == null)
                return NotFound("Category not found");

            return Ok(category);
        }

        [HttpDelete("DeleteCategory/{id:int:min(1)}")]
        public IActionResult DeleteCategory(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid ID");

            var category = _db.Categories.Find(id);
            if (category == null)
                return NotFound("Category not found");

            _db.Categories.Remove(category);
            _db.SaveChanges();
            return NoContent();
        }
    }
}

