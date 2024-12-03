using System.ComponentModel.DataAnnotations;

namespace WebsiteClone.Models
{
    public class Discordpermiss
    {
        [Key]
        public required string UserId { get; set; }
        [Required]
        public required string Username { get; set; }
        public required string Role { get; set; }
    }
}
