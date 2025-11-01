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

        modelBuilder.Entity<Ride>().OwnsOne(o => o.PickUp, pickup =>
        {
            pickup.Property(l => l.Latitude)
                // .HasColumnName("Latitude")
                ;
            pickup.Property(l => l.Longitude)
                // .HasColumnName("Longitude")
                ;
        });

         modelBuilder.Entity<Ride>().OwnsOne(o => o.DropOff, dropoff =>
        {
            dropoff.Property(l => l.Latitude)
                // .HasColumnName("Latitude")
                ;
            dropoff.Property(l => l.Longitude)
                // .HasColumnName("Longitude")
                ;
        });
    }
}

