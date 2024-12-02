using Microsoft.AspNetCore.Authentication.Cookies;
using AspNet.Security.OAuth.Discord;
using AspNetCore.Unobtrusive.Ajax;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.EntityFrameworkCore;
using WebsiteClone.Data;
using System.Security.Claims;


var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews();

///************************************************************** Conect Database **************************************************************
builder.Services.AddDbContext<ApplicationDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))); /// สำรหับ Connect DB

builder.Services.AddUnobtrusiveAjax(useCdn: true, injectScriptIfNeeded: false); // สำหรับ Ajax

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = DiscordAuthenticationDefaults.AuthenticationScheme;
}).AddCookie()
.AddDiscord(options => {
// ตั้งค่า Client ID และ Client Secret จาก appsettings.json
options.ClientId = builder.Configuration["Discord:ClientId"];
options.ClientSecret = builder.Configuration["Discord:ClientSecret"];
options.Scope.Add("identify"); // กำหนดสิทธิ์ที่ต้องการ
options.SaveTokens = true;
options.CallbackPath = new PathString("/signin-discord");
    // ใช้ event เพื่อจัดการข้อมูลที่ได้รับจาก Discord

});



builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseUnobtrusiveAjax();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// ใช้งาน Authentication และ Authorization
app.UseAuthentication();
app.UseAuthorization();


app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();

