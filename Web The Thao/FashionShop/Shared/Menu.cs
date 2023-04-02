using FashionShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FashionShop.Shared
{
    public class Menu
    {
        public virtual List<Category> Categories { get; set; }
        public virtual List<Branch> Branchs { get; set; }
    }
}