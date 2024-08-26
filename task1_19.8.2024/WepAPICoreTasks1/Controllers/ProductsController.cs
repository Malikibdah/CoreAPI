using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WepAPICoreTasks1.DTOs;
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
        [HttpGet("Products/GetProductByCategoryId/{id}")]
        public IActionResult GetProductByCategoryId(int id)
        {
            var Products = _db.Products.Where(malik => malik.CategoryId == id).ToList();
            if (Products == null)
            {
                return NotFound();
            }
            return Ok(Products);
        }
        [HttpGet("Products/SortProducs")]
        public IActionResult SortProducs()
        {
            var Products = _db.Products.OrderByDescending(x=>x.Price).ToList();
            if (Products == null)
            {
                return NotFound();
            }
            return Ok(Products);
        }
        [HttpPost]
        public IActionResult AddProduct([FromForm] ProductDTOcs productDTO)
        {


            var uploadImageFolder = Path.Combine(Directory.GetCurrentDirectory(), "Image");
            if (!Directory.Exists(uploadImageFolder))
            {
                Directory.CreateDirectory(uploadImageFolder);
            }
            var imageFile = Path.Combine(uploadImageFolder, productDTO.ProductImage.FileName);
            using (var stream = new FileStream(imageFile, FileMode.Create))
            {
                productDTO.ProductImage.CopyToAsync(stream);
            }

            var data = new Product
            {
                ProductName = productDTO.ProductName,
                ProductImage = productDTO.ProductImage.FileName,
                Price = productDTO.Price,
                CategoryId = productDTO.CategoryId,
                Description = productDTO.Description,
            };

            _db.Products.Add(data);
            _db.SaveChanges();
            return Ok();
        }

        [HttpPut("UpdateProductById/{id:int}")]
        public IActionResult UpdateProductById(int id ,[FromForm] ProductDTOcs productDTO)
        {

            if(productDTO.ProductImage != null) {
            var uploadImageFolder = Path.Combine(Directory.GetCurrentDirectory(), "Image");
            if (!Directory.Exists(uploadImageFolder))
            {
                Directory.CreateDirectory(uploadImageFolder);
            }
            var imageFile = Path.Combine(uploadImageFolder, productDTO.ProductImage.FileName);
            using (var stream = new FileStream(imageFile, FileMode.Create))
            {
                productDTO.ProductImage.CopyToAsync(stream);
            }
            }
            var product = _db.Products.FirstOrDefault(x => x.ProductId == id);

            product.ProductName = productDTO.ProductName;
            product.ProductImage = productDTO.ProductImage.FileName;
            product.Price = productDTO.Price;
            product.CategoryId = productDTO.CategoryId;
            product.Description = productDTO.Description;
            

            _db.Products.Update(product);
            _db.SaveChanges();
            return Ok();
        }

    }
}
