using System.ComponentModel.DataAnnotations;

namespace RestaurantWeb.Models
{
    public class ContactInquiry
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập họ và tên")]
        [StringLength(120)]
        [Display(Name = "Họ và tên")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        [StringLength(30)]
        [Display(Name = "Số điện thoại / WhatsApp")]
        public string Phone { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập email")]
        [EmailAddress]
        [StringLength(150)]
        [Display(Name = "Địa chỉ Email")]
        public string Email { get; set; } = string.Empty;

        [StringLength(100)]
        [Display(Name = "Mục đích liên hệ")]
        public string Purpose { get; set; } = "Tiệc riêng tư (Private Dining)";

        [StringLength(100)]
        [Display(Name = "Quy mô khách dự kiến")]
        public string? EstimatedGuests { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Thời gian dự kiến")]
        public DateTime? EventDate { get; set; }

        [StringLength(1000)]
        [Display(Name = "Lời nhắn / Yêu cầu thực đơn")]
        public string? Message { get; set; }

        [StringLength(50)]
        [Display(Name = "Trạng thái xử lý")]
        public string Status { get; set; } = "Mới tiếp nhận";

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}

