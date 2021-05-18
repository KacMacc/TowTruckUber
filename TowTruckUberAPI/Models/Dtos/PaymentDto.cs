using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TowTruckUberAPI.Models.Dtos
{
    public class PaymentDto
    {
        public int Id { get; init; }
        public string State { get; init; }
        public string Name { get; init; }
        public string StartDate { get; init; }
        public string Description { get; init; }
        public String PayerId { get; init; }
        public Object Plan { get; init; }
    }
}
