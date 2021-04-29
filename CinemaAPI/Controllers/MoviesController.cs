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
    [Route("api/[controller]")]
    public class MoviesController : Controller
    {
        private CinemaDBContext _dbContext;

        public MoviesController(CinemaDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/values
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_dbContext.movies);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var movie= _dbContext.movies.Find(id);

            if (movie == null)
            {
                return NotFound("No record found against this id");
            }
            else
            {
                return Ok(movie);
            }
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] Movie movieObj)
        {
            _dbContext.movies.Add(movieObj);
            _dbContext.SaveChanges();
            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Movie movieObj)
        {
            var movie = _dbContext.movies.Find(id);

            if (movie == null)
            {
                return NotFound("No record found against this id");
            }
            else
            {
                movie.Name = movieObj.Name;
                movie.Language = movieObj.Language;
                movie.Rating = movieObj.Rating;
                _dbContext.SaveChanges();
                return Ok("Record updated succefully");
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var movie = _dbContext.movies.Find(id);
            if (movie == null)
            {
                return NotFound("No record found against this id");
            }
            else
            {
                _dbContext.movies.Remove(movie);
                _dbContext.SaveChanges();
                return Ok("Record deleted");
            }
        }
    }
}
