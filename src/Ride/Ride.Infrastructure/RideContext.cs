namespace myRideApp.Rides.Infrastructure;

public class RideContext : DbContext
{
    public RideContext(DbContextOptions<RideContext> options) : base(options) { }

    public DbSet<Ride> Rides { get; set; }
    public DbSet<OutboxMessage> OutboxMessages { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Fare value object persisted as owned entity type supported since EF Core 2.0
        modelBuilder.Entity<Ride>().OwnsOne(o => o.Fare);
    }
}

