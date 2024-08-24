using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            var Orders = _db.Orders.ToList();
            return Ok(Orders);
        }

        [HttpGet("Orders/GetOrderById/{id}")]

        public IActionResult GetOrderById(int id)
        {
            var Orders = _db.Orders.Find(id);
            return Ok(Orders);
        }

        //[HttpGet("Orders/GetOrderByName/{name}")]
        //public IActionResult GetOrderByName(string name)
        //{
        //    var Orders = _db.Orders.Where(malik => malik. == name);
        //    return Ok(Orders);
        //}

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
