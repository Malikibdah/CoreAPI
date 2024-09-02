using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WepAPICoreTasks1.DTOs;
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
        [HttpPost]
        public IActionResult AddUser([FromForm] UserDTO userDTO)
        {

            var data = new User
            {
                Username = userDTO.Username,
                Password = userDTO.Password,
                Email = userDTO.Email,

            };

            _db.Users.Add(data);
            _db.SaveChanges();
            return Ok();
        }
        [HttpPut("UpdateUserById/{id:int}")]
        public IActionResult UpdateUserById(int id, [FromForm] UserDTO userDTO)
        {
            var user = _db.Users.FirstOrDefault(x => x.UserId == id);

            user.Username = userDTO.Username;
            user.Password = userDTO.Password;
            user.Email = userDTO.Email;

            _db.Users.Update(user);
            _db.SaveChanges();
            return Ok();
        }
        [HttpPost("Register")]
        public IActionResult RegestUser([FromForm] UserDTO userDTO)
        {
            byte[] passwordHash, passwordSalt;

            PasswordHasher.CreatePasswordHash(userDTO.Password , out passwordHash , out passwordSalt);
            var user = new User
            {
                Username = userDTO.Username,
                Password = userDTO.Password,
                Email = userDTO.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt

            };
            _db.Users.Add(user);
            _db.SaveChanges();
            return Ok(user);
        }
        [HttpPost("login")]
        public IActionResult Login([FromForm] UserDTO userDTO)
        {
            var user = _db.Users.FirstOrDefault(x => x.Email == userDTO.Email);
            if (user == null || !PasswordHasher.VerifyPasswordHash(userDTO.Password, user.PasswordHash, user.PasswordSalt))
            {
                return Unauthorized("Invalid username or password."); ;
                    
            };
            return Ok(user);
        }
        [HttpGet("UserInfo/{email}")]
        public IActionResult UserInfo(string email)
        {
            var user = _db.Users.FirstOrDefault(x => x.Email == email);
            if (user == null)
            {
                return NotFound();
            }
            var userinfo = new UserInfo 
            { 
                Email = user.Email,
                Username = user.Username,
                UserId = user.UserId
            };
            return Ok(userinfo);
        }       
    }
}
