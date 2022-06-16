using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Data;

public class DataContext : IdentityDbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<Date> Dates { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<SaleFact> SalesFact { get; set; }
    public DbSet<Store> Stores { get; set; }
    public DbSet<Category> Categories { get; set; }
}