using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantWeb.Models
{
    public class Reservation
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập danh xưng và họ tên")]
        [StringLength(120)]
        [Display(Name = "Danh xưng & Họ tên")]
        public string CustomerName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập số điện thoại liên lạc")]
        [StringLength(30)]
        [Display(Name = "Số điện thoại")]
        public string CustomerPhone { get; set; } = string.Empty;

        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        [StringLength(150)]
        [Display(Name = "Địa chỉ Email")]
        public string? CustomerEmail { get; set; }

        [StringLength(250)]
        [Display(Name = "Cơ quan / Xuất hóa đơn VAT")]
        public string? CompanyVatInfo { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn ngày dùng tiệc")]
        [DataType(DataType.Date)]
        [Display(Name = "Ngày phục vụ")]
        public DateTime ReservationDate { get; set; } = DateTime.Today.AddDays(1);

        [Required(ErrorMessage = "Vui lòng chọn khung giờ")]
        [StringLength(40)]
        [Display(Name = "Khung giờ")]
        public string ReservationTime { get; set; } = "19:30 (Tối)";

        [Required]
        [Range(1, 50)]
        [Display(Name = "Số lượng thực khách")]
        public int NumberOfGuests { get; set; } = 2;

        [StringLength(80)]
        [Display(Name = "Nhóm thực khách")]
        public string GuestGroupLabel { get; set; } = "2 khách (Duo)";

        [StringLength(120)]
        [Display(Name = "Không gian ngồi mong muốn")]
        public string SeatingArea { get; set; } = "Bàn Cạnh Cửa Sổ Hướng Vườn";

        [StringLength(150)]
        [Display(Name = "Trải nghiệm ẩm thực lựa chọn")]
        public string SelectedMenuPackage { get; set; } = "Tasting Menu Prestige (7 Món)";

        [Display(Name = "Sommelier Grand Cru Wine Pairing")]
        public bool HasWinePairing { get; set; } = false;

        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Tạm tính bàn tiệc (VNĐ)")]
        public decimal EstimatedTotal { get; set; } = 5300000;

        [StringLength(100)]
        [Display(Name = "Dịp đặc biệt")]
        public string Occasion { get; set; } = "Hẹn hò lãng mạn";

        [StringLength(600)]
        [Display(Name = "Lưu ý dị ứng & Yêu cầu đặc biệt")]
        public string? SpecialRequests { get; set; }

        [StringLength(50)]
        [Display(Name = "Trạng thái")]
        public string Status { get; set; } = "Chờ xác nhận";

        [Display(Name = "Bàn được xếp")]
        public int? DiningTableId { get; set; }

        public DiningTable? DiningTable { get; set; }

        [Display(Name = "Thời gian tạo")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}

