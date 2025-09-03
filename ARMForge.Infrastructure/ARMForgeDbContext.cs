using ARMForge.Kernel.Entities;
using Microsoft.EntityFrameworkCore;

namespace ARMForge.Infrastructure
{
    public class ARMForgeDbContext : DbContext
    {
        public ARMForgeDbContext(DbContextOptions<ARMForgeDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Shipment> Shipments { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Database=ARMForgeDb;Username=postgres;Password=12345");
        }
    }
}
