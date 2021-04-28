using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace TowTruckUberAPI.Models
{
    [Table("AspNetUsers")]
    public class User : IdentityUser
    { 
        /*
       * default properties of IdentityUser:
       * Id, UserName, Claims, Email, Logins, PasswordHash, Roles, PhoneNumber, SecurityStamp
       * to use other properties, add it into class
       */
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }

        public bool IsContractor { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public ICollection<Trip> CustomerTrips { get; set; }
        public ICollection<Trip> ContractorTrips { get; set; }
    }
}
