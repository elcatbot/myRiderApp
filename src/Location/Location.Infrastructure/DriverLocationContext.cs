namespace myRideApp.Drivers.Infrastructure;

public class DriverLocationContext : DbContext
{
    public DriverLocationContext(DbContextOptions<DriverLocationContext> options) : base(options) { }
    public DbSet<DriverLocation> DriverLocations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DriverLocation>(entity =>
        {
            entity.HasKey(e => e.DriverId);
            entity.Property(e => e.Latitude).IsRequired();
            entity.Property(e => e.Longitude).IsRequired();
            entity.Property(e => e.UpdatedAt).IsRequired();
        });
    }
}

