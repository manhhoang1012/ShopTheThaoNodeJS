using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace FashionShop.Models
{
    public class CartItem
    {
        [DisplayName("Số lượng")]
        public int Quantity { set; get; }
        [DisplayName("Sản phẩm")]
        public Product Product { set; get; }
    }
}