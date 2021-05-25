using System;
using System.Collections.Generic;
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
    public class ReservationsController : Controller
    {
        private CinemaDBContext _dbContext;

        public ReservationsController(CinemaDBContext dbContext) {
            _dbContext = dbContext;
        }

        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] Reservation reservationObj) {

            _dbContext.Reservations.Add(reservationObj);
            _dbContext.SaveChanges();

            return StatusCode(StatusCodes.Status201Created);
        }
        

    }
}
