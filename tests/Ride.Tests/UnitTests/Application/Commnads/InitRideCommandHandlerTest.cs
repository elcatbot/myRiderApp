namespace myRideApp.Rides.Application.Commands;

public class InitRideCommandHandlerTest
{
    [Fact]
    public async Task Handle_InitRide_UpdatesRepository_SavesAndPublishesEvent()
    {
        // Arrange
        var riderId = Guid.NewGuid();
        var driverId = Guid.NewGuid();
        var ride = new Ride(riderId);
        ride.AssignDriver(driverId);

        var repositoryMock = new Mock<IRideRepository>();
        repositoryMock.Setup(r => r.GetByIdAsync(ride.Id)).ReturnsAsync(ride);

        var eventBusMock = new Mock<IEventBus>();
        eventBusMock
            .Setup(e => e.PublishAsync(It.IsAny<RideInitializedIntegrationEvent>()))
            .ReturnsAsync(true);

        var command = new InitRideCommand(ride.Id);
        var handler = new InitRideCommandHandler(repositoryMock.Object, eventBusMock.Object);

        // Act
        var sut = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(sut);
        Assert.Equal(RideStatus.InProgress, ride.Status);

        // Repository update and save should be invoked
        repositoryMock.Verify(r => r.UpdateAsync(ride), Times.Once);
        repositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);

        // An integration event with correct payload should be published
        eventBusMock.Verify(eb => eb.PublishAsync(
            It.Is<RideInitializedIntegrationEvent>(ev =>
                ev.RideId == ride.Id &&
                ev.DriverId == driverId &&
                ev.RiderId == riderId &&
                ev.RequestedAt == ride.RequestedAt
            )), Times.Once);
    }

    [Fact]
    public async Task Handle_InitRide_ReturnsNull_WhenRideDoesNotExists()
    {
        // Arrange
        // Arrange
        var riderId = Guid.NewGuid();
        var ride = new Ride(riderId);

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
        eventBusMock.Verify(e => e.PublishAsync(It.IsAny<RideRequestedIntegrationEvent>()), Times.Never);
    }
}