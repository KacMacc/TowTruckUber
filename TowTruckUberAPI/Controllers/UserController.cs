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
using Microsoft.AspNetCore.Cors;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace TowTruckUberAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;

        public UserController(UserManager<User> userManager, IConfiguration configuration)
        {
            this._userManager = userManager;
            _configuration = configuration;
        }


        [AllowAnonymous]
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
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, loginDto.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(3),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }

            return Unauthorized();
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var userExists = await _userManager.FindByNameAsync(registerDto.Email);

            if (userExists is not null)
                return StatusCode(StatusCodes.Status411LengthRequired, new Response { Status = "Error", Message = "User with this e-mail already exists!" });

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


            return !result.Succeeded ? StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = $"User creation failed! Please check user details and try again." })
                : Ok(new Response { Status = "Success", Message = "User created successfully!" });
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

    }
}
