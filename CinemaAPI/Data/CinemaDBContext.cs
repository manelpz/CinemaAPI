using System;
using CinemaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CinemaAPI.Data
{
    public class CinemaDBContext: DbContext
    {
        public CinemaDBContext(DbContextOptions<CinemaDBContext> options) : base(options)
        {

        }

        public DbSet<Movie> movies { get; set; }

        public DbSet<User> User { get; set; }

        public DbSet<Reservation> Reservations { get; set; }
    }
}
