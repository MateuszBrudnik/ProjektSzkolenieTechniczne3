using Microsoft.EntityFrameworkCore;
using CarServiceAPI.Models;
using System.Collections.Generic;

public class CarServiceDbContext : DbContext
{
    public CarServiceDbContext(DbContextOptions<CarServiceDbContext> options) : base(options) { }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<Car> Cars { get; set; }
    public DbSet<Repair> Repairs { get; set; }
}
