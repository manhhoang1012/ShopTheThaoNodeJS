using FashionShop.Models.Base;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace FashionShop.Models
{
    [Table("Customer")]
    public class Customer : BaseEntity
    {
        [Column(TypeName = "ntext")]
        [MaxLength(50)]
        [DisplayName("Tên khách hàng")]
        public string Name { get; set; }
        [DisplayName("Email")]
        public string Email { get; set;}
        [DisplayName("Số điện thoại")]
        public string Phone { get; set; }
        [Column(TypeName = "ntext")]
        [MaxLength(50)]
        [DisplayName("Địa chỉ")]
        public string Address { get; set; }
        [MaxLength(50)]
        [DisplayName("Mật khẩu")]
        public string Password { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
