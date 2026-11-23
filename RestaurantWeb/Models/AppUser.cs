using System.ComponentModel.DataAnnotations;

namespace RestaurantWeb.Models
{
    public class AppUser
    {
        public int Id { get; set; }

        [StringLength(30)]
        [Display(Name = "Danh xưng")]
        public string Title { get; set; } = "Ông";

        [Required(ErrorMessage = "Vui lòng nhập họ và tên")]
        [StringLength(100)]
        [Display(Name = "Họ và tên")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập email")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        [StringLength(150)]
        [Display(Name = "Email danh dự")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        [StringLength(25)]
        [Display(Name = "Số điện thoại")]
        public string Phone { get; set; } = string.Empty;

        [StringLength(50)]
        [Display(Name = "Mã Thẻ Hội Viên L'Atelier")]
        public string MemberCardCode { get; set; } = "LA-VIP-2025-8869";

        [StringLength(50)]
        [Display(Name = "Hạng Hội Viên")]
        public string MemberTier { get; set; } = "Privilège Noir";

        [StringLength(10)]
        [Display(Name = "Mã PIN Concierge (6 số)")]
        public string? ConciergePin { get; set; } = "886999";

        [DataType(DataType.Date)]
        [Display(Name = "Ngày sinh nhật")]
        public DateTime? DateOfBirth { get; set; }

        [StringLength(250)]
        [Display(Name = "Khẩu vị & Kiêng khem")]
        public string? DietaryPreferences { get; set; }

        [StringLength(150)]
        [Display(Name = "Gu Rượu Vang Ưu Tiên")]
        public string? WinePreference { get; set; }

        [StringLength(150)]
        [Display(Name = "Vị Trí Bàn Thường Trực")]
        public string? PreferredSeating { get; set; }

        [Required]
        [StringLength(250)]
        public string PasswordHash { get; set; } = string.Empty;

        [Required]
        [StringLength(30)]
        [Display(Name = "Vai trò")]
        public string Role { get; set; } = "Customer"; // Admin, Staff, Customer

        [StringLength(250)]
        [Display(Name = "Địa chỉ")]
        public string? Address { get; set; }

        [Display(Name = "Ngày gia nhập")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Display(Name = "Hoạt động")]
        public bool IsActive { get; set; } = true;

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}

