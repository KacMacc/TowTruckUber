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
using Microsoft.EntityFrameworkCore;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace TowTruckUberAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public UserController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
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
        public string Register()
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
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user is not null)
            {
                await _signInManager.SignOutAsync();
                var result = await _signInManager.PasswordSignInAsync(user, loginDto.Password, false, false);

                if (!result.Succeeded)
                    return Unauthorized(new Response { Status = "Error", Message = "Wrong password or/and email." });

                return Ok(new Response { Status = "Success", Message = "User login successfully." });
            }

            return Unauthorized(new Response { Status = "Error", Message = "Wrong password or/and email." });
        }

        [HttpPost]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok(new Response {Status = "Success", Message = "User logout successfully."});
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

            foreach(var error in result.Errors)
            {
                registerErrors += $"{error.Description}<br>";
            }

            var x = new Response {Status = "Error", Message = registerErrors};
            return !result.Succeeded ? StatusCode(StatusCodes.Status500InternalServerError, JsonSerializer.Serialize(x))
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
