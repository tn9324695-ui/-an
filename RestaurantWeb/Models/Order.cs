using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantWeb.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "Mã đơn")]
        public string OrderCode { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập họ tên")]
        [StringLength(100)]
        [Display(Name = "Họ tên thực khách")]
        public string CustomerName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        [StringLength(20)]
        [Display(Name = "Số điện thoại")]
        public string CustomerPhone { get; set; } = string.Empty;

        [StringLength(300)]
        [Display(Name = "Địa chỉ / Vị trí bàn")]
        public string? CustomerAddress { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Hình thức phục vụ")]
        public string OrderType { get; set; } = "Dùng tại bàn L'Atelier";

        [Required]
        [StringLength(50)]
        [Display(Name = "Phương thức thanh toán")]
        public string PaymentMethod { get; set; } = "Thanh toán tại bàn";

        [Required]
        [StringLength(50)]
        [Display(Name = "Trạng thái")]
        public string Status { get; set; } = "Chờ xử lý";

        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Tổng tiền (VNĐ)")]
        public decimal TotalAmount { get; set; }

        [StringLength(500)]
        [Display(Name = "Ghi chú cho bếp")]
        public string? Note { get; set; }

        [Display(Name = "Thời gian đặt")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public int? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}

