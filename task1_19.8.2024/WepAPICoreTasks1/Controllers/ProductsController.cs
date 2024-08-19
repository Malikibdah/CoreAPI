using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WepAPICoreTasks1.Models;

namespace WepAPICoreTasks1.Controllers
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
        [HttpGet]
        public IActionResult getAllProducts()
        {
            var products = _db.Products.Include(m=>m.Category).ToList();
            return Ok(products);
        }
        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            var product = _db.Products.Include(m=>m.Category).FirstOrDefault(m=>m.ProductId==id);
            return Ok(product);
        }
    }
}
