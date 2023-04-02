using FashionShop.Models.Base;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using FashionShop.Shared;
using System.Collections.Generic;

namespace FashionShop.Models
{
    [Table("Order")]
    public class Order : BaseEntity
    {
        [DisplayName("Tên người nhận")]
        public string Name { get; set; }
        [DisplayName("Giảm giá")]
        public decimal? Discount { get; set; }


        [DisplayName("Số lượng")]
        public int? Quantity { get; set; }
        [DisplayName("Ngày giao")]
        public DateTime? Shipdate { get; set; }
        [DisplayName("Số điện thoại")]
        public string Phone { get; set; }
        [Column(TypeName = "ntext")]
        [MaxLength(50)]
        [DisplayName("Địa chỉ")]
        public string Address { get; set; }

        [DisplayName("Tổng tiền")]
        public decimal? Total { get; set; }

        [DisplayName("Trạng thái")]
        [DefaultValue("Chờ xác nhận")]
        public string Status { get; set; } = StatusConst.WaitConfirm;
        [DisplayName("Sản phẩm")]
        public string ProductId { get; set; }
        // Sản phẩm
        [ForeignKey("ProductId")]
        public Product Product { set; get; }
        // Người đặt hàng
        public string CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public Customer Customer { set; get; }

        // người xử lí đơn hàng
        // Sản phẩm
        [ForeignKey("UpdateUserId")]
        public Account Account { set; get; }


    }
}
