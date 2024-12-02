using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebsiteClone.Data;
using WebsiteClone.Models;

namespace WebsiteClone.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDBContext _db;
        public ProductController(ApplicationDBContext db)
        {
            _db = db;
        }
        // แสดงรายการสินค้า
        public ActionResult Index()
        {
            var products = _db.Product.ToList();
            return View(products);
        }

        // แสดงรายละเอียดสินค้า
        public ActionResult Details(int id)
        {
            var product = _db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        private ActionResult HttpNotFound()
        {
            throw new NotImplementedException();
        }
    }
}
