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
        public DbSet<UserRole> UserRoles { get; set; }
        //public DbSet<Warehouse> Warehouses { get; set; }
        //public DbSet<Inventory> Inventories { get; set; }
        //public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        //public DbSet<PurchaseOrderItem> PurchaseOrderItems { get; set; }
        //public DbSet<Payment> Payments { get; set; }
        //public DbSet<Category> Categories { get; set; }
        //public DbSet<Brand> Brands { get; set; }
        //public DbSet<StockMovement> StockMovements { get; set; }
        //public DbSet<MaintenanceRecord> MaintenanceRecords { get; set; }
        //public DbSet<FuelLog> FuelLogs { get; set; }
        //public DbSet<Route> Routes { get; set; }
        //public DbSet<Delivery> Deliveries { get; set; }
        //public DbSet<Return> Returns { get; set; }
        //public DbSet<AuditLog> AuditLogs { get; set; }
        //public DbSet<Notification> Notifications { get; set; }
        //public DbSet<Setting> Settings { get; set; }
        //public DbSet<Report> Reports { get; set; }
        //public DbSet<DashboardWidget> DashboardWidgets { get; set; }


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
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Database=ARMForgeDb;Username=postgres;Password=12345");
        }

    }
}
