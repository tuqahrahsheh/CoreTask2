using CoreTask2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreTask2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly MyDbContext _db;

        public UsersController(MyDbContext db)
        {
            _db = db;
        }

        [HttpGet("getAllUsers")]
        public IActionResult GetAllUsers()
        {
            var users = _db.Users.ToList();
            return Ok(users);
        }

        [HttpGet("GetUserById/{id:int:min(1)}")]
        public IActionResult GetUserById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid ID");

            var user = _db.Users.Find(id);
            if (user == null)
                return NotFound("User not found");

            return Ok(user);
        }

        [HttpGet("GetUserByName/{name}")]
        public IActionResult GetUserByName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return BadRequest("Name cannot be null or empty");

            var user = _db.Users.FirstOrDefault(u => u.Username == name);
            if (user == null)
                return NotFound("User not found");

            return Ok(user);
        }

        [HttpDelete("DeleteUser/{id:int:min(1)}")]
        public IActionResult DeleteUser(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid ID");

            var user = _db.Users.Find(id);
            if (user == null)
                return NotFound("User not found");

            _db.Users.Remove(user);
            _db.SaveChanges();
            return NoContent();
        }
    }
}
