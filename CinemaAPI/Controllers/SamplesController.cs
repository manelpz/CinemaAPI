using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CinemaAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class SamplesController : Controller
    {
        // GET: api/values
        [HttpGet]
        public string Get()
        {
            return "Hello from the user side";
        }

        // GET api/values/5
        [Authorize(Roles = "ADmin")]
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "Hello from the admin side";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
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
