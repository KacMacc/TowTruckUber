using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TowTruckUberAPI.Models.Dtos;

namespace TowTruckUberAPI.Controllers
{
    [Authorize]
    [Route("trip")]
    [ApiController]
    public class TripController : ControllerBase
    {
        [HttpGet]
        [Route("generateTrips/{email}")]
        public IActionResult GenerateTripByEmail([FromRoute][Required] string email)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("userTrip/{email}")]
        public IActionResult GetTripByEmail([FromRoute][Required] string email)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("{tripId}")]
        public IActionResult GetTripById([FromRoute][Required] int tripId)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public IActionResult PostTripById([FromBody] TripDto trip)
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        [Route("{tripId}")]
        public IActionResult PutTripById([FromRoute][Required] int tripId, [FromBody] string status)
        {
            throw new NotImplementedException();
        }
    }
}
