namespace myRideApp.Drivers.Infrastructure.Configurations;

public class DriverEntityConfiguration : IEntityTypeConfiguration<Driver>
{
    public void Configure(EntityTypeBuilder<Driver> builder)
    {
        builder.HasKey(d => d.Id);

        builder.OwnsOne(d => d.PhoneNumber, phoneNumber =>
        {
            phoneNumber.Property(p => p.Value)
                // .HasColumnName("PhoneNumber")
                .IsRequired();
        });

        builder.OwnsOne(d => d.Email, email =>
        {
            email.Property(e => e.Value)
                 //  .HasColumnName("Email")
                 .IsRequired();
        });

        builder.OwnsOne(d => d.Vehicle, vehicle =>
        {
            vehicle.Property(v => v.Make)
                // .HasColumnName("VehicleMake")
                .IsRequired();
            vehicle.Property(v => v.Model)
                // .HasColumnName("VehicleModel")
                .IsRequired();
            vehicle.Property(v => v.PlateNumber)
                // .HasColumnName("PlateNumber")
                .IsRequired();
        });

        builder.OwnsOne(d => d.CurrentLocation, location =>
        {
            location.Property(l => l.Latitude)
                // .HasColumnName("Latitude")
                ;
            location.Property(l => l.Longitude)
                // .HasColumnName("Longitude")
                ;
        });

        builder.OwnsOne(d => d.Rating, rating =>
        {
            rating.Property(r => r.TotalRatings)
                // .HasColumnName("TotalRatings")
                ;
            rating.Property(r => r.SumOfScores)
                // .HasColumnName("SumOfScores")
                ;
        });

        builder.Property(d => d.Status)
               .HasConversion<string>()
               .IsRequired();

        builder.Property(d => d.RegisteredAt)
               .IsRequired();

        builder.Navigation(d => d.Availability)
           .HasField("_availability")
           .UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.OwnsMany(d => d.Availability, availability =>
        {
            // availability.WithOwner().HasForeignKey("DriverId");
            // availability.Property<int>("Id"); // Shadow key
            // availability.HasKey("Id");

            // availability.Property(a => a.Day).HasColumnName("Day").IsRequired();
            // availability.Property(a => a.Start).HasColumnName("Start").IsRequired();
            // availability.Property(a => a.End).HasColumnName("End").IsRequired();

            // availability.ToTable("DriverAvailability");
        });

        builder.OwnsOne(d => d.History, history =>
        {
            // history.Ignore(h => h.TotalRides);
            // history.Ignore(h => h.TotalDistance);
            // history.Ignore(h => h.LastRide);

            // history.OwnsMany(h => h.Rides, ride =>
            // {
            //     ride.WithOwner().HasForeignKey("DriverId");
            //     ride.Property<Guid>("Id"); // Shadow key
            //     ride.HasKey("Id");

            //     ride.Property(r => r.RideId).HasColumnName("RideId").IsRequired();
            //     ride.Property(r => r.CompletedAt).HasColumnName("CompletedAt").IsRequired();
            //     ride.Property(r => r.DistanceKm).HasColumnName("DistanceKm").IsRequired();

            //     // ride.ToTable("DriverRideHistory");
            // });
        });

        // builder.ToTable("Drivers");
    }
}