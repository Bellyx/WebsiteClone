using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace WebsiteClone.Models
{
    public class Car_plate
    {
        [Key]
        public required string Identifier { get; set; }
        [Required]
        public required string Car_id { get; set; }
        public required string Car_number { get; set; }
        public required string Firstname { get; set; }
        public required string Lastname { get; set; }

    }
    public class CarplateViewModel
    {
        public int CarCount { get; set; }
    }
}
