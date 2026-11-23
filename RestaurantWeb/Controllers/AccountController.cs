using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantWeb.Data;
using RestaurantWeb.Models;

namespace RestaurantWeb.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string email, string password, string? returnUrl = null)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                ViewBag.Error = "Vui lòng nhập Số thẻ / Email / Điện thoại và Mã PIN / Mật khẩu.";
                return View();
            }

            var key = email.Trim();
            var pwd = password.Trim();

            var user = await _context.AppUsers.FirstOrDefaultAsync(u =>
                u.IsActive &&
                (u.Email == key || u.Phone == key || u.MemberCardCode == key) &&
                (u.PasswordHash == pwd || u.ConciergePin == pwd));

            if (user == null)
            {
                ViewBag.Error = "Thông tin định danh hội viên hoặc Mã PIN/Mật khẩu không chính xác!";
                return View();
            }

            HttpContext.Session.SetInt32("UserId", user.Id);
            HttpContext.Session.SetString("UserName", $"{user.Title} {user.FullName}".Trim());
            HttpContext.Session.SetString("UserEmail", user.Email);
            HttpContext.Session.SetString("UserPhone", user.Phone);
            HttpContext.Session.SetString("UserRole", user.Role);
            HttpContext.Session.SetString("UserCardCode", user.MemberCardCode);
            HttpContext.Session.SetString("UserTier", user.MemberTier);
            HttpContext.Session.SetString("UserAddress", user.Address ?? string.Empty);

            TempData["SuccessMessage"] = $"Kính chào {user.Title} {user.FullName} ({user.MemberTier} • {user.MemberCardCode})!";

            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            if (user.Role == "Admin" || user.Role == "Staff")
            {
                return RedirectToAction("Index", "Admin");
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(new AppUser
            {
                Title = "Ông",
                MemberCardCode = $"LA-VIP-{DateTime.Now.Year}-{Random.Shared.Next(1000, 9999)}",
                MemberTier = "Privilège Noir",
                WinePreference = "Burgundy Pinot Noir & Chardonnay",
                PreferredSeating = "Phòng Riêng VIP Atelier Salon"
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(AppUser model)
        {
            if (string.IsNullOrWhiteSpace(model.FullName) || string.IsNullOrWhiteSpace(model.Email) || string.IsNullOrWhiteSpace(model.Phone))
            {
                ViewBag.Error = "Vui lòng điền đầy đủ Họ tên, Điện thoại và Thư điện tử.";
                return View(model);
            }

            if (await _context.AppUsers.AnyAsync(u => u.Email == model.Email.Trim()))
            {
                ViewBag.Error = "Địa chỉ Email này đã được kích hoạt trên cổng Hội viên L'Atelier!";
                return View(model);
            }

            model.Email = model.Email.Trim();
            model.Role = "Customer";
            model.MemberTier = "Privilège Noir";
            if (string.IsNullOrWhiteSpace(model.MemberCardCode))
            {
                model.MemberCardCode = $"LA-VIP-{DateTime.Now.Year}-{Random.Shared.Next(1000, 9999)}";
            }
            if (string.IsNullOrWhiteSpace(model.PasswordHash))
            {
                model.PasswordHash = "123456";
            }
            model.CreatedAt = DateTime.Now;
            model.IsActive = true;

            _context.AppUsers.Add(model);
            await _context.SaveChangesAsync();

            HttpContext.Session.SetInt32("UserId", model.Id);
            HttpContext.Session.SetString("UserName", $"{model.Title} {model.FullName}".Trim());
            HttpContext.Session.SetString("UserEmail", model.Email);
            HttpContext.Session.SetString("UserPhone", model.Phone);
            HttpContext.Session.SetString("UserRole", model.Role);
            HttpContext.Session.SetString("UserCardCode", model.MemberCardCode);
            HttpContext.Session.SetString("UserTier", model.MemberTier);

            TempData["SuccessMessage"] = $"Kích hoạt thẻ Hội viên L'Atelier Privilège ({model.MemberCardCode}) thành công!";
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            TempData["SuccessMessage"] = "Quý khách đã đăng xuất khỏi cổng Hội viên L'Atelier Privilège.";
            return RedirectToAction("Index", "Home");
        }
    }
}

