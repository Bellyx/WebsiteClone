using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        ///  query หาข้อมูลรถใน server บอก  เจ้าของ, ป้ายทะเบียน,และรุ่นรถ 
        public IActionResult Car_plate()
        {
            var carPlateData = _db.Car_Plate
         .FromSqlInterpolated($@"
        SELECT 
            CAR_PLATE.IDENTIFIER,
            CAR_PLATE.CAR_ID,
            CAR_PLATE.CAR_NUMBER,
            USERS.FIRSTNAME,
            USERS.LASTNAME
        FROM CAR_PLATE 
        JOIN USERS ON USERS.IDENTIFIER = CAR_PLATE.IDENTIFIER
        ORDER BY USERS.IDENTIFIER ").ToList();

            // นับจำนวนข้อมูล
            ViewBag.DatacarCount = carPlateData.Count();

            // ส่งข้อมูลไปยัง View
            return View(carPlateData);
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
