using myRideApp.Utilities.EventBus;

namespace myRideApp.Rides.Application.Commands;

public class CompleteRideCommandHandlerTest
{
    [Fact]
    public async Task Handle_CompleteRide_UpdatesRepository_SavesAndPublishesEvent()
    {
        // Arrange
        var riderId = Guid.NewGuid();
        var driverId = Guid.NewGuid();
        var ride = new Ride(riderId, new Location(1, 1), new Location(2, 2), 4000);
        ride.AssignDriver(driverId);
        ride.InitRide();

        var repositoryMock = new Mock<IRideRepository>();
        repositoryMock.Setup(r => r.GetByIdAsync(ride.Id)).ReturnsAsync(ride);

        var eventBusMock = new Mock<IEventBus>();
        eventBusMock
            .Setup(e => e.PublishAsync(It.IsAny<RideCompletedIntegrationEvent>(), "Ride"))
            .ReturnsAsync(true);

        var command = new CompleteRideCommand(ride.Id);
        var handler = new CompleteRideCommandHandler(repositoryMock.Object, eventBusMock.Object);

        // Act
        var sut = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(sut);
        Assert.Equal(RideStatus.Completed, ride.Status);

        // Repository update and save should be invoked
        repositoryMock.Verify(r => r.UpdateAsync(ride), Times.Once);
        repositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);

        // An integration event with correct payload should be published
        eventBusMock.Verify(eb => eb.PublishAsync(
            It.Is<RideCompletedIntegrationEvent>(ev =>
                ev.RideId == ride.Id &&
                ev.DriverId == driverId &&
                ev.RiderId == riderId &&
                ev.RequestedAt == ride.RequestedAt
            ), "Ride"), Times.Once);
    }

    [Fact]
    public async Task Handle_InitRide_ReturnsNull_WhenRideDoesNotExists()
    {
        // Arrange
        // Arrange
        var riderId = Guid.NewGuid();
        var ride = new Ride(riderId, new Location(1, 1), new Location(2, 2), 4000);

        var repositoryMock = new Mock<IRideRepository>();
        repositoryMock.Setup(r => r.GetByIdAsync(ride.Id)).ReturnsAsync((Ride)null!);

        var eventBusMock = new Mock<IEventBus>();

        var command = new InitRideCommand(ride.Id);
        var handler = new InitRideCommandHandler(repositoryMock.Object, eventBusMock.Object);

        // Act 
        var sut = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(sut);
        repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Ride>()), Times.Never);
        repositoryMock.Verify(r => r.SaveChangesAsync(), Times.Never);
        eventBusMock.Verify(e => e.PublishAsync(It.IsAny<RideRequestedIntegrationEvent>(), "Ride"), Times.Never);
    }
}