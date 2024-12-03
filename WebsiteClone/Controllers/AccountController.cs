using System.Net;
using AspNet.Security.OAuth.Discord;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebsiteClone.Data;
using WebsiteClone.Models;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;

namespace WebsiteClone.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDBContext _db;
        public AccountController(ApplicationDBContext db)
        {
            _db = db;
        }
        // หน้า Login พื้นฐาน
        public IActionResult Login()
        {
            return View();
        }

        // ลิงก์สำหรับเข้าสู่ระบบผ่าน Discord
        public IActionResult LoginWithDiscord()
        {
            var redirectUrl = Url.Action("Callback", "Account");
            var properties = new AuthenticationProperties { RedirectUri = redirectUrl };

            // เรียกใช้ OAuth การเข้าสู่ระบบผ่าน Discord
            return Challenge(properties, "Discord");
        }

        // รับข้อมูลหลังจากที่ Discord ส่งกลับ
        public async Task<IActionResult> Callback()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            if (result.Succeeded)
            {
                var claimsIdentity = result.Principal.Identity as ClaimsIdentity;

                // ดึงข้อมูล username, userId และ avatar จาก Claims ที่ Discord ส่งมา
                var username = result.Principal.FindFirst("urn:discord:username")?.Value;
                var userId = result.Principal.FindFirst("urn:discord:id")?.Value;
                var avatar = result.Principal.FindFirst("urn:discord:avatar")?.Value; // avatar hash
                var discriminator = result.Principal.FindFirst("urn:discord:discriminator")?.Value; // avatar hash

                // ดึง Role ของผู้ใช้จากฐานข้อมูล
                var role = await GetUserRoleAsync(userId, username);
               
                // สร้าง URL สำหรับรูปโปรไฟล์
                string avatarUrl = string.Empty;
                if (!string.IsNullOrEmpty(avatar) && !string.IsNullOrEmpty(userId))
                {
                    avatarUrl = $"https://cdn.discordapp.com/avatars/{userId}/{avatar}.png";
                }

                // เพิ่มข้อมูลใน ClaimsIdentity
                if (!string.IsNullOrEmpty(username))
                {
                    claimsIdentity.AddClaim(new Claim(ClaimTypes.Name, username));
                    claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, role));  // เพิ่ม Role ใน Claims
                }

                // บันทึกข้อมูล avatarUrl ลงใน ViewData หรือ Claims
                ViewData["AvatarUrl"] = avatarUrl;
                ViewData["Username"] = username;
                ViewData["UserId"] = userId;
                ViewData["discriminator"] = discriminator;
                TempData["Role"] = role;


                return RedirectToAction("Dashboard", "Account");
            }

            return RedirectToAction("Login");
        }

        // สำหรับ Dashboard เข้าหน้า
        [Authorize]
        public IActionResult Dashboard()
        {
            // อ่านข้อมูลจาก ClaimsIdentity
            var username = User.Identity?.Name; // อ่านชื่อผู้ใช้จาก ClaimsIdentity (หลังจากเพิ่ม Claim ใน Callback)
            var userId = User.FindFirst("urn:discord:id")?.Value; // ดึงข้อมูล UserId จาก Claims ที่ Discord ส่งมา
            var avatar = User.FindFirst("urn:discord:avatar")?.Value; // ดึงข้อมูล avatar hash จาก Claims ที่ Discord ส่งมา
            var discriminator = User.FindFirst("urn:discord:discriminator")?.Value; // ดึงข้อมูล discriminator จาก Claims (ถ้ามี)
            var role = TempData["Role"]?.ToString();

            // สร้าง URL สำหรับรูปโปรไฟล์ Discord
            string avatarUrl = string.Empty;
            if (!string.IsNullOrEmpty(avatar) && !string.IsNullOrEmpty(userId))
            {
                avatarUrl = $"https://cdn.discordapp.com/avatars/{userId}/{avatar}.png";
            }

            // ส่งข้อมูลไปยัง View
            ViewData["Username"] = username;
            ViewData["Discriminator"] = discriminator;
            ViewData["UserId"] = userId;
            ViewData["AvatarUrl"] = avatarUrl;
            ViewData["Role"] = role;  // ส่งข้อมูล Role ไปยัง View
            return View();
        }

        // Action สำหรับ Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            // Sign out จากระบบ (ทำการลบข้อมูลใน Cookie)
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // หลังจาก Logout แล้วให้เปลี่ยนเส้นทางไปที่หน้าหลัก
            return RedirectToAction("Login", "Account");
        }

        // ดึง Role ของผู้ใช้จากฐานข้อมูล
        public async Task<string> GetUserRoleAsync(string userId, string username)
        {
            // ค้นหาผู้ใช้ในฐานข้อมูลตาม UserId
            var user = await _db.Discordpermiss
                                 .FromSqlRaw("SELECT UserID, USERNAME, ROLE FROM Discordpermiss WHERE UserId = {0}", userId)
                                 .FirstOrDefaultAsync();

            // ถ้าไม่พบผู้ใช้ ให้ทำการ Insert ข้อมูลใหม่ลงในตาราง และกำหนด Role เป็น "User"
            if (user == null)
            {
                // สร้างคำสั่ง SQL สำหรับ Insert ข้อมูลใหม่โดยใช้ parameterized query
                var sql = "INSERT INTO Discordpermiss (UserId, Username, Role) VALUES ({0}, {1}, {2})";

                // รันคำสั่ง SQL โดยใช้ ExecuteSqlRawAsync
                await _db.Database.ExecuteSqlRawAsync(sql, userId, username, "User");

                // กำหนด Role เป็น "User"
                return "User";  // ส่งค่า Role ที่กำหนดให้กับผู้ใช้ใหม่
            }
            // ถ้าพบผู้ใช้และ Role ให้ส่งค่า Role ของผู้ใช้, ถ้าไม่พบจะคืนค่า "User"
                return user?.Role ?? "User";  // ถ้าไม่พบ Role จะใช้ค่า "User" เป็นค่าเริ่มต้น
        }




        ///
        [Authorize(Roles = "Admin")]  // ผู้ใช้ที่มี Role "Admin" เท่านั้นที่สามารถเข้าถึงหน้า AdminPage ได้
        public IActionResult AdminPage()
        {
            return View();
        }
    }
}
