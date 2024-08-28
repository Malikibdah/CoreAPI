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
            var cartItems = _db.CartItems.Select(x =>
            new CartItemsResponseDTO
            {

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
        public IActionResult AddToCart([FromBody]AddtocartDTO addtocartDTO)
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
    }
}
