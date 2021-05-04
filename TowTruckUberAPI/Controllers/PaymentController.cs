using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json;
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
        private readonly AppDbContext _dbContext;

        public PaymentController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        [HttpPost]
        [Route("{paymentId}")]
        public IActionResult CreatePayment([FromRoute][Required] int paymentId, [FromBody] PaymentDto paymentDto)
        {
            throw new NotImplementedException();
        }


        [HttpGet]
        [Route("{paymentId}")]
        public async Task<IActionResult> GetPaymentById([FromRoute][Required] int paymentId)
        {
            Payment paymentExists = await _dbContext.Payments.FindAsync(paymentId);

            if (paymentExists is null)
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { Status = "Error", Message = "Cannot find payment with this Id." });

            return Ok(JsonSerializer.Serialize(paymentExists));
        }
    }
}
