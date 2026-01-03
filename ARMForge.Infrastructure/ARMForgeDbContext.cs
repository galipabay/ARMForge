using ARMForge.Kernel.Entities;
using Microsoft.EntityFrameworkCore;

namespace ARMForge.Infrastructure
{
    public class ARMForgeDbContext(DbContextOptions<ARMForgeDbContext> options) : DbContext(options)
    {
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
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<PurchaseOrderItem> PurchaseOrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // 🔹 UserRole join table için composite key
            modelBuilder.Entity<UserRole>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });

            // 🔹 User ↔ UserRole (1:N)
            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);

            // 🔹 Role ↔ UserRole (1:N)
            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId);

            // 🔹 Email unique constraint
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // Driver - Shipment ilişkisi
            // Bir sürücünün birden fazla sevkiyatı olabilir, bir sevkiyatın bir sürücüsü olur.
            modelBuilder.Entity<Shipment>()
                .HasOne(s => s.Driver)
                .WithMany(d => d.Shipments)
                .HasForeignKey(s => s.DriverId)
                .OnDelete(DeleteBehavior.Restrict);

            // Vehicle - Shipment ilişkisi
            // Bir aracın birden fazla sevkiyatı olabilir, bir sevkiyatın bir aracı olur.
            modelBuilder.Entity<Shipment>()
                .HasOne(s => s.Vehicle)
                .WithMany(v => v.Shipments)
                .HasForeignKey(s => s.VehicleId)
                .OnDelete(DeleteBehavior.Restrict);

            // Order - Shipment ilişkisi
            // Bir sipariş birden fazla sevkiyata bölünebilir (partial shipment)
            modelBuilder.Entity<Shipment>()
                .HasOne(s => s.Order)
                .WithMany(o => o.Shipments)
                .HasForeignKey(s => s.OrderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Product>()
                .HasIndex(p => p.StockKeepingUnit)
                .IsUnique();

            modelBuilder.Entity<OrderItem>()
                .HasIndex(oi => new { oi.OrderId, oi.ProductId })
                .IsUnique();

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        // TODO: PROD ÖNCESİ KALDIRILACAK – Connection string DI'a taşınacak
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Database=ARMForgeDb;Username=postgres;Password=12345");
        }

    }
}
