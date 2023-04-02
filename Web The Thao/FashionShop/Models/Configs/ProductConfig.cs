using System.Data.Entity.ModelConfiguration;
using FashionShop.Models;

namespace FashionShop.Models.Configs
{
    public class ProductConfig : EntityTypeConfiguration<Product>
    {
        public ProductConfig()
        {
            this.HasMany<Order>(x => x.Orders);
            this.HasMany<Cart>(x => x.Carts);
        }
    }
}
