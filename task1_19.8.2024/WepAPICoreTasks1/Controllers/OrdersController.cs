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
        
        [HttpGet("calculater")]
        public IActionResult calculater(string calc)
        { 
            var data =  calc.Split(" ");
            var numb1 = Convert.ToInt32(data[0]);
            var numb2 = Convert.ToInt32(data[2]);
            var operation = data[1];
            if (operation == "-")
            {
                var result = numb1 - numb2;
                return Ok(result);
            }
            else if (operation == "+")
            {
                var result = numb2 + numb1;
                return Ok(result);
            }
            else if (operation == "*")
            {
                var result = numb1 * numb2;
                return Ok(result);
            }
            else if (operation == "/")
            {
                if (numb2 == 0)
                {
                    return BadRequest(" ولك الوووووووووو لا يمكن القسمة على صفر يا غالي صحصح");
                }
                var result = numb1 / numb2;
                return Ok(result);
            }

                return NoContent();
        }

    }
}
