namespace myRideApp.Drivers.Infrastructure;

public class DriverContext : DbContext
{
    public DriverContext(DbContextOptions<DriverContext> options) : base(options) { }

    public DbSet<Driver> Drivers { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // All value objects persisted as owned entity type supported since EF Core 2.0
        modelBuilder.ApplyConfiguration(new DriverEntityConfiguration());
    }
}

