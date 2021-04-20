using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TowTruckUberAPI.Models
{
    public class MapGrid
    {
        public int Id { get; set; }
        [Required]
        public string Latitude { get; set; }
        [Required]
        public string Longitude { get; set; }

        public ICollection<Address> Addresses { get; set; }
    }
}
