using Microsoft.EntityFrameworkCore;
using Store.Data.Context.Configurations;
using Store.Domain;

namespace Store.Data.Context
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options)
            : base(options)
        { }

        public DbSet<Product> Products { get; set; }
        public DbSet<ItemProduct> ItemProducts { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<User> Users { get; set; }

        private void AddModelConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new ItemProductConfiguration());
            modelBuilder.ApplyConfiguration(new PurchaseConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {       
            AddModelConfiguration(modelBuilder);

            modelBuilder.Entity<Product>().HasMany(a => a.PurchaseItens).WithOne(s => s.Product);
            modelBuilder.Entity<Purchase>().HasMany(a => a.Itens).WithOne(s => s.Purchase);
            modelBuilder.Entity<User>().HasMany(a => a.Purchases).WithOne(s => s.User);
        }
    }
}
