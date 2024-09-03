using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WepAPICoreTasks1.DTOs;
using WepAPICoreTasks1.Models;

namespace WepAPICoreTasks1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartitemsController : ControllerBase
    {
        private readonly MyDbContext _db;

        public CartitemsController(MyDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        public IActionResult GetCartItems()
        {
            var cartItems = _db.CartItems.Where(m => m.CartId == 1).Select(x =>
            new CartItemsResponseDTO
            {
                CartItemId = x.CartItemId,
                CartId = x.CartId,
                Quantity = x.Quantity,
                ProductRsponseDTO = new ProductRsponseDTO
                {

                    ProductName = x.Product.ProductName,
                    ProductImage = x.Product.ProductImage,
                    Price = x.Product.Price,

                }
            });
            return Ok(cartItems);
        }
        [HttpPost]
        public IActionResult AddToCart([FromBody] AddtocartDTO addtocartDTO)
        {
            var data = new CartItem
            {
                CartId = addtocartDTO.CartId,
                ProductId = addtocartDTO.ProductId,
                Quantity = addtocartDTO.Quantity,
            };
            _db.CartItems.Add(data);
            _db.SaveChanges();
            return Ok(data);
        }
        [HttpPut("CartItem/UpdateItem/{id:int}")]
        public IActionResult UpdateCartItem(int id, [FromBody] UpdateCartItemDTO updateCartItemDTO)
        {
            var cartItem = _db.CartItems.FirstOrDefault(x => x.CartItemId == id);

            cartItem.Quantity = updateCartItemDTO.Quantity;

            _db.CartItems.Update(cartItem);
            _db.SaveChanges();
            return Ok(cartItem);
        }
        [HttpDelete("CartItem/DeletCartItemById/{id}")]
        public IActionResult DeletCartItemById(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var cartItem = _db.CartItems.FirstOrDefault(malik => malik.CartItemId == id);

            if (cartItem == null)
            {
                return NotFound();
            }
            else
            {
                _db.CartItems.Remove(cartItem);
            }
            _db.SaveChanges();
            return NoContent();
        }
        [HttpPost("getnumberrepeatodd")]
        public IActionResult single([FromBody] List<int> numbers)
        {


            Dictionary<int, int> numberCount = new Dictionary<int, int>();

            foreach (var number in numbers)
            {
                if (numberCount.ContainsKey(number))
                {
                    numberCount[number]++;
                }
                else
                {
                    numberCount[number] = 1;
                }
            }

            var singleOccurrenceNumbers = numberCount
                .Where(kvp => kvp.Value == 1)
                .Select(kvp => kvp.Key)
                .ToList();

            return Ok(singleOccurrenceNumbers);

        }
    }
}
