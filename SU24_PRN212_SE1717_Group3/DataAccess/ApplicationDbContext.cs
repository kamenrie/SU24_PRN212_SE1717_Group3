
using Microsoft.EntityFrameworkCore;
using SU24_PRN212_SE1717_Group3.Models;
namespace SU24_PRN212_SE1717_Group3.DataAccess
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
        public DbSet<ShippingInformation> Shippinginformation { get; set; }
        public DbSet<Shop> Shop { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<GeneralFeedback> Generalfeedback { get; set; } 
        public DbSet<Delivery> Delivery { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           modelBuilder.Entity<OrderDetail>().HasKey(o => new {o.OrderId,o.ProductId });
            modelBuilder.Entity<MyDiscount>().HasKey(o=> new {o.Discountid,o.Accountid});
           
        }

        public DbSet<OrderDetail> Orderdetail { get; set; }

        public DbSet<Stock> Stocks { get; set; }  

        public DbSet<MyDiscount> Mydiscount { get; set; }

        

       

    }
}
