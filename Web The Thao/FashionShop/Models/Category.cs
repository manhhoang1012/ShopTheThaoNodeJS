﻿using FashionShop.Models.Base;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace FashionShop.Models
{
    [Table("Category")]
    public class Category : BaseEntity
    {
        [Column(TypeName = "ntext")]
        [MaxLength(50)]
        [DisplayName("Danh mục")]
        public string Name { get; set; }
        [Column(TypeName = "ntext")]
        [DisplayName("Mô tả")]
        public string Description { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
