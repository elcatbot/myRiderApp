namespace myRideApp.Tests.Rides.Domain;

public class RideTest
{
    [Fact]
    public void CreateRide_ReturnsRide_Success()
    {
        // Arrage
        var riderId = Guid.NewGuid();

        // Act
        var ride = new Ride(riderId);

        // Assert
        Assert.NotNull(ride);
        Assert.Equal(RideStatus.Requested, ride.Status);
        Assert.NotEmpty(ride.DomainEvents);
    }

    [Fact]
    public void AssignDriver_ReturnsRide_Success()
    {
        // Arrage
        var riderId = Guid.NewGuid();
        var driverId = Guid.NewGuid();
        var ride = new Ride(riderId);

        // Act
        ride.AssignDriver(driverId);

        // Assert
        Assert.NotNull(ride);
        Assert.Equal(RideStatus.Accepted, ride.Status);
    }

    [Fact]
    public void AssignDriver_ReturnsDomainException_WhenStatusIsNotRequested()
    {
        // Arrage
        var riderId = Guid.NewGuid();
        var driverId = Guid.NewGuid();
        var ride = new Ride(riderId);
        ride.AssignDriver(driverId);
        ride.InitRide();

        // Act & Assert
        Assert.Throws<RideDomainException>(() => ride.AssignDriver(driverId));
    }

    [Fact]
    public void InitRide_ReturnsRide_Success()
    {
        // Arrage
        var riderId = Guid.NewGuid();
        var driverId = Guid.NewGuid();
        var ride = new Ride(riderId);
        ride.AssignDriver(driverId);

        // Act
        ride.InitRide();

        // Assert
        Assert.NotNull(ride);
        Assert.Equal(RideStatus.InProgress, ride.Status);
    }

    [Fact]
    public void InitRide_ReturnsDomainException_WhenStatusIsNotRequested()
    {
        // Arrage
        var riderId = Guid.NewGuid();
        var ride = new Ride(riderId);

        // Act & Assert
        Assert.Throws<RideDomainException>(() => ride.InitRide());
    }

    [Fact]
    public void CompleteRide_ReturnsRide_Success()
    {
        // Arrage
        var riderId = Guid.NewGuid();
        var driverId = Guid.NewGuid();
        var ride = new Ride(riderId);
        ride.AssignDriver(driverId);
        ride.InitRide();

        // Act
        ride.CompleteRide();

        // Assert
        Assert.NotNull(ride);
        Assert.Equal(RideStatus.Completed, ride.Status);
    }

    [Fact]
    public void CompleteRide_ReturnsDomainException_WhenStatusIsNotInProgress()
    {
        // Arrage
        var riderId = Guid.NewGuid();
        var ride = new Ride(riderId);

        // Act & Assert
        Assert.Throws<RideDomainException>(() => ride.CompleteRide());
    }

    [Fact]
    public void CancelRide_ReturnsRide_Success()
    {
        // Arrage
        var riderId = Guid.NewGuid();
        var driverId = Guid.NewGuid();
        var ride = new Ride(riderId);
        ride.AssignDriver(driverId);
        ride.InitRide();

        // Act
        ride.CancelRide();

        // Assert
        Assert.NotNull(ride);
        Assert.Equal(RideStatus.Cancelled, ride.Status);
    }

    [Fact]
    public void CancelRide_ReturnsDomainException_WhenStatusIsNotInProgress()
    {
        // Arrage
        var riderId = Guid.NewGuid();
        var ride = new Ride(riderId);

        // Act & Assert
        Assert.Throws<RideDomainException>(() => ride.CancelRide());
    }
}