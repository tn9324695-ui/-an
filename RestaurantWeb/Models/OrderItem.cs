using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantWeb.Models
{
    public class OrderItem
    {
        public int Id { get; set; }

        public int OrderId { get; set; }
        public Order? Order { get; set; }

        public int MenuItemId { get; set; }
        public MenuItem? MenuItem { get; set; }

        [Range(1, 100)]
        [Display(Name = "Số lượng")]
        public int Quantity { get; set; } = 1;

        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Đơn giá")]
        public decimal UnitPrice { get; set; }

        [StringLength(200)]
        [Display(Name = "Ghi chú món")]
        public string? Note { get; set; }

        [NotMapped]
        public decimal SubTotal => Quantity * UnitPrice;
    }
}

