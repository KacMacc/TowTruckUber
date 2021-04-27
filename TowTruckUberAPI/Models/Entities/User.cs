using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace TowTruckUberAPI.Models
{
    public class User : IdentityUser
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required] public string Email { get; set; }
        public bool IsContractor { get; set; }
        public DateTime CreatedAt{ get; set; } = DateTime.Now;

        public ICollection<Trip> CustomerTrips { get; set; }
        public ICollection<Trip> ContractorTrips { get; set; }
    }
}
