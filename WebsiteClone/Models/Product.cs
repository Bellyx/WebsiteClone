using System.ComponentModel.DataAnnotations;

namespace WebsiteClone.Models
{
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
}
