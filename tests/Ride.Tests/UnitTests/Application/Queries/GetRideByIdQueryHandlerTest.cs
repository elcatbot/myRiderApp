namespace myRideApp.Tests.Rides.Application.Queries;

public class GetRideByIdQueryHandlerTest
{
    private readonly Mock<IRideRepository> _repository;

    public GetRideByIdQueryHandlerTest()
    {
        _repository = new();
    }

    [Fact]
    public async Task Handle_ReturnsRide_WhenRideExists()
    {
        // Arrange
        var ride = FakeRide();
        _repository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(ride);

        var query = new GetRideByIdQuery(ride.Id);
        var handler = new GetRideByIdQueryHandler(_repository.Object);

        // Act
        var sut = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.Equal(sut.Id, ride.Id);
        Assert.Equal(sut.Status, RideStatus.Requested);
        _repository.Verify(r => r.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ReturnsNull_WhenRideDoesNotExist()
    {
        // Arrange
        var rideId = Guid.NewGuid();
        _repository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Ride)null!);

        var query = new GetRideByIdQuery(rideId);
        var handler = new GetRideByIdQueryHandler(_repository.Object);

        // // Act & Assert
        await Assert.ThrowsAsync<NullReferenceException>(async () => await handler.Handle(query, CancellationToken.None));
        _repository.Verify(r => r.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
    }

    private Ride FakeRide()
        => new Ride(new Guid("971e6c78-f337-4396-87b7-e93f7a5389d8"));
}