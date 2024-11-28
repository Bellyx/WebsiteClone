using System.ComponentModel.DataAnnotations;

namespace WebsiteClone.Models
{
    public class Users
    {
        [Key]
        public string identifier { get; set; }
        [Required]
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string Phone_number { get; set; }
        public string sex { get; set; }

    }
    public class PlayerInfoViewModel
    {
        public IEnumerable<Users> AllPlayers { get; set; }
        public int PlayerCount { get; set; }
    }

}
