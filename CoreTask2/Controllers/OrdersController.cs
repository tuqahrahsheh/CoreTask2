using CoreTask2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreTask2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {

        private readonly MyDbContext _db;

        public OrdersController(MyDbContext db)
        {
            _db = db;
        }

        [HttpGet("getAllOrders")]
        public IActionResult GetAllOrders()
        {
            var orders = _db.Orders.ToList();
            return Ok(orders);
        }

        [HttpGet("GetOrderById")]
        public IActionResult GetOrderById([FromQuery] int id)
        {
            if (id <= 0)
                return BadRequest("Invalid ID");

            var order = _db.Orders.Find(id);
            if (order == null)
                return NotFound("Order not found");

            return Ok(order);
        }

        [HttpGet("GetOrderByName")]
        public IActionResult GetOrderByName([FromQuery] string name)
        {
            if (string.IsNullOrEmpty(name))
                return BadRequest("Name cannot be null or empty");

            var order = _db.Orders.FirstOrDefault(o => o.OrderDate == date); 
            if (order == null)
                return NotFound("Order not found");

            return Ok(order);
        }

        [HttpDelete("DeleteOrder")]
        public IActionResult DeleteOrder([FromQuery] int id)
        {
            if (id <= 0)
                return BadRequest("Invalid ID");

            var order = _db.Orders.Find(id);
            if (order == null)
                return NotFound("Order not found");

            _db.Orders.Remove(order);
            _db.SaveChanges();
            return NoContent();
        }
    }
}

