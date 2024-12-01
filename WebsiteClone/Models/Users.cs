using System.ComponentModel.DataAnnotations;

namespace WebsiteClone.Models
{
    public class Users
    {
        [Key]
        public required string Identifier { get; set; }
        [Required]
        public required string Firstname { get; set; }
        public required string Lastname { get; set; }
        public required string Phone_number { get; set; }
        public required string Sex { get; set; }

    }
    public class PlayerInfoViewModel
    {
        public required IEnumerable<Users> AllPlayers { get; set; }
        public int PlayerCount { get; set; }
    }

}
