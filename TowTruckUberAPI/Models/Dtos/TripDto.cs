using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TowTruckUberAPI.Models.Dtos
{
    public record TripDto
    {
        public string Status { get; init; }
        public string CreatedAt { get; init; }
        public string EstimatedTime { get; init; }
        public string Distance { get; init; }
        public string Price { get; init; }
        public string StartLocation { get; init; }
        public string EndLocation { get; init; }
        public UserDto Customer { get; init; }
        public UserDto Contractor { get; init; }
    }
}
