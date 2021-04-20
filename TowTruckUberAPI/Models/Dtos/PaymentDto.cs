using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TowTruckUberAPI.Models.Dtos
{
    public class PaymentDto
    {
        public string State { get; init; }
        public string Name { get; init; }
        public string Description { get; set; }
        public UserDto Payer { get; set; }
    }
}
