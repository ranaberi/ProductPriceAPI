using Microsoft.EntityFrameworkCore;
using ProductPriceAPI.Models;

namespace ProductPriceAPI.Persistence.Contexts
{
    public class AppDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Retailer> Retailers { get; set; }
        public DbSet<ProductPrice> ProductPrices { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).IsRequired().ValueGeneratedOnAdd();
                entity.Property(e => e.Name).IsRequired().HasMaxLength(30);
                entity.Property(e => e.Description);
                entity.Property(e => e.EAN).IsRequired().HasMaxLength(13);
                entity.Property(e => e.IssuingCountry);

                entity.HasMany(p => p.ProductPrices)
                    .WithOne(pp => pp.Product)
                    .HasForeignKey(pp => pp.ProductId);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).IsRequired().ValueGeneratedOnAdd();
                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<ProductPrice>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).IsRequired().ValueGeneratedOnAdd();
                entity.Property(e => e.Price).IsRequired().HasColumnType("decimal(18,2)");
                entity.Property(e => e.Currency).IsRequired();
                entity.Property(e => e.Date);

                entity.HasOne(pp => pp.Product)
                    .WithMany(p => p.ProductPrices)
                    .HasForeignKey(pp => pp.ProductId);

                entity.HasOne(pp => pp.Retailer)
                    .WithMany(r => r.ProductPrices)
                    .HasForeignKey(pp => pp.RetailerId);
            });

            modelBuilder.Entity<Retailer>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).IsRequired().ValueGeneratedOnAdd();
                entity.Property(e => e.Name).IsRequired();

                entity.HasMany(r => r.ProductPrices)
                    .WithOne(pp => pp.Retailer)
                    .HasForeignKey(pp => pp.RetailerId);
            });

            modelBuilder.Entity<Category>().HasData
                (
                    new Category { Id = 100, Name = "Electronics" },
                    new Category { Id = 101, Name = "Books" },
                    new Category { Id = 102, Name = "Clothing" }
                 );

            modelBuilder.Entity<Retailer>().HasData
                (
                    new Retailer { Id = 100, Name = "Amazon" },
                    new Retailer { Id = 101, Name = "Walmart" },
                    new Retailer { Id = 102, Name = "Bol" },
                    new Retailer { Id = 1, Name = "Retailer 1" },
                    new Retailer { Id = 2, Name = "Retailer 2" },
                    new Retailer { Id = 3, Name = "Retailer 3" }
                );

            modelBuilder.Entity<Product>().HasData
                (
                    new Product { Id = 100, Name = "Samsung Galaxy S10", Description = "Smartphone", EAN = "8801643724253", CategoryId = 100, IssuingCountry = "South Korea" },
                    new Product { Id = 101, Name = "Apple iPhone 11", Description = "Smartphone", EAN = "190199222406", CategoryId = 100, IssuingCountry = "United States" },
                    new Product { Id = 105, Name = "Levi's 501 Original Fit Jeans", Description = "Jeans", EAN = "540053647", CategoryId = 102, IssuingCountry = "Netherlands" },
                    new Product { Id = 1, Name = "Product 1", Description = "Description 1", EAN = "1234567890123", IssuingCountry = "Country 1" }


                 ) ;

            modelBuilder.Entity<ProductPrice>().HasData
                (
                    new ProductPrice { Id = 100, Price = 799.99m, Currency = "EUR", Date = new DateTime(2020, 1, 1), ProductId = 100, RetailerId = 100 },
                    new ProductPrice { Id = 101, Price = 699.99m, Currency = "EUR", Date = new DateTime(2020, 1, 1), ProductId = 101, RetailerId = 100 },
                    new ProductPrice { Id = 102, Price = 6.99m, Currency = "EUR", Date = new DateTime(2020, 1, 1), ProductId = 102, RetailerId = 100 },
                    new ProductPrice { Id = 103, Price = 7.99m, Currency = "EUR", Date = new DateTime(2020, 1, 1), ProductId = 103, RetailerId = 100 },
                    new ProductPrice { Id = 104, Price = 8.99m, Currency = "EUR", Date = new DateTime(2020, 1, 1), ProductId = 104, RetailerId = 100 },
                    new ProductPrice { Id = 105, Price = 49.99m, Currency = "EUR", Date = new DateTime(2020, 1, 1), ProductId = 105, RetailerId = 100 },
                    new ProductPrice { Id = 106, Price = 39.99m, Currency = "EUR", Date = new DateTime(2020, 1, 1), ProductId = 106, RetailerId = 100 },
                    new ProductPrice { Id = 1, Price = 100.00m, Currency = "EUR", Date = new DateTime(2022, 1, 1), ProductId = 1, RetailerId = 1 },
                    new ProductPrice { Id = 2, Price = 200.00m, Currency = "EUR", Date = new DateTime(2022, 1, 2), ProductId = 1, RetailerId = 2 },
                    new ProductPrice { Id = 3, Price = 300.00m, Currency = "EUR", Date = new DateTime(2022, 1, 3), ProductId = 1, RetailerId = 3 }
                );
        }
    }
}