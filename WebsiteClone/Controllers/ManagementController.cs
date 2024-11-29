using System.Net;
using Microsoft.AspNetCore.Mvc;
using WebsiteClone.Data;
using WebsiteClone.Models;

namespace WebsiteClone.Controllers
{
    public class ManagementController : Controller
    {
        private readonly ApplicationDBContext _db;

        ///เข้าถึงฐานข้อมูล 
        public ManagementController(ApplicationDBContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
        
            return View();
        }
        public IActionResult Car_plate()
        {
            return View();
        }

        public IActionResult Item_list()
        {
            return View();
        }
        public IActionResult Player_info()
        {

            // ใช้สำหรับดึงข้อมูลที่มีหลายข้อมูล
            IEnumerable<Users> Allplayer = _db.Users;
            ViewBag.DataCount = Allplayer.Count(); // นับจำนวนข้อมูล
            return View(Allplayer);
        }

    

    }
}
