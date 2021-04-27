using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using TowTruckUberAPI.Models;
using TowTruckUberAPI.Models.Dtos;

namespace TowTruckUberAPI.Controllers
{
    [Authorize]
    [Route("payment")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        [HttpPost]
        [Route("/{paymentId}")]
        public IActionResult CreatePayment([FromRoute][Required] int paymentId, [FromBody] PaymentDto paymentDto)
        {
            throw new NotImplementedException();
        }
        

        [HttpGet]
        [Route("/{paymentId}")]
        public IActionResult GetPaymentById([FromRoute][Required] int paymentId)
        {
            throw new NotImplementedException();
        }
    }
}
