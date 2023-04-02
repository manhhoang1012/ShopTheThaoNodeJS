using FashionShop.Models.Base;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace FashionShop.Models
{
    [Table("Product")]
    public class Product : BaseEntity
    {
        [Column(TypeName = "ntext")]
        [MaxLength(100)]
        [DisplayName("Tên sản phẩm")]
        public string Name { get; set; }

        [DisplayName("Mã")]
        public string Code { get; set; }
        [Column(TypeName = "ntext")]
        [DisplayName("Mô tả")]
        public string Description { get; set; }
        [DisplayName("Ảnh")]
        public string Image { get; set; }
        [DisplayName("Giá bán")]
        public decimal? Price { get; set; }
        [DisplayName("Giảm giá")]
        public decimal? Discount { get; set; }
        [DisplayName("Số lượng")]
        public int? Quantity { get; set; }
        [DisplayName("Danh mục")]
        public string CategoryId { get; set; }

        [DisplayName("Thương hiệu")]
        public string BranchId { get; set; }
        [ForeignKey("BranchId")]
        public Branch Branch { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        public ICollection<Order> Orders { get; set; }
        public ICollection<Cart> Carts { get; set; }
    }
}
