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

    public class Rental
    {
        [Key]
        public string Identifier { get; set; } //ไอดีผู้เช่า
        [Required]
        public int RentalId { get; set; }  // ID ของการเช่า
        public string ProductId { get; set; }  // รหัสสินค้าที่เช่า
        public DateTime StartDate { get; set; }  // วันที่เริ่มเช่า
        public DateTime ExpiryDate { get; set; }  // วันที่หมดอายุ
        public int DaysRented { get; set; }  // จำนวนวันที่เช่า
        public int PointsUsed { get; set; }  // จำนวน point ที่ใช้
        public bool IsExpired { get; set; }  // สถานะการหมดอายุของสินค้า
        public virtual Product Product { get; set; }  // เชื่อมโยงกับสินค้า
    }

    public class Product
    {
        [Key]
        public string ProductId { get; set; }  // ID ของสินค้า
        [Required]
        public string Productname { get; set; }  // ชื่อสินค้า
        public required string Description { get; set; }  // รายละเอียดสินค้า
        public decimal PricePerDay { get; set; }  // ราคาเช่าต่อวัน
        public int PointsRequired { get; set; }  // จำนวน point ที่ต้องใช้ในการเช่า
    }

    public class Discordpermiss
    {
        [Key]
        public required string UserId { get; set; }
        [Required]
        public required string Username { get; set; }
        public required string Role { get; set; }
    }
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
