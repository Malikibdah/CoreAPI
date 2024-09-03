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
        private readonly ILogger<OrdersController> _logger;


        public OrdersController(MyDbContext db, ILogger<OrdersController> logger)
        {
            _db = db;
            _logger = logger;
        }
        [HttpGet("Orders/getAllOrders")]
        public IActionResult getAllOrders()
        {
            var Orders = _db.Orders.ToList();
            _logger.LogInformation("I am malik ,");
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
            var data = calc.Split(" ");
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
        [HttpGet("chicknumberstate")]
        public IActionResult chicknumberstate(int num1, int num2)
        {
            if (num1 == 30 || num2 == 30)
            {
                return Ok("the number state is true");
            }
            else if ((num1 + num2) == 30)
            {
                return Ok("the number state is true");
            }
            else
            {
                return Ok("the number state is false");
            }

        }
        [HttpGet("chichthenumber")]
        public IActionResult chichthenumber(int num1)
        {
            if (num1 % 3 == 0 || num1 % 7 == 0 && num1 > 0)
            {
                return Ok("true");
            }
            return Ok("false");
        }
    }
}
