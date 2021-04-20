using System;
using System.ComponentModel.DataAnnotations;

namespace TowTruckUberAPI.Models
{
    public enum StatusEnum
    {
        Available,
        InProgress,
        Completed,
        Canceled
    }
    public class Trip
    {
        public int Id { get; set; }
        [Required]
        public StatusEnum Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? EstimatedTime { get; set; }
        [Required]
        public long Distance { get; set; }
        [Required]
        public double? Price { get; set; }

        [Required]
        public Address StartLocation { get; set; }
        [Required]
        public Address EndLocation { get; set; }

        public User Customer { get; set; }
        public User Contractor { get; set; }

        public Payment Payment { get; set; }
    }
}
