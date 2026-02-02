using myRideApp.Utilities.EventBus;

namespace myRideApp.Tests.Rides.Application.Commands;

public class RequestRideCommandHandlerTest
{
    [Fact]
    public async Task Handle_AddRide_SavesAndPublishesEvent_ReturnsNonNullRideDto()
    {
        // Arrange
        var repoMock = new Mock<IRideRepository>();
        var eventBusMock = new Mock<IEventBus>();

        repoMock
            .Setup(r => r.Add(It.IsAny<Ride>()));

        repoMock
            .Setup(r => r.SaveChangesAsync())
            .Returns(Task.CompletedTask);

        eventBusMock
            .Setup(e => e.PublishAsync(It.IsAny<RideRequestedIntegrationEvent>(), "Ride"))
            .ReturnsAsync(true);

        var handler = new RequestRideCommandHandler(repoMock.Object, eventBusMock.Object);
        var command = new RequestRideCommand(Guid.NewGuid(), new Location(1, 1), new Location(2, 2), 4000);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        repoMock.Verify(r => r.Add(It.IsAny<Ride>()), Times.Once);
        repoMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        eventBusMock.Verify(e => e.PublishAsync(It.IsAny<RideRequestedIntegrationEvent>(), "Ride"), Times.Once);
    }

    [Fact]
    public async Task Handle_WhenRepositoryThrows_DoesNotPublishAndThrows()
    {
        // Arrange
        var repoMock = new Mock<IRideRepository>();
        var eventBusMock = new Mock<IEventBus>();

        repoMock
            .Setup(r => r.Add(It.IsAny<Ride>()))
            .Throws(new InvalidOperationException("repo failure"));

        var handler = new RequestRideCommandHandler(repoMock.Object, eventBusMock.Object);
        var command = new RequestRideCommand(Guid.NewGuid(), new Location(1, 1), new Location(2, 2), 4000);

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => handler.Handle(command, CancellationToken.None));
        repoMock.Verify(r => r.Add(It.IsAny<Ride>()), Times.Once);
        repoMock.Verify(r => r.SaveChangesAsync(), Times.Never);
        eventBusMock.Verify(e => e.PublishAsync(It.IsAny<RideRequestedIntegrationEvent>(), "Ride"), Times.Never);
    }
}