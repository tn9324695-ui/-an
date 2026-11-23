using System.ComponentModel.DataAnnotations;

namespace RestaurantWeb.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên danh mục")]
        [StringLength(100)]
        [Display(Name = "Tên danh mục")]
        public string Name { get; set; } = string.Empty;

        [StringLength(300)]
        [Display(Name = "Mô tả")]
        public string? Description { get; set; }

        [StringLength(50)]
        [Display(Name = "Mã phân loại (starters, mains, desserts, wines)")]
        public string IconCss { get; set; } = "starters";

        [Display(Name = "Thứ tự hiển thị")]
        public int DisplayOrder { get; set; } = 1;

        [Display(Name = "Đang hoạt động")]
        public bool IsActive { get; set; } = true;

        public ICollection<MenuItem> MenuItems { get; set; } = new List<MenuItem>();
    }
}

