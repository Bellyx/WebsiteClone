using Microsoft.AspNetCore.Mvc;
using WebsiteClone.Data;
using WebsiteClone.Models;

namespace WebsiteClone.Controllers
{
    public class RentalController : Controller
    {
        private readonly ApplicationDBContext _db;
        public RentalController(ApplicationDBContext db)
        {
            _db = db;
        }
        // เช่าสินค้า
        public ActionResult Create(string productId)
        {
            var product = _db.Product.Find(productId);
            if (product == null)
            {
                return HttpNotFound();
            }

            // กำหนดจำนวนวันที่เช่า (7 วัน) และคำนวณวันที่หมดอายุ
            var rental = new Rental
            {
                Identifier = Guid.NewGuid().ToString(),  // กำหนดค่าถูกต้องที่นี่
                ProductId = productId,
                StartDate = DateTime.Now,
                ExpiryDate = DateTime.Now.AddDays(7),
                DaysRented = 7,
                PointsUsed = product.PointsRequired,
                IsExpired = false
            };

            _db.Rental.Add(rental);
            _db.SaveChanges();

            return RedirectToAction("Index", "Product");
        }

        private ActionResult HttpNotFound()
        {
            throw new NotImplementedException();
        }

        // ตรวจสอบและลบสินค้าที่หมดอายุ
        public ActionResult CheckExpiredRentals()
        {
            var expiredRentals = _db.Rental.Where(r => r.ExpiryDate < DateTime.Now && !r.IsExpired).ToList();

            foreach (var rental in expiredRentals)
            {
                rental.IsExpired = true;  // ตั้งสถานะว่าเป็นการหมดอายุ
                var product = _db.Product.Find(rental.ProductId);
                _db.Product.Remove(product);  // ลบสินค้าที่หมดอายุ
            }

            _db.SaveChanges();

            return RedirectToAction("Index", "Product");
        }
        public ActionResult Index()
        {
            // ตรวจสอบการหมดอายุทุกครั้งเมื่อโหลดหน้า
            var expiredRentals = _db.Rental.Where(r => r.ExpiryDate < DateTime.Now && !r.IsExpired).ToList();

            foreach (var rental in expiredRentals)
            {
                rental.IsExpired = true;
                var product = _db.Product.Find(rental.ProductId);
                _ = _db.Product.Remove(product);
            }

            _db.SaveChanges();

            var products = _db.Product.ToList();
            return View(products);
        }
    }
}

