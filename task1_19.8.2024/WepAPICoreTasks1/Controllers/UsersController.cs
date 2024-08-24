﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WepAPICoreTasks1.Models;

namespace WepAPICoreTasks1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly MyDbContext _db;

        public UsersController(MyDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        [Route("Users/getAllUsers")]
        public IActionResult getAllUsers()
        {
            var User = _db.Users.ToList();
            if (User == null) 
            { 
                return NotFound();
            }
            return Ok(User);
        }

        [HttpGet]
        [Route("Users/GetUserById/{id}")] //route attribute with minemam value 2 
        public IActionResult GetUserById(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var User = _db.Users.Find(id);
            if (User == null)
            { 
                return NotFound();
            }
            return Ok(User);
        }

        [HttpGet]
        [Route("Users/GetUserByName/{name}")] //route attribute with minemam value 2 
        public IActionResult GetUserByName(string name)
        {
            var User = _db.Users.Where(malik => malik.Username == name);
            if (User == null)
            {
                return NotFound();
            }
            return Ok(User);
        }

        [HttpDelete]
        [Route("Users/DeleteUserById/{id}")]
        public IActionResult DeleteUserById(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var User = _db.Users.Include(x => x.Orders).FirstOrDefault(malik => malik.UserId == id);
            if (User == null)
            {
                return NotFound();
            }
            if (User.Orders.Any())
            {
                return BadRequest("لا يمن الحذف لانه مشبك مع اكثر من طلب ");
            }
            _db.Users.Remove(User);
            _db.SaveChanges();
            return NoContent();
        }
    }
}
