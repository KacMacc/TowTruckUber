using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TowTruckUberAPI.Models.Dtos
{
    public record RegisterDto
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; init; }
        [Required(ErrorMessage = "Surname is required")]
        public string Surname { get; init; }
        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; init; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; init; }
        [Required(ErrorMessage = "PhoneNumber is required")]
        public string PhoneNumber { get; init; }
    }
}
