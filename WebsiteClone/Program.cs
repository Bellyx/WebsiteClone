using Microsoft.AspNetCore.Authentication.Cookies;
using AspNet.Security.OAuth.Discord;
using AspNetCore.Unobtrusive.Ajax;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.EntityFrameworkCore;
using WebsiteClone.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;


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
}).AddCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";  // กำหนดหน้า Access Denied ถ้าผู้ใช้ไม่มีสิทธิ์
})
.AddDiscord(options =>
{
    // ตั้งค่า Client ID และ Client Secret จาก appsettings.json
    options.ClientId = builder.Configuration["Discord:ClientId"];
    options.ClientSecret = builder.Configuration["Discord:ClientSecret"];
    options.Scope.Add("identify"); // กำหนดสิทธิ์ที่ต้องการ
    options.SaveTokens = true;
    options.CallbackPath = new PathString("/signin-discord");

    // เพิ่ม event สำหรับจัดการข้อมูลที่ได้รับจาก Discord
    options.ClaimActions.MapJsonKey("urn:discord:username", "username");
    options.ClaimActions.MapJsonKey("urn:discord:UserId", "UserId");
    options.ClaimActions.MapJsonKey("urn:discord:avatar", "avatar");
    options.ClaimActions.MapJsonKey("urn:discord:Discriminator", "discriminator");
    options.ClaimActions.MapJsonKey("urn:discord:id", "id");
    options.ClaimActions.MapJsonKey("urn:discord:Role", "Role");
});

builder.Services.AddAuthorization(options =>
{
    // กำหนด roles ต่างๆ หากต้องการ
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
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

