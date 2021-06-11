using System;
using System.Collections.Generic;
using System.Linq;

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
        public IActionResult Post([FromBody] Reservation reservationObj)
        {

            reservationObj.ReservationTime = DateTime.Now;
            _dbContext.Reservations.Add(reservationObj);
            _dbContext.SaveChanges();

            return StatusCode(StatusCodes.Status201Created);

        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult GetReservations()
        {
            var reservations = from reservation in _dbContext.Reservations
                               join customer in _dbContext.Users on reservation.UserId equals customer.Id
                               join movie in _dbContext.Movies on reservation.MovieId equals movie.Id
                               select new {
                                   Id = reservation.Id,
                                   ReservationTime = reservation.ReservationTime,
                                   CustomerName = customer.Id,
                                   MovieName = movie.Name
                               };
            return Ok(reservations);
        }

        //reservations/1
        [Authorize(Roles = "Admin")]
        [HttpGet ("{id}")]
        public IActionResult GetReservationDetail(int id)
        {
            var reservationResult = (from reservation in _dbContext.Reservations
                               join customer in _dbContext.Users on reservation.UserId equals customer.Id
                               join movie in _dbContext.Movies on reservation.MovieId equals movie.Id
                               where reservation.Id == id
                               select new
                               {
                                   Id = reservation.Id,
                                   ReservationTime = reservation.ReservationTime,
                                   CustomerName = customer.Id,
                                   MovieName = movie.Name,
                                   Email = customer.Email,
                                   Qty = reservation.Qty,
                                   Price = reservation.Price,
                                   Phone = reservation.Phone,
                                   PlayingDate = movie.PlayingDate,
                                   PlayingTime = movie.PlayingTime
                               }).FirstOrDefault();
            return Ok(reservationResult);
        }


        // DELETE api/values/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var reservation = _dbContext.Reservations.Find(id);
            if (reservation == null)
            {
                return NotFound("No record found against this id");
            }
            else
            {
                _dbContext.Reservations.Remove(reservation);
                _dbContext.SaveChanges();
                return Ok("Record deleted");
            }
        }
    }
}
