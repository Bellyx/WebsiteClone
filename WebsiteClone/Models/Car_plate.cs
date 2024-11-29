using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace WebsiteClone.Models
{
    public class Car_plate
    {
        [Key]
        public required string Identifier { get; set; }
        [Required]
        public required string CarId { get; set; }
        public required string CarPlate { get; set; }

    }
    public class CarplateViewModel
    {
        public int CarCount { get; set; }
    }
}
