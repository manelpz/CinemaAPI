using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace CinemaAPI.Models
{
    public class Movie
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Language { get; set; }
        public double Rating { get; set; }


        [NotMapped]
        public IFormFile Image { get; set; }

        

    }
}
