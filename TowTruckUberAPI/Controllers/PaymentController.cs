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
using Microsoft.AspNetCore.Identity;

namespace TowTruckUberAPI.Controllers
{
    [Authorize]
    [Route("payment")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly UserManager<User> _userManager;

        public PaymentController(AppDbContext dbContext, UserManager<User> userManager)
        {
            _userManager = userManager;
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

        [AllowAnonymous]
        [HttpPost]
        [Route("payment")]
        public async Task<IActionResult> AddPayment([FromBody] PaymentDto paymentDto)
        {
            var user = await _userManager.FindByNameAsync(paymentDto.PayerId);

            Payment payment = new Payment()
            {
                Id = paymentDto.Id,
                Name = paymentDto.Name ?? "nejm",
                StartDate = paymentDto.StartDate ?? "dejt",
                Payer = user,
                Plan = 1,
                Description = paymentDto.Description ?? "dawda",
                State = StateEnum.Active,
            };

            var result = _dbContext.Payments.Add(payment);
            _dbContext.SaveChanges();

            return Ok(new Response { Status = "Success", Message = "Payment added to database" });
        }
    }
}
