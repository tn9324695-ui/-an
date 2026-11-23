using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using RestaurantWeb.Data;
using RestaurantWeb.Models;
using RestaurantWeb.ViewModels;

namespace RestaurantWeb.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var role = HttpContext.Session.GetString("UserRole");
            if (role != "Admin" && role != "Staff")
            {
                TempData["ErrorMessage"] = "Vui lòng đăng nhập bằng tài khoản Quản trị (Admin) hoặc Concierge (Staff) để truy cập cổng điều hành L'Atelier.";
                context.Result = new RedirectToActionResult("Login", "Account", new { returnUrl = context.HttpContext.Request.Path });
                return;
            }
            base.OnActionExecuting(context);
        }

        public async Task<IActionResult> Index()
        {
            var orderRev = await _context.Orders
                .Where(o => o.Status != "Đã hủy")
                .SumAsync(o => (decimal?)o.TotalAmount) ?? 0;

            var resRev = await _context.Reservations
                .Where(r => r.Status != "Đã hủy")
                .SumAsync(r => (decimal?)r.EstimatedTotal) ?? 0;

            var vm = new AdminDashboardViewModel
            {
                TotalRevenue = orderRev + resRev,
                EstimatedReservationRevenue = resRev,
                TotalOrdersCount = await _context.Orders.CountAsync(),
                PendingOrdersCount = await _context.Orders.CountAsync(o => o.Status == "Chờ xử lý" || o.Status == "Đang chế biến"),
                TotalReservationsCount = await _context.Reservations.CountAsync(),
                PendingReservationsCount = await _context.Reservations.CountAsync(r => r.Status == "Chờ xác nhận"),
                TotalMenuItemsCount = await _context.MenuItems.CountAsync(),
                AvailableTablesCount = await _context.DiningTables.CountAsync(t => t.Status == "Trống"),
                TotalTablesCount = await _context.DiningTables.CountAsync(),
                TotalMembersCount = await _context.AppUsers.CountAsync(),
                PendingInquiriesCount = await _context.ContactInquiries.CountAsync(i => i.Status == "Mới tiếp nhận"),
                RecentOrders = await _context.Orders
                    .Include(o => o.OrderItems)
                    .OrderByDescending(o => o.CreatedAt)
                    .Take(5)
                    .ToListAsync(),
                RecentReservations = await _context.Reservations
                    .Include(r => r.DiningTable)
                    .OrderByDescending(r => r.CreatedAt)
                    .Take(6)
                    .ToListAsync(),
                Tables = await _context.DiningTables
                    .OrderBy(t => t.Area)
                    .ThenBy(t => t.TableNumber)
                    .ToListAsync(),
                RecentInquiries = await _context.ContactInquiries
                    .OrderByDescending(i => i.CreatedAt)
                    .Take(5)
                    .ToListAsync()
            };

            return View(vm);
        }

        // ================= QUẢN LÝ MÓN ĂN =================
        public async Task<IActionResult> MenuItems(int? categoryId)
        {
            var query = _context.MenuItems.Include(m => m.Category).AsQueryable();
            if (categoryId.HasValue && categoryId.Value > 0)
            {
                query = query.Where(m => m.CategoryId == categoryId.Value);
            }
            ViewBag.Categories = await _context.Categories.OrderBy(c => c.DisplayOrder).ToListAsync();
            ViewBag.SelectedCategory = categoryId;
            return View(await query.OrderBy(m => m.CategoryId).ThenBy(m => m.Name).ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> CreateMenuItem()
        {
            ViewBag.Categories = await _context.Categories.OrderBy(c => c.DisplayOrder).ToListAsync();
            return View(new MenuItem { IsAvailable = true, PreparationTimeMinutes = 15, DietaryTags = "GF" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateMenuItem(MenuItem model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _context.Categories.OrderBy(c => c.DisplayOrder).ToListAsync();
                return View(model);
            }

            if (string.IsNullOrWhiteSpace(model.ImageUrl))
            {
                model.ImageUrl = "https://lh3.googleusercontent.com/aida-public/AB6AXuBEd5tRJkWJVuA4QePPkh9kh8S2beqryNbcmEcriDlLOxlIG0mg0cirqBsYI3n5v_AgpLw-cOCzPblRmgTd1lcTH9avTt9sFssIjig-gB7jMlkXjWDUtzGKnXJ09A97R4ILZVP4hZwrYGaWjDbh-UJBbLKRwq2NxzZ_cuhJCo2pJD4FcSOBCtw0aRchulUEiEGWpAdYTYvxnCzdHyi88PtNpsncx8iyEvEOeGiWHNvI";
            }

            _context.MenuItems.Add(model);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = $"Đã thêm tuyệt tác \"{model.Name}\" vào thực đơn L'Atelier!";
            return RedirectToAction(nameof(MenuItems));
        }

        [HttpGet]
        public async Task<IActionResult> EditMenuItem(int id)
        {
            var item = await _context.MenuItems.FindAsync(id);
            if (item == null) return NotFound();

            ViewBag.Categories = await _context.Categories.OrderBy(c => c.DisplayOrder).ToListAsync();
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditMenuItem(MenuItem model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _context.Categories.OrderBy(c => c.DisplayOrder).ToListAsync();
                return View(model);
            }

            _context.MenuItems.Update(model);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = $"Đã cập nhật món ăn \"{model.Name}\" thành công!";
            return RedirectToAction(nameof(MenuItems));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteMenuItem(int id)
        {
            var item = await _context.MenuItems.FindAsync(id);
            if (item != null)
            {
                var hasOrderItems = await _context.OrderItems.AnyAsync(oi => oi.MenuItemId == id);
                if (hasOrderItems)
                {
                    item.IsAvailable = false;
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = $"Món \"{item.Name}\" đã chuyển sang trạng thái Tạm ngưng phục vụ.";
                }
                else
                {
                    _context.MenuItems.Remove(item);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = $"Đã xóa món \"{item.Name}\" khỏi thực đơn.";
                }
            }
            return RedirectToAction(nameof(MenuItems));
        }

        // ================= QUẢN LÝ DANH MỤC =================
        public async Task<IActionResult> Categories()
        {
            var categories = await _context.Categories
                .Include(c => c.MenuItems)
                .OrderBy(c => c.DisplayOrder)
                .ToListAsync();
            return View(categories);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCategory(string name, string? description, int displayOrder = 1)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                _context.Categories.Add(new Category
                {
                    Name = name.Trim(),
                    Description = description,
                    DisplayOrder = displayOrder,
                    IsActive = true
                });
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = $"Đã thêm danh mục \"{name}\"!";
            }
            return RedirectToAction(nameof(Categories));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.Categories.Include(c => c.MenuItems).FirstOrDefaultAsync(c => c.Id == id);
            if (category != null)
            {
                if (category.MenuItems.Any())
                {
                    TempData["ErrorMessage"] = "Không thể xóa danh mục đang chứa món ăn!";
                }
                else
                {
                    _context.Categories.Remove(category);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Đã xóa danh mục.";
                }
            }
            return RedirectToAction(nameof(Categories));
        }

        // ================= QUẢN LÝ ĐƠN ĐẶT MÓN =================
        public async Task<IActionResult> Orders(string? status)
        {
            var query = _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.MenuItem)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(status))
            {
                query = query.Where(o => o.Status == status);
            }

            ViewBag.SelectedStatus = status;
            return View(await query.OrderByDescending(o => o.CreatedAt).ToListAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateOrderStatus(int id, string status)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                order.Status = status;
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = $"Đã cập nhật đơn hàng {order.OrderCode} sang trạng thái: {status}.";
            }
            return RedirectToAction(nameof(Orders));
        }

        // ================= QUẢN LÝ ĐẶT BÀN 6 BƯỚC & SƠ ĐỒ BÀN =================
        public async Task<IActionResult> Reservations(string? status)
        {
            var query = _context.Reservations
                .Include(r => r.DiningTable)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(status))
            {
                query = query.Where(r => r.Status == status);
            }

            ViewBag.SelectedStatus = status;
            ViewBag.Tables = await _context.DiningTables.OrderBy(t => t.TableNumber).ToListAsync();
            return View(await query.OrderByDescending(r => r.ReservationDate).ThenByDescending(r => r.CreatedAt).ToListAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateReservationStatus(int id, string status, int? diningTableId)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation != null)
            {
                reservation.Status = status;
                if (diningTableId.HasValue)
                {
                    reservation.DiningTableId = diningTableId.Value;
                    var table = await _context.DiningTables.FindAsync(diningTableId.Value);
                    if (table != null)
                    {
                        if (status == "Đã xác nhận") table.Status = "Đã đặt";
                        else if (status == "Đã nhận bàn") table.Status = "Đang phục vụ";
                        else if (status == "Đã hủy") table.Status = "Trống";
                    }
                }
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = $"Đã cập nhật phiếu đặt tiệc #LA-{id:D4} sang trạng thái: {status}.";
            }
            return RedirectToAction(nameof(Reservations));
        }

        public async Task<IActionResult> Tables()
        {
            var tables = await _context.DiningTables
                .OrderBy(t => t.Area)
                .ThenBy(t => t.TableNumber)
                .ToListAsync();
            return View(tables);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateTableStatus(int id, string status)
        {
            var table = await _context.DiningTables.FindAsync(id);
            if (table != null)
            {
                table.Status = status;
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = $"Đã chuyển {table.TableNumber} sang trạng thái: {status}.";
            }
            return RedirectToAction(nameof(Tables));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTable(string tableNumber, int capacity, string area)
        {
            if (!string.IsNullOrWhiteSpace(tableNumber))
            {
                _context.DiningTables.Add(new DiningTable
                {
                    TableNumber = tableNumber.Trim(),
                    Capacity = capacity,
                    Area = area,
                    Status = "Trống",
                    IsActive = true
                });
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = $"Đã thêm bàn mới: {tableNumber}.";
            }
            return RedirectToAction(nameof(Tables));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateInquiryStatus(int id, string status)
        {
            var inquiry = await _context.ContactInquiries.FindAsync(id);
            if (inquiry != null)
            {
                inquiry.Status = status;
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = $"Đã cập nhật yêu cầu Concierge của {inquiry.FullName} sang: {status}.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

