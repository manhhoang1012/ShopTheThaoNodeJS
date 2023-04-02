using System.Data.Entity.ModelConfiguration;

namespace FashionShop.Models.Configs
{
    public class AccountConfig : EntityTypeConfiguration<Account>
    {
        public AccountConfig()
        {
            this.Property(x => x.Name).HasColumnType("ntext").HasMaxLength(250);
            this.Property(x => x.Email).HasMaxLength(250);
            this.Property(x => x.Phone).HasMaxLength(250);
            this.Property(x => x.Password).HasMaxLength(250);
            this.HasMany<Order>(x => x.Orders);
        }
    }
}
