using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TowTruckUberAPI.Models
{
    public enum StateEnum
    {
        Pending,
        Active,
        Suspended,
        Cancelled,
        Expired
    }

    public class Payment
    {
        public int Id{ get; set; }
        public StateEnum State { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string StartDate { get; set; }

        [Required]
        public User Payer { get; set; }
        [NotMapped]
        [Required]
        public Object Plan { get; set; }
    }
}
