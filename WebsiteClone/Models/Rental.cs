using System.ComponentModel.DataAnnotations;

namespace WebsiteClone.Models
{
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
}
