using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TowTruckUberAPI.Models.Dtos
{
    public record LoginDto
    {
        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; init; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; init; }
    }
}
