using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CinemaAPI.Data;
using CinemaAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CinemaAPI.Controllers
{
    
    [Route("api/[controller]/[action]")]
    public class UsersController : Controller
    {
        private CinemaDBContext _dbContext;

        public UsersController(CinemaDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public IActionResult Register([FromBody] User user)
        {
            var userWithSameEmail=_dbContext.Users.Where(u => u.Email == user.Email).SingleOrDefault();
            if (userWithSameEmail != null) {
                return BadRequest("User with same email already exists");
            }

            var userObj = new User {
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                Role = "Users"
            };

            _dbContext.Users.Add(userObj);
            _dbContext.SaveChanges();

            return StatusCode(StatusCodes.Status201Created);
        }
    }
}
