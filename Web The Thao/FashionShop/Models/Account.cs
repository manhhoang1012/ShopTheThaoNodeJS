using FashionShop.Models.Base;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace FashionShop.Models
{
    [Table("Account")]
    public class Account : BaseEntity
    {
        [Column(TypeName ="ntext")]
        [MaxLength(50)]
        [DisplayName("Tên người dùng")]
        public string Name { get; set; }
        [MaxLength(50)]
        [DisplayName("Email")]
        public string Email { get; set; }
        [MaxLength(20)]
        [DisplayName("Số điện thoại")]
        public string Phone { get; set; }
        [MaxLength(50)]
        [DisplayName("Mật khẩu")]
        public string Password { get; set; }
        [MaxLength(50)]
        [DisplayName("Vai trò")]
        public string Role { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
