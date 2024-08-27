using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WepAPICoreTasks1.DTOs;
using WepAPICoreTasks1.Models;

namespace WepAPICoreTasks1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly MyDbContext _db;

        public CategoriesController(MyDbContext db)
        {
            _db = db;
        }
        //[HttpGet]
        //public IActionResult getAllCategories()
        //{
        //    var categories = _db.Categories.ToList();  
        //    return Ok(categories);
        //}
        //[HttpGet("{id}")]
        //public IActionResult GetCategoryById(int id )
        //{
        //    var category = _db.Categories.Where(malik => malik.CategoryId==id);
        //    return Ok(category);
        //}
        [HttpGet]
        [Route("Categorys/getAllCategories")]
        public IActionResult getAllCategories()
        {
            var categories = _db.Categories.ToList();
            if(categories==null)
            {
                return NotFound();
            }
            return Ok(categories);
        }
        
        [HttpGet]
        [Route("Categorys/GetCategoryById/{id:int:min(2)}")] //route attribute with minemam value 2 
        public IActionResult GetCategoryById(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var categories = _db.Categories.Find(id);
            if (categories == null) 
            { 
                return NotFound();
            }
            return Ok(categories);
        }

        [HttpGet]
        [Route("Categorys/GetCategoryById/{name}")] //route attribute with minemam value 2 
        public IActionResult GetCategoryByName(string name)
        {
            var category = _db.Categories.Where(malik => malik.CategoryName == name);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpDelete]
        [Route("Categorys/DeletCategoryById/{id}")]
        public IActionResult DeleteCategory(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var category = _db.Categories.Include(x=>x.Products).FirstOrDefault(malik => malik.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }
            if (category.Products.Any())
            {
                return BadRequest("لا يمن الحذف لانه مشبك مع اكثر من منتج ");
            }
            _db.Categories.Remove(category);
            _db.SaveChanges();
            return NoContent();
        }

        [HttpPost("AddCaegory")]
        public IActionResult AddCategory([FromForm] CategoryDTO categoryDTO)
        {
            var uploadFile = Path.Combine(Directory.GetCurrentDirectory(), "Image");
            if (!Directory.Exists(uploadFile))
            {
                Directory.CreateDirectory(uploadFile);
            }
            var imgFile = Path.Combine(uploadFile,categoryDTO.CategoriesImage.FileName);
            using (var steam = new FileStream(imgFile,FileMode.Create))
            {
                categoryDTO.CategoriesImage.CopyTo(steam);
            }
            
                var newCategory = new Category()
                {
                    CategoryName = categoryDTO.CategoryName,
                    CategoryImage = categoryDTO.CategoriesImage.FileName,
                };
            _db.Categories.Add(newCategory);
            _db.SaveChanges();
            return Ok();
        }
        [HttpPut("updateCategory/{id:int}")]
        public IActionResult updateCategory(int id , [FromForm] CategoryDTO categoryDTO)
        {
            if (categoryDTO.CategoriesImage != null)
            {
                var uploadFile = Path.Combine(Directory.GetCurrentDirectory(), "Image");
                if (!Directory.Exists(uploadFile))
                {
                    Directory.CreateDirectory(uploadFile);
                }
                var imgFile = Path.Combine(uploadFile, categoryDTO.CategoriesImage.FileName);
                using (var steam = new FileStream(imgFile, FileMode.Create))
                {
                    categoryDTO.CategoriesImage.CopyTo(steam);
                }

            }
            var c = _db.Categories.FirstOrDefault(m => m.CategoryId == id);
            c.CategoryName = categoryDTO.CategoryName ?? c.CategoryName;
            c.CategoryImage = categoryDTO.CategoriesImage.FileName ?? c.CategoryImage;

            _db.Categories.Update(c);
            _db.SaveChanges();

            return Ok(); 
        }



    }
}
