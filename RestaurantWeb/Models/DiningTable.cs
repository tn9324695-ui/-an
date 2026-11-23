using System.ComponentModel.DataAnnotations;

namespace RestaurantWeb.Models
{
    public class DiningTable
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mã/tên bàn")]
        [StringLength(50)]
        [Display(Name = "Tên bàn")]
        public string TableNumber { get; set; } = string.Empty;

        [Required]
        [Range(1, 30)]
        [Display(Name = "Sức chứa (người)")]
        public int Capacity { get; set; } = 4;

        [Required]
        [StringLength(100)]
        [Display(Name = "Không gian")]
        public string Area { get; set; } = "Bàn Cạnh Cửa Sổ Hướng Vườn";

        [Required]
        [StringLength(50)]
        [Display(Name = "Trạng thái")]
        public string Status { get; set; } = "Trống";

        [Display(Name = "Đang sử dụng")]
        public bool IsActive { get; set; } = true;

        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}

