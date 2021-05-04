using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TowTruckUberAPI.Models.Dtos
{
    public record TripDto
    {
        public StatusEnum Status { get; init; }
        public DateTime CreatedAt { get; init; }
        public DateTime EstimatedTime { get; init; }
        public long Distance { get; init; }
        public double? Price { get; init; }
        public Address StartLocation { get; init; }
        public Address EndLocation { get; init; }
        public User Contractor { get; init; }
    }
}
