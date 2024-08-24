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
        //[HttpGet]
        //public IActionResult getAllProducts()
        //{
        //    var products = _db.Products.Include(m=>m.Category).ToList();
        //    return Ok(products);
        //}
        //[HttpGet("{id}")]
        //public IActionResult GetProductById(int id)
        //{

        //    var product = _db.Products.Include(m => m.Category).FirstOrDefault(m => m.ProductId == id);
        //    //var product = _db.Products.Include(m => m.Category).Where(m => m.CategoryId ==id && m.Price >100 ).ToList();

        //    var productdet = new
        //    {
        //       ProdctID= product.ProductId,
        //       ProdctName= product.ProductName,
        //       Descriptions = product.Description,
        //       Prices = product.Price,
        //       ProductImag= product.ProductImage,
        //       ProductCategory= product.Category.Products,
        //    };


        //    return Ok(productdet);
        //}
        [HttpGet("Products/getAllProducts")]
        public IActionResult getAllProducts()
        {
            var Products = _db.Products.ToList();
            if (Products == null)
            { 
                return NotFound();
            }

            return Ok(Products);
        }

        [HttpGet("Products/GetProductById/{id:max(10)}")]

        public IActionResult GetProductById(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var Products = _db.Products.Find(id);
            if (Products == null)
            {
                return NotFound();
            }
            
            return Ok(Products);
        }

        [HttpGet("Products/GetProductByName/{name}")]
        public IActionResult GetProductByName(string name)
        {
            var Products = _db.Products.Where(malik => malik.ProductName == name);
            if (Products == null)
            {
                return NotFound();
            }
            return Ok(Products);
        }

        [HttpDelete("Products/DeletProductById/{id}")]
        public IActionResult DeleteProduct(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var Products = _db.Products.FirstOrDefault(malik => malik.ProductId == id);
           
            if (Products == null)
            {
                return NotFound();
            }
            else
            {
                _db.Products.Remove(Products);
            }
            _db.SaveChanges();
            return NoContent();
        }
    }
}
