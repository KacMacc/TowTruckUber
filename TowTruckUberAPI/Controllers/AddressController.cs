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
    [Route("address")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        public class AddressApiController : ControllerBase
        {

            [HttpPost]
            [Route("/{addressId}")]
            public IActionResult CreateAddress(AddressDto addressDto)
            {
                throw new NotImplementedException();
            }


            [HttpGet]
            [Route("/userAddress/{email}")]
            public IActionResult GetAddressByEmail([FromRoute][Required] string email)
            {
                throw new NotImplementedException();
            }


            [HttpGet]
            [Route("/{addressId}")]
            public IActionResult GetAddressById([FromRoute][Required] int addressId)
            {
                throw new NotImplementedException();
            }
        }
    }
}
