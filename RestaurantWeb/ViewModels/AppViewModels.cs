using RestaurantWeb.Models;

namespace RestaurantWeb.ViewModels
{
    public class HomeViewModel
    {
        public List<Category> Categories { get; set; } = new();
        public List<MenuItem> FeaturedMenuItems { get; set; } = new();
        public List<DiningTable> AvailableTables { get; set; } = new();
        public Reservation QuickReservation { get; set; } = new();
        public int TotalMenuItemsCount { get; set; }
        public int AvailableTablesCount { get; set; }
    }

    public class AdminDashboardViewModel
    {
        public decimal TotalRevenue { get; set; }
        public decimal EstimatedReservationRevenue { get; set; }
        public int TotalOrdersCount { get; set; }
        public int PendingOrdersCount { get; set; }
        public int TotalReservationsCount { get; set; }
        public int PendingReservationsCount { get; set; }
        public int TotalMenuItemsCount { get; set; }
        public int AvailableTablesCount { get; set; }
        public int TotalTablesCount { get; set; }
        public int TotalMembersCount { get; set; }
        public int PendingInquiriesCount { get; set; }
        public List<Order> RecentOrders { get; set; } = new();
        public List<Reservation> RecentReservations { get; set; } = new();
        public List<DiningTable> Tables { get; set; } = new();
        public List<ContactInquiry> RecentInquiries { get; set; } = new();
    }

    public class CheckoutViewModel
    {
        public List<CartItem> CartItems { get; set; } = new();
        public decimal TotalAmount => CartItems.Sum(x => x.SubTotal);
        public Order OrderInfo { get; set; } = new();
    }
}

