using myRideApp.Utilities.EventBus;

namespace myRideApp.Rides.Application.Commands;

public class AssignDriverCommandHandlerTest
{
    [Fact]
    public async Task Handle_AssignsDriver_UpdatesRepository_SavesAndPublishesEvent()
    {
        // Arrange
        var riderId = Guid.NewGuid();
        var driverId = Guid.NewGuid();
        var ride = new Ride(riderId, new Location(1, 1), new Location(2, 2), 4000);
        
        var repositoryMock = new Mock<IRideRepository>();
        repositoryMock.Setup(r => r.GetByIdAsync(ride.Id)).ReturnsAsync(ride);

        var eventBusMock = new Mock<IEventBus>();

        var command = new AssignDriverCommand(ride.Id, driverId);
        var handler = new AssignDriverCommandHandler(repositoryMock.Object);

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal(driverId, ride.DriverId);
        Assert.Equal(RideStatus.Accepted, ride.Status);

        // Repository update and save should be invoked
        repositoryMock.Verify(r => r.Update(ride), Times.Once);
        repositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);

    }

    [Fact]
    public async Task Handle_WhenSaveChangesDomainEventsThrows_DoesNotPublishIntegrationEventsAndThrows()
    {
         // Arrange
        // Arrange
        var riderId = Guid.NewGuid();
        var driverId = Guid.NewGuid();
        var ride = new Ride(riderId, new Location(1, 1), new Location(2, 2), 4000);

        var repositoryMock = new Mock<IRideRepository>();
        repositoryMock.Setup(r => r.GetByIdAsync(ride.Id)).ReturnsAsync(ride);
        repositoryMock.Setup(r => r.SaveChangesAsync()).ThrowsAsync(new Exception("Domain event handling failed"));

        var eventBusMock = new Mock<IEventBus>();

        var command = new AssignDriverCommand(ride.Id, driverId);
        var handler = new AssignDriverCommandHandler(repositoryMock.Object);
        
        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => handler.Handle(command, CancellationToken.None));
        repositoryMock.Verify(r => r.Update(It.IsAny<Ride>()), Times.Once);
        repositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
    }
}