using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TowTruckUberAPI.Models.Dtos
{
    public record ResponseDto
    {
        public string Status { get; init; }
        public string Message { get; init; }
    }
}
