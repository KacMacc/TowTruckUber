using System.ComponentModel.DataAnnotations;

namespace TowTruckUberAPI.Models
{
    public class Address
    {
        public int Id { get; set; }
        public string Country { get; set; }
        [Required]
        public string City { get; set; }
        public string ZipCode { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string HouseNum { get; set; }
        public string FlatNum { get; set; }

        public MapGrid MapGrid { get; set; }

        public string ToStringForEmail()
        {
            return $"{nameof(Country)}: {Country}, {nameof(City)}: {City}, {nameof(ZipCode)}: {ZipCode}\n" +
            $"{nameof(Street)}: {Street}, {nameof(HouseNum)}: {HouseNum} {nameof(FlatNum)}: {FlatNum}";
        }
    }
}
