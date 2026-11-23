using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantWeb.Data;
using RestaurantWeb.Helpers;
using RestaurantWeb.Models;
using RestaurantWeb.ViewModels;

namespace RestaurantWeb.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;
        private const string CartSessionKey = "RESTAURANT_CART";

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }

        private List<CartItem> GetCart()
        {
            return HttpContext.Session.GetObjectFromJson<List<CartItem>>(CartSessionKey) ?? new List<CartItem>();
        }

        private void SaveCart(List<CartItem> cart)
        {
            HttpContext.Session.SetObjectAsJson(CartSessionKey, cart);
            HttpContext.Session.SetInt32("CartCount", cart.Sum(x => x.Quantity));
        }

        public IActionResult Index()
        {
            var cart = GetCart();
            return View(cart);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int menuItemId, int quantity = 1, string? note = null, string? returnUrl = null)
        {
            var dish = await _context.MenuItems
                .Include(m => m.Category)
                .FirstOrDefaultAsync(m => m.Id == menuItemId && m.IsAvailable);

            if (dish == null)
            {
                TempData["ErrorMessage"] = "Món ăn không tồn tại hoặc tạm ngưng phục vụ.";
                return RedirectToAction("Menu", "Home");
            }

            var cart = GetCart();
            var existing = cart.FirstOrDefault(c => c.MenuItemId == menuItemId);

            if (existing != null)
            {
                existing.Quantity += Math.Max(1, quantity);
                if (!string.IsNullOrWhiteSpace(note))
                {
                    existing.Note = note;
                }
            }
            else
            {
                cart.Add(new CartItem
                {
                    MenuItemId = dish.Id,
                    Name = dish.Name,
                    ImageUrl = dish.ImageUrl,
                    CategoryName = dish.Category?.Name ?? "Thực đơn L'Atelier",
                    UnitPrice = dish.EffectivePrice,
                    Quantity = Math.Max(1, quantity),
                    Note = note
                });
            }

            SaveCart(cart);
            TempData["SuccessMessage"] = $"Đã thêm \"{dish.Name}\" vào danh sách chọn món của quý khách!";

            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult UpdateQuantity(int menuItemId, int quantity)
        {
            var cart = GetCart();
            var item = cart.FirstOrDefault(c => c.MenuItemId == menuItemId);
            if (item != null)
            {
                if (quantity <= 0) cart.Remove(item);
                else item.Quantity = quantity;
                SaveCart(cart);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Remove(int menuItemId)
        {
            var cart = GetCart();
            cart.RemoveAll(c => c.MenuItemId == menuItemId);
            SaveCart(cart);
            TempData["SuccessMessage"] = "Đã xóa món khỏi danh sách.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Clear()
        {
            SaveCart(new List<CartItem>());
            TempData["SuccessMessage"] = "Đã làm trống giỏ chọn món.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Checkout()
        {
            var cart = GetCart();
            if (!cart.Any())
            {
                TempData["ErrorMessage"] = "Danh sách món đang trống. Vui lòng chọn món từ thực đơn.";
                return RedirectToAction("Menu", "Home");
            }

            var vm = new CheckoutViewModel
            {
                CartItems = cart,
                OrderInfo = new Order
                {
                    CustomerName = HttpContext.Session.GetString("UserName") ?? string.Empty,
                    CustomerPhone = HttpContext.Session.GetString("UserPhone") ?? string.Empty,
                    CustomerAddress = HttpContext.Session.GetString("UserAddress") ?? "Dùng tại bàn L'Atelier (128 Đồng Khởi)",
                    OrderType = "Dùng tại bàn L'Atelier",
                    PaymentMethod = "Thanh toán tại bàn"
                }
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout(CheckoutViewModel model)
        {
            var cart = GetCart();
            if (!cart.Any())
            {
                return RedirectToAction("Menu", "Home");
            }

            if (string.IsNullOrWhiteSpace(model.OrderInfo.CustomerName) || string.IsNullOrWhiteSpace(model.OrderInfo.CustomerPhone))
            {
                TempData["ErrorMessage"] = "Vui lòng nhập đầy đủ Họ tên và Số điện thoại.";
                model.CartItems = cart;
                return View(model);
            }

            var orderCountToday = await _context.Orders.CountAsync(o => o.CreatedAt.Date == DateTime.Today) + 1;
            var order = new Order
            {
                OrderCode = $"LA-{DateTime.Now:yyMMdd}-{orderCountToday:D3}",
                CustomerName = model.OrderInfo.CustomerName.Trim(),
                CustomerPhone = model.OrderInfo.CustomerPhone.Trim(),
                CustomerAddress = string.IsNullOrWhiteSpace(model.OrderInfo.CustomerAddress)
                    ? "Dùng tại bàn L'Atelier"
                    : model.OrderInfo.CustomerAddress.Trim(),
                OrderType = model.OrderInfo.OrderType,
                PaymentMethod = model.OrderInfo.PaymentMethod,
                Status = "Chờ xử lý",
                Note = model.OrderInfo.Note,
                TotalAmount = cart.Sum(x => x.SubTotal),
                CreatedAt = DateTime.Now,
                AppUserId = HttpContext.Session.GetInt32("UserId"),
                OrderItems = cart.Select(c => new OrderItem
                {
                    MenuItemId = c.MenuItemId,
                    Quantity = c.Quantity,
                    UnitPrice = c.UnitPrice,
                    Note = c.Note
                }).ToList()
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            SaveCart(new List<CartItem>());
            TempData["SuccessMessage"] = $"Gửi yêu cầu gọi món thành công! Mã đơn của quý khách là {order.OrderCode}.";

            return RedirectToAction(nameof(OrderSuccess), new { id = order.Id });
        }

        public async Task<IActionResult> OrderSuccess(int id)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.MenuItem)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null) return NotFound();
            return View(order);
        }
    }
}

