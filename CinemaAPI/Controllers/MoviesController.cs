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
        [Authorize(Roles = "Admin")]
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
                movie.Description = movieObj.Description;
                movie.Language = movieObj.Language;
                movie.Duration = movieObj.Duration;
                movie.PlayingDate = movieObj.PlayingDate;
                movie.PlayingTime = movieObj.PlayingTime;
                movie.Rating = movieObj.Rating;
                movie.Genre = movieObj.Genre;
                movie.TrailorUrl = movieObj.TrailorUrl;
                movie.TicketPrice = movieObj.TicketPrice;
                _dbContext.SaveChanges();
                return Ok("Record updated succefully");
            }
        }

        // DELETE api/values/5
        [Authorize(Roles ="Admin")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var movie = _dbContext.Movies.Find(id);
            if (movie == null)
            {
                return NotFound("No record found against this id");
            }
            else
            {
                _dbContext.Movies.Remove(movie);
                _dbContext.SaveChanges();
                return Ok("Record deleted");
            }
        }

        //GET ALL 
        [Authorize]
        [HttpGet("[Action]")]

        public IActionResult AllMovies(string sort)
        {
            var movies = from movie in _dbContext.Movies
                         select new
                         {
                             Id = movie.Id,
                             Name = movie.Name,
                             Duration = movie.Duration,
                             Language = movie.Language,
                             Rating = movie.Rating,
                             Genre = movie.Genre,
                             ImageUrl = movie.ImageUrl
                         };


            switch (sort) {
                case "desc":
                    return Ok(movies.OrderByDescending(m=>m.Rating));
                case "asc":
                    return Ok(movies.OrderBy(m => m.Rating));
                default:
                    return Ok(movies);
            }

        }


        //GET DETAIL
        [Authorize]
        [HttpGet("[Action]/{id}")]
        public IActionResult MovieDetail(int id) {
            var movie = _dbContext.Movies.Find(id);

            if (movie == null) {
                return NotFound();
            }

            return Ok(movie);
        }
    }
}
