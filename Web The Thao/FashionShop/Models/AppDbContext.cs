using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Objects;
using System.Linq;
using FashionShop.Models;
using FashionShop.Models.Configs;
using System.ComponentModel;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace FashionShop
{
    public class AppDbContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Branch> Branchs { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public AppDbContext() : base("ConnectionString")
        {
            Database.SetInitializer<AppDbContext>(new CreateDatabaseIfNotExists<AppDbContext>());
           // Database.SetInitializer(new MigrateDatabaseToLatestVersion<AppDbContext, FashionShop.Migrations.Configuration>());
        }
        public class AppDbInitializer : CreateDatabaseIfNotExists<AppDbContext>
        {
            protected override void Seed(AppDbContext context)
            {
                base.Seed(context);
            }
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AccountConfig());
            modelBuilder.Configurations.Add(new ProductConfig());
            modelBuilder.Configurations.Add(new CategoryConfig());
            modelBuilder.Configurations.Add(new BranchConfig());
            modelBuilder.Configurations.Add(new CustomerConfig());
            modelBuilder.Configurations.Add(new OrderConfig()); 
            modelBuilder.Configurations.Add(new CartConfig());
        }

  
    }
}