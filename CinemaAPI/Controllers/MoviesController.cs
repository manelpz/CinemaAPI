using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CinemaAPI.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CinemaAPI.Controllers
{
    [Route("api/[controller]")]
    public class MoviesController : Controller
    {

        private List<Movie> movies = new List<Movie>
        {
            new Movie(){ Id = 0, Name = "Mission imposible", Language="english"},
            new Movie(){ Id = 1, Name = "the matrix", Language="english"},
        };

        [HttpGet]
        public IEnumerable<Movie> Get() {

            return movies;

        }

        public void Post(Movie) {

        }
        
    }
}
