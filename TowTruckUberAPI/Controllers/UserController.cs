using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using TowTruckUberAPI.Models;
using TowTruckUberAPI.Models.Dtos;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Cors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TowTruckUberAPI.Services;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace TowTruckUberAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<User> _logger;

        public UserController(AppDbContext dbContext, UserManager<User> userManager, SignInManager<User> signInManager, ILogger<User> logger)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }


        [HttpGet]
        [Route("login")]
        public string Login()
        {
            MapGrid mapGrid = new MapGrid()
            {
                Id = 3,
                Latitude = "3535.353",
                Longitude = "-493.434"
            };

            return JsonSerializer.Serialize(mapGrid); ;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("register")]
        public async Task<string> Register()
        {
            MapGrid mapGrid = new MapGrid()
            {
                Id = 3,
                Latitude = "3535.353",
                Longitude = "-493.434"
            };

            var customer = await _userManager.FindByEmailAsync("czarek595@gmail.com");
            var contractor = await _userManager.FindByEmailAsync("czarek595@gmail.com");
            var address1 = await _dbContext.Addresses.FindAsync(1);
            var payment = await _dbContext.Payments.FindAsync(1);
            Trip trip = new Trip()
            {
                Contractor = contractor,
                Customer = customer,
                CreatedAt = new DateTime(2021, 5, 11, 11, 10, 30),
                EstimatedTime = new DateTime(2021, 5, 11, 12, 40, 44),
                Distance = 3300,
                Price = 129.32,
                EndLocation = address1,
                StartLocation = address1,
                Id = 1,
                Payment = payment
            };


            Notification.SendNotificationToCustomer(trip);

            return JsonSerializer.Serialize(mapGrid); ;
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user is not null)
            {
                await _signInManager.SignOutAsync();
                var result = await _signInManager.PasswordSignInAsync(user, loginDto.Password, false, false);

                if (!result.Succeeded)
                {
                    _logger.LogWarning("Wrong password or/and email.");
                    return Unauthorized(new Response { Status = "Error", Message = "Wrong password or/and email." });
                }

                _logger.LogInformation("User logged in.");
                return Ok(new Response { Status = "Success", Message = "User login successfully." });
            }
            _logger.LogWarning("Wrong password or/and email.");
            return Unauthorized(new Response { Status = "Error", Message = "Wrong password or/and email." });
        }

        [HttpPost]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            _logger.LogInformation("User logout successfully.");
            return Ok(new Response { Status = "Success", Message = "User logout successfully." });
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var userExists = await _userManager.FindByNameAsync(registerDto.Email);

            if (userExists is not null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User with this e-mail already exists!" });

            User user = new User()
            {
                Name = registerDto.Name,
                Surname = registerDto.Surname,
                PhoneNumber = registerDto.PhoneNumber,
                Email = registerDto.Email,
                UserName = $"{registerDto.Name}{registerDto.Surname}",
                SecurityStamp = Guid.NewGuid().ToString(),
            };


            var result = await _userManager.CreateAsync(user, registerDto.Password);
            string registerErrors = "";

            foreach (var error in result.Errors)
            {
                registerErrors += $"{error.Description}<br>";
            }

            var x = new Response { Status = "Error", Message = registerErrors };

            if (!result.Succeeded)
            {
                _logger.LogWarning("User creation failed.");
                StatusCode(StatusCodes.Status500InternalServerError, JsonSerializer.Serialize(x));
            }

            _logger.LogInformation("User created successfully.");
            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }

        [HttpDelete]
        [Route("{email}")]
        public async Task<IActionResult> DeleteUser([FromRoute][Required] string email)
        {
            var userExists = await _userManager.FindByEmailAsync(email);

            if (userExists is null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User with this e-mail already exists!" });

            var result = await _userManager.DeleteAsync(userExists);

            return !result.Succeeded ? StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User deleting failed! Please check user details and try again." })
                : Ok(new Response { Status = "Success", Message = "User deleted successfully!" });
        }

        [HttpGet]
        [Route("{email}")]
        public async Task<IActionResult> GetUserByEmail([FromRoute][Required] string email)
        {
            var userExists = await _userManager.FindByEmailAsync(email);

            if (userExists is null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Can't find user with that email." });

            return Ok(JsonSerializer.Serialize(userExists));
        }


        [HttpPut]
        [Route("{email}")]
        public async Task<IActionResult> UpdateUser([FromRoute][Required] string email, [FromBody] User body)
        {
            var userExists = await _userManager.FindByEmailAsync(email);

            if (userExists is null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Can't find user with that email." });

            userExists.Name = body.Name ?? userExists.Name;
            userExists.Surname = body.Surname ?? userExists.Surname; ;
            userExists.PhoneNumber = body.PhoneNumber ?? userExists.PhoneNumber; ;
            userExists.Email = body.Email ?? userExists.Email; ;
            userExists.UserName = $"{body.Name}{body.Surname}";

            return Ok(new Response { Status = "Success", Message = "User deleted successfully!" });
        }
        [AllowAnonymous]
        [HttpGet]
        [Route("AccessDenied")]
        public IActionResult AccessDenied()
        {
            _logger.LogWarning("Unauthorized access.");
            return Unauthorized();
        }

    }
}
