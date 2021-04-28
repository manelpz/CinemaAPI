using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CinemaAPI.Data;
using CinemaAPI.Models;
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
        public IEnumerable<Movie> Get()
        {
            return _dbContext.movies;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Movie Get(int id)
        {
            var movie= _dbContext.movies.Find(id);
            return movie;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] Movie movieObj)
        {
            _dbContext.movies.Add(movieObj);
            _dbContext.SaveChanges();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
