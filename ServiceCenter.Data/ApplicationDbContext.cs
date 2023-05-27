using Microsoft.EntityFrameworkCore;
using ServiceCenter.Domain.Entity;
using ServiceCenter.Domain.Enum;

namespace ServiceCenter.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        public DbSet<Abonent> Abonents { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Employee> Employeers { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<OrderMaterial> OrderMaterials { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<OrderService> Services { get; set; }
        public DbSet<Tariff> Tariffs { get; set; }
        public DbSet<TariffType> TariffTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(builder =>
            {
                builder.ToTable("Users").HasKey(x => x.User_ID);
                builder.Property(x => x.User_ID).ValueGeneratedOnAdd();

                builder.HasData(new User
                {
                    User_ID = 1,
                    Username = "admin",
                    Password = "admin",
                    Role = Role.Admin
                });
                builder.HasData(new User
                {
                    User_ID = 2,
                    Username = "oper",
                    Password = "oper",
                    Role = Role.Operator
                });
                builder.HasData(new User
                {
                    User_ID = 3,
                    Username = "emp",
                    Password = "emp",
                    Role = Role.Employee
                });
            });

            modelBuilder.Entity<Abonent>(builder =>
            {
                builder.ToTable("Abonents").HasKey(x => x.Abonent_ID);
                builder.Property(x => x.Abonent_ID).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Employee>(builder =>
            {
                builder.ToTable("Employeers").HasKey(x => x.Employee_ID);
                builder.Property(x => x.Employee_ID).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Material>(builder =>
            {
                builder.ToTable("Materials").HasKey(x => x.Material_ID);
                builder.Property(x => x.Material_ID).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<OrderMaterial>(builder =>
            {
                builder.ToTable("SingleOrderMaterial").HasKey(x => x.OrderMaterial_ID);
                builder.Property(x => x.OrderMaterial_ID).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Order>(builder =>
            {
                builder.ToTable("Orders").HasKey(x => x.Order_ID);
                builder.Property(x => x.Order_ID).ValueGeneratedOnAdd();
                builder.HasMany(x => x.Services)
                       .WithMany(x => x.Orders)
                       .UsingEntity(x => x.ToTable("OrderServices"));
            });

            modelBuilder.Entity<Payment>(builder =>
            {
                builder.ToTable("Payments").HasKey(x => x.Payment_ID);
                builder.Property(x => x.Payment_ID).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<OrderService>(builder =>
            {
                builder.ToTable("Services").HasKey(x => x.OrderService_ID);
                builder.Property(x => x.OrderService_ID).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Tariff>(builder =>
            {
                builder.ToTable("Tariffs").HasKey(x => x.Tariff_ID);
                builder.Property(x => x.Tariff_ID).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<TariffType>(builder =>
            {
                builder.ToTable("TariffTypes").HasKey(x => x.TariffType_ID);
                builder.Property(x => x.TariffType_ID).ValueGeneratedOnAdd();
            });
        }
    }
}
