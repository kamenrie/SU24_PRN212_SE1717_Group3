
using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Models;
namespace DataAccessLayer
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext() {}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=LAPTOP-WDBHGR93;Initial Catalog=SU24_PRN212_SE1717_Group3;Integrated Security=True;Trust Server Certificate=True");
        }

        public DbSet<Account> Account { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Discount> Discount { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Profile> Profile { get; set; }
        public DbSet<ShippingInformation> ShippingInformation { get; set; }
        public DbSet<Shop> Shop { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<GeneralFeedback> GeneralFeedback { get; set; } 
        public DbSet<Delivery> Delivery { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           modelBuilder.Entity<OrderDetail>().HasKey(o => new {o.OrderId, o.ProductId, o.SizeId });
            modelBuilder.Entity<MyDiscount>().HasKey(o=> new {o.DiscountId, o.AccountId});
           
        }

        public DbSet<OrderDetail> Orderdetail { get; set; }

        public DbSet<Stock> Stock { get; set; }  

        public DbSet<MyDiscount> Mydiscount { get; set; }

        public DbSet<Size> Size { get; set; }

        

       

    }
}
