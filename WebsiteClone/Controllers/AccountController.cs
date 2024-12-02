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

namespace WebsiteClone.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        // ลิงก์สำหรับเข้าสู่ระบบผ่าน Discord
        public IActionResult LoginWithDiscord()
        {
            var redirectUrl = Url.Action("Callback", "Account"); // หน้า Callback หลังจาก Discord ให้ข้อมูลกลับมา
            var properties = new AuthenticationProperties { RedirectUri = redirectUrl };

            // เรียกใช้ OAuth การเข้าสู่ระบบผ่าน Discord
            return Challenge(properties, "Discord");
        }

        // รับข้อมูลหลังจากที่ Discord ส่งกลับ
        public async Task<IActionResult> Callback()
        {
            // ตรวจสอบการเข้าสู่ระบบ
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            if (result.Succeeded)
            {
                // หลังจากเข้าสู่ระบบสำเร็จ ให้เปลี่ยนเส้นทางไปที่หน้า Dashboard
                return RedirectToAction("Dashboard", "Account");
            }

            // ถ้าไม่สำเร็จให้แสดงหน้าล็อกอินอีกครั้ง
            return RedirectToAction("Login");
        }
        ///สำหรับ Dashboard เข้าหน้า
        [Authorize]
        public IActionResult Dashboard()
        {
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

    }
}
