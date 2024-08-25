using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WepAPICoreTasks1.Models;

namespace WepAPICoreTasks1.Controllers
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
        [HttpGet("Orders/getAllOrders")]
        public IActionResult getAllOrders()
        {
            var Orders = _db.Orders.Include(m => m.User);
            return Ok(Orders);
        }

        [HttpGet("Orders/GetOrderById/{id}")]

        public IActionResult GetOrderById(int id)
        {
            var Orders = _db.Orders.Find(id);
            return Ok(Orders);
        }

        [HttpGet("Orders/GetOrderByDate/{date}")]
        public IActionResult GetOrderByDate(string date)
        {
            var Orders = _db.Orders.Where(malik => malik.OrderDate == date).ToList();
            return Ok(Orders);
        }

        [HttpDelete("Orders/DeleteOrderById/{id}")]
        public IActionResult DeleteOrder(int id)
        {
            var Orders = _db.Orders.FirstOrDefault(malik => malik.OrderId == id);
            if (Orders == null)
            {
                return NotFound();
            }
            else
            {
                _db.Orders.Remove(Orders);
            }
            _db.SaveChanges();
            return NoContent();
        }

    }
}
