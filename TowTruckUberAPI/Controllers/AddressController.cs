using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TowTruckUberAPI.Models;
using TowTruckUberAPI.Models.Dtos;

namespace TowTruckUberAPI.Controllers
{
    [Route("address")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        public class AddressApiController : ControllerBase
        {
            private readonly AppDbContext _dbContext;

            public AddressApiController(AppDbContext dbContext)
            {
                _dbContext = dbContext;
            }


            [HttpPost]
            [Route("{addressId}")]
            public async Task<IActionResult> CreateAddress(AddressDto addressDto)
            {
                Address address = new Address()
                {
                    City = addressDto.City,
                    Country = addressDto.Country,
                    FlatNum = addressDto.FlatNum,
                    HouseNum = addressDto.FlatNum,
                    Street = addressDto.Street,
                    ZipCode = addressDto.ZipCode,
                };

                await _dbContext.Addresses.AddAsync(address);
                await _dbContext.SaveChangesAsync();

                return Ok(new Response { Status = "Success", Message = "Address created successfully." });
            }


            [HttpGet]
            [Route("{addressId}")]
            public async Task<IActionResult> GetAddressById([FromRoute][Required] int addressId)
            {
                Address addressExists = await _dbContext.Addresses.FindAsync(addressId);

                if (addressExists is null)
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Cannot find address with this Id." });

                return Ok(JsonSerializer.Serialize(addressExists));
            }
        }
    }
}
