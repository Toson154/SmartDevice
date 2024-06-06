namespace Devices_E_Commerce.Data;
using Devices_E_Commerce.Models;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Device> Devices { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Vendor> Vendors { get; set; }
    public DbSet<CustomerAccount> CustomerAccounts { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Payment> Payments { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }


}
