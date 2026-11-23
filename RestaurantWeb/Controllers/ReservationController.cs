using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantWeb.Data;
using RestaurantWeb.Models;

namespace RestaurantWeb.Controllers
{
    public class ReservationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReservationController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? tableId, string? menuPackage)
        {
            ViewBag.Tables = await _context.DiningTables
                .Where(t => t.IsActive)
                .OrderBy(t => t.Area)
                .ThenBy(t => t.TableNumber)
                .ToListAsync();

            var model = new Reservation
            {
                ReservationDate = DateTime.Today.AddDays(1),
                ReservationTime = "19:30 (Tối)",
                NumberOfGuests = 2,
                GuestGroupLabel = "2 khách (Duo)",
                SeatingArea = "Bàn Cạnh Cửa Sổ Hướng Vườn",
                SelectedMenuPackage = !string.IsNullOrWhiteSpace(menuPackage) ? menuPackage : "Tasting Menu Prestige (7 Món)",
                HasWinePairing = false,
                EstimatedTotal = 5300000,
                Occasion = "Hẹn hò lãng mạn",
                DiningTableId = tableId
            };

            var userName = HttpContext.Session.GetString("UserName");
            var userPhone = HttpContext.Session.GetString("UserPhone");
            var userEmail = HttpContext.Session.GetString("UserEmail");

            if (!string.IsNullOrEmpty(userName)) model.CustomerName = userName;
            if (!string.IsNullOrEmpty(userPhone)) model.CustomerPhone = userPhone;
            if (!string.IsNullOrEmpty(userEmail)) model.CustomerEmail = userEmail;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Reservation model)
        {
            if (string.IsNullOrWhiteSpace(model.CustomerName) || string.IsNullOrWhiteSpace(model.CustomerPhone))
            {
                TempData["ErrorMessage"] = "Vui lòng nhập đầy đủ Danh xưng, Họ tên và Số điện thoại liên hệ.";
                ViewBag.Tables = await _context.DiningTables.Where(t => t.IsActive).ToListAsync();
                return View("Index", model);
            }

            model.Status = "Chờ xác nhận";
            model.CreatedAt = DateTime.Now;

            // Tự động gán bàn trống phù hợp theo Không gian ngồi (SeatingArea) nếu chưa chọn bàn cụ thể
            if (!model.DiningTableId.HasValue && !string.IsNullOrWhiteSpace(model.SeatingArea))
            {
                var matchingTable = await _context.DiningTables
                    .FirstOrDefaultAsync(t => t.IsActive && t.Status == "Trống" && t.Area.Contains(model.SeatingArea));
                if (matchingTable != null)
                {
                    model.DiningTableId = matchingTable.Id;
                    matchingTable.Status = "Đã đặt";
                }
            }
            else if (model.DiningTableId.HasValue)
            {
                var table = await _context.DiningTables.FindAsync(model.DiningTableId.Value);
                if (table != null && table.Status == "Trống")
                {
                    table.Status = "Đã đặt";
                }
            }

            _context.Reservations.Add(model);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = $"Đặt bàn thành công! Mã phiếu đặt tiệc L'Atelier của quý khách là #LA-{model.Id:D4}.";
            return RedirectToAction(nameof(BookingSuccess), new { id = model.Id });
        }

        public async Task<IActionResult> BookingSuccess(int id)
        {
            var reservation = await _context.Reservations
                .Include(r => r.DiningTable)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        [HttpGet]
        public async Task<IActionResult> Lookup(string? phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
            {
                phone = HttpContext.Session.GetString("UserPhone");
            }

            ViewBag.Phone = phone;

            if (!string.IsNullOrWhiteSpace(phone))
            {
                var cleanPhone = phone.Trim();
                ViewBag.Reservations = await _context.Reservations
                    .Include(r => r.DiningTable)
                    .Where(r => r.CustomerPhone.Contains(cleanPhone))
                    .OrderByDescending(r => r.CreatedAt)
                    .ToListAsync();

                ViewBag.Orders = await _context.Orders
                    .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.MenuItem)
                    .Where(o => o.CustomerPhone.Contains(cleanPhone))
                    .OrderByDescending(o => o.CreatedAt)
                    .ToListAsync();
            }

            return View();
        }
    }
}

