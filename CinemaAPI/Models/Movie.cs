using System;
using Microsoft.AspNetCore.Http;

namespace CinemaAPI.Models
{
    public class Movie
    {
        public Movie()
        {
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Language { get; set; }
        public double Rating { get; set; }
        public IFormFile Image { get; set; }

    }
}
