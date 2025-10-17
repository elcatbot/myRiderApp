namespace myRideApp.Tests.Rides.Application.Commands;

public class RequestRideCommandHandlerTest
{
    [Fact]
    public async Task Handle_AddRide_SavesAndPublishesEvent_ReturnsNonEmptyGuid()
    {
        // Arrange
        var repoMock = new Mock<IRideRepository>();
        var eventBusMock = new Mock<IEventBus>();

        repoMock
            .Setup(r => r.AddAsync(It.IsAny<Ride>()))
            .Returns(Task.CompletedTask);

        repoMock
            .Setup(r => r.SaveChangesAsync())
            .Returns(Task.CompletedTask);

        eventBusMock
            .Setup(e => e.PublishAsync(It.IsAny<RideRequestedIntegrationEvent>()))
            .ReturnsAsync(true);

        var handler = new RequestRideCommandHandler(repoMock.Object, eventBusMock.Object);
        var command = new RequestRideCommand(Guid.NewGuid());

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotEqual(Guid.Empty, result);
        repoMock.Verify(r => r.AddAsync(It.IsAny<Ride>()), Times.Once);
        repoMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        eventBusMock.Verify(e => e.PublishAsync(It.IsAny<RideRequestedIntegrationEvent>()), Times.Once);
    }

    [Fact]
    public async Task Handle_WhenRepositoryThrows_DoesNotPublishAndThrows()
    {
        // Arrange
        var repoMock = new Mock<IRideRepository>();
        var eventBusMock = new Mock<IEventBus>();

        repoMock
            .Setup(r => r.AddAsync(It.IsAny<Ride>()))
            .ThrowsAsync(new InvalidOperationException("repo failure"));

        var handler = new RequestRideCommandHandler(repoMock.Object, eventBusMock.Object);
        var command = new RequestRideCommand(Guid.NewGuid());

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => handler.Handle(command, CancellationToken.None));
        repoMock.Verify(r => r.AddAsync(It.IsAny<Ride>()), Times.Once);
        repoMock.Verify(r => r.SaveChangesAsync(), Times.Never);
        eventBusMock.Verify(e => e.PublishAsync(It.IsAny<RideRequestedIntegrationEvent>()), Times.Never);
    }
}