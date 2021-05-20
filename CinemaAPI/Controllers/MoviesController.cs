using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CinemaAPI.Data;
using CinemaAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860



namespace CinemaAPI.Controllers
{
    [Route("api/[controller]")]
    public class MoviesController : Controller
    {
        private CinemaDBContext _dbContext;

        public MoviesController(CinemaDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        // POST api/values
        [Authorize(Roles ="Admin")]
        [HttpPost]
        public IActionResult Post([FromForm] Movie movieObj)
        {
            var guid = Guid.NewGuid();
            var filePath = Path.Combine("wwwroot", guid + ".jpg");

            if (movieObj.Image != null)
            {

                var fileStream = new FileStream(filePath, FileMode.Create);
                movieObj.Image.CopyTo(fileStream);
            }

            movieObj.ImageUrl = filePath.Remove(0, 7);
            _dbContext.Movies.Add(movieObj);
            _dbContext.SaveChanges();

            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT api/values/5
        [Authorize(Roles ="admin")]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromForm] Movie movieObj)
        {
            var movie = _dbContext.Movies.Find(id);

            if (movie == null)
            {
                return NotFound("No record found against this id");
            }
            else
            {
                var guid = Guid.NewGuid();
                var filePath = Path.Combine("wwwroot", guid + ".jpg");

                if (movieObj.Image != null)
                {

                    var fileStream = new FileStream(filePath, FileMode.Create);
                    movieObj.Image.CopyTo(fileStream);
                    movie.ImageUrl = filePath.Remove(0, 7);

                }

                movie.Name = movieObj.Name;
                movie.Language = movieObj.Language;
                movie.Rating = movieObj.Rating;
                _dbContext.SaveChanges();
                return Ok("Record updated succefully");
            }
        }



    }
}
