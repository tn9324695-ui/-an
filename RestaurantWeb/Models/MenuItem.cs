using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantWeb.Models
{
    public class MenuItem
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên món ăn")]
        [StringLength(150)]
        [Display(Name = "Tên món ăn (Tiếng Việt)")]
        public string Name { get; set; } = string.Empty;

        [StringLength(180)]
        [Display(Name = "Tên phụ đề Pháp (French Subtitle)")]
        public string? FrenchSubtitle { get; set; }

        [StringLength(800)]
        [Display(Name = "Mô tả nguyên liệu & chế biến")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập giá bán")]
        [Range(1000, 100000000, ErrorMessage = "Giá bán phải từ 1.000đ trở lên")]
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Giá phục vụ (VNĐ)")]
        public decimal Price { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Giá ưu đãi hội viên (VNĐ)")]
        public decimal? DiscountPrice { get; set; }

        [StringLength(600)]
        [Display(Name = "Đường dẫn hình ảnh")]
        public string? ImageUrl { get; set; }

        [StringLength(100)]
        [Display(Name = "Nhãn chế độ ăn (GF, Nuts, Signature...)")]
        public string? DietaryTags { get; set; }

        [StringLength(200)]
        [Display(Name = "Gợi ý Sommelier Wine Pairing")]
        public string? WinePairingNote { get; set; }

        [Display(Name = "Tuyệt tác theo mùa (Hiển thị Trang chủ)")]
        public bool IsFeatured { get; set; } = false;

        [Display(Name = "Đang phục vụ")]
        public bool IsAvailable { get; set; } = true;

        [Display(Name = "Thời gian chuẩn bị (phút)")]
        public int PreparationTimeMinutes { get; set; } = 15;

        [Display(Name = "Độ cay (0-3)")]
        [Range(0, 3)]
        public int SpicyLevel { get; set; } = 0;

        [Required(ErrorMessage = "Vui lòng chọn danh mục")]
        [Display(Name = "Danh mục")]
        public int CategoryId { get; set; }

        public Category? Category { get; set; }

        [NotMapped]
        public decimal EffectivePrice => (DiscountPrice.HasValue && DiscountPrice.Value > 0 && DiscountPrice.Value < Price)
            ? DiscountPrice.Value
            : Price;
    }
}

