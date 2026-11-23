using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantWeb.Data;
using RestaurantWeb.Models;
using RestaurantWeb.ViewModels;

namespace RestaurantWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var vm = new HomeViewModel
            {
                Categories = await _context.Categories
                    .Where(c => c.IsActive)
                    .OrderBy(c => c.DisplayOrder)
                    .ToListAsync(),
                FeaturedMenuItems = await _context.MenuItems
                    .Include(m => m.Category)
                    .Where(m => m.IsAvailable && m.IsFeatured)
                    .OrderBy(m => m.CategoryId)
                    .ToListAsync(),
                AvailableTables = await _context.DiningTables
                    .Where(t => t.IsActive)
                    .OrderBy(t => t.Area)
                    .ThenBy(t => t.TableNumber)
                    .ToListAsync(),
                TotalMenuItemsCount = await _context.MenuItems.CountAsync(m => m.IsAvailable),
                AvailableTablesCount = await _context.DiningTables.CountAsync(t => t.IsActive && t.Status == "Trống")
            };

            return View(vm);
        }

        public async Task<IActionResult> Menu(int? categoryId, string? search, string? cat)
        {
            var query = _context.MenuItems
                .Include(m => m.Category)
                .Where(m => m.IsAvailable)
                .AsQueryable();

            if (categoryId.HasValue && categoryId.Value > 0)
            {
                query = query.Where(m => m.CategoryId == categoryId.Value);
            }

            if (!string.IsNullOrWhiteSpace(search))
            {
                var keyword = search.Trim();
                query = query.Where(m =>
                    m.Name.Contains(keyword) ||
                    (m.FrenchSubtitle != null && m.FrenchSubtitle.Contains(keyword)) ||
                    (m.Description != null && m.Description.Contains(keyword)));
            }

            ViewBag.Categories = await _context.Categories.Where(c => c.IsActive).OrderBy(c => c.DisplayOrder).ToListAsync();
            ViewBag.SelectedCategory = categoryId;
            ViewBag.Search = search;
            ViewBag.ActiveTab = cat ?? "all";

            var items = await query.OrderBy(m => m.CategoryId).ThenByDescending(m => m.IsFeatured).ToListAsync();
            return View(items);
        }

        public IActionResult Story()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Contact()
        {
            var model = new ContactInquiry
            {
                FullName = HttpContext.Session.GetString("UserName") ?? string.Empty,
                Phone = HttpContext.Session.GetString("UserPhone") ?? string.Empty,
                Email = HttpContext.Session.GetString("UserEmail") ?? string.Empty,
                EventDate = DateTime.Today.AddDays(3)
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitContact(ContactInquiry model)
        {
            if (string.IsNullOrWhiteSpace(model.FullName) || string.IsNullOrWhiteSpace(model.Phone))
            {
                TempData["ErrorMessage"] = "Quý khách vui lòng điền đầy đủ Họ tên và Số điện thoại liên hệ.";
                return View("Contact", model);
            }

            model.Status = "Mới tiếp nhận";
            model.CreatedAt = DateTime.Now;
            _context.ContactInquiries.Add(model);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = $"Trân trọng cảm ơn {model.FullName}. Yêu cầu đã được chuyển tới Đội ngũ Concierge L'Atelier và sẽ được phản hồi trong vòng 04 giờ làm việc.";
            return RedirectToAction(nameof(Contact));
        }

        public async Task<IActionResult> DishDetail(int id)
        {
            var item = await _context.MenuItems
                .Include(m => m.Category)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (item == null)
            {
                return NotFound();
            }

            ViewBag.RelatedItems = await _context.MenuItems
                .Include(m => m.Category)
                .Where(m => m.CategoryId == item.CategoryId && m.Id != item.Id && m.IsAvailable)
                .Take(3)
                .ToListAsync();

            return View(item);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

