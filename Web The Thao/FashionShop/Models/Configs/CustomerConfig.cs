using System.Data.Entity.ModelConfiguration;
using FashionShop.Models;

namespace FashionShop.Models.Configs
{
    public class CustomerConfig : EntityTypeConfiguration<Customer>
    {
        public CustomerConfig()
        {
            this.HasMany<Order>(x => x.Orders);
        }
    }
}

