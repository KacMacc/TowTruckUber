using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TowTruckUberAPI.Models;
using TowTruckUberAPI.Models.Dtos;

namespace TowTruckUberAPI.Controllers
{
    [Authorize]
    [Route("trip")]
    [ApiController]
    public class TripController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly UserManager<User> _userManager;

        public TripController(AppDbContext dbContext, UserManager<User> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        [HttpGet]
        [Route("generateTrips/{email}")]
        public IActionResult GenerateTripByEmail([FromRoute][Required] string email)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("userTrip/{email}")]
        public async Task<IActionResult> GetTripByEmail([FromRoute][Required] string email)
        {
            var userExists = await _userManager.FindByEmailAsync(email);

            if (userExists is null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Can't find user with that email." });

            var tripExists = userExists.ContractorTrips.Concat(userExists.CustomerTrips).OrderByDescending(x => x.CreatedAt).FirstOrDefault();

            if (tripExists is null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Can't find any trip with that user." });

            return Ok(JsonSerializer.Serialize(tripExists));
        }

        [HttpGet]
        [Route("{tripId}")]
        public async Task<IActionResult> GetTripById([FromRoute][Required] int tripId)
        {
            var tripExists = await _dbContext.Trips.FindAsync(tripId);

            if (tripExists is null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Can't find trip with that Id." });

            return Ok(JsonSerializer.Serialize(tripExists));
        }

        [HttpPost]
        public async Task<IActionResult> PostTripById([FromBody] TripDto tripDto)
        {

            string getUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(getUserId);

            Trip trip = new Trip()
            {
                Customer = currentUser,
                CreatedAt = tripDto.CreatedAt,
                Status = tripDto.Status,
                EstimatedTime = tripDto.EstimatedTime,
                Distance = tripDto.Distance,
                Price = tripDto.Price ?? 0,
                StartLocation = tripDto.StartLocation,
                EndLocation = tripDto.EndLocation,
                Contractor = tripDto.Contractor
            };

            await _dbContext.Trips.AddAsync(trip);
            await _dbContext.SaveChangesAsync();

            return Ok(new Response { Status = "Success", Message = "Trip created successfully." });
        }

        [HttpPut]
        [Route("{tripId}")]
        public async Task<IActionResult> UpdateTripStatus([FromRoute][Required] int tripId, [FromBody] StatusEnum status)
        {
            var tripExists = await _dbContext.Trips.FindAsync(tripId);

            if (tripExists is null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Can't find trip with that Id." });

            tripExists.Status = status;

            return Ok(new Response { Status = "Success", Message = "Trip's status updated successfully." });
        }
    }
}
