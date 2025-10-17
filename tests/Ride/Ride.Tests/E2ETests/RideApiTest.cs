namespace myRideApp.E2ETests.Rides;

public class RiderApiTest
    : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory<Program> _factory;

    public RiderApiTest(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = _factory.CreateClient();
    }

    [Fact]
    public async Task POST_RequestRide_Should_Return_Created()
    {
        // Arrange
        var riderId = Guid.NewGuid();
        var request = new RequestRideCommand(riderId);

        // Act
        var response = await _client.PostAsJsonAsync("/api/rides", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }

    [Fact]
    public async Task GET_GetRide_Should_Return_Ok()
    {
        // Arrange
        var riderId = Guid.NewGuid();
        var request = new RequestRideCommand(riderId);
        var res = await _client.PostAsJsonAsync("/api/rides", request);
        var rideId = new Guid(res.Headers.GetValues("X-ride-id").FirstOrDefault()!);

        // // Act
        var response = await _client.GetAsync($"/api/rides/{rideId}");

        // // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task GET_GetRide_ShouldReturnNotFound_WhenRideIdDoesNotExist()
    {
        // Arrange
        var rideId = Guid.NewGuid();

        // // Act
        var response = await _client.GetAsync($"/api/rides/{rideId}");

        // // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task PUT_AssignDriver_ShouldReturnCreated()
    {
        // Arrange
        var riderId = Guid.NewGuid();
        var driverId = Guid.NewGuid();
        var request = new RequestRideCommand(riderId);
        var res = await _client.PostAsJsonAsync("/api/rides", request);
        var rideId = new Guid(res.Headers.GetValues("X-ride-id").FirstOrDefault()!);
        var assignDriverRequest = new AssignDriverCommand(rideId, driverId);

        // Act
        var response = await _client.PutAsJsonAsync("/api/rides/assign", assignDriverRequest);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task PUT_AssignDriver_ShouldReturnNoContent_WhenRideIdDoesNotExist()
    {
        // Arrange
        var rideId = Guid.NewGuid();
        var driverId = Guid.NewGuid();
        var assignDriverRequest = new AssignDriverCommand(rideId, driverId);

        // Act
        var response = await _client.PutAsJsonAsync("/api/rides/assign", assignDriverRequest);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task PUT_InitRide_ShouldReturnCreated()
    {
        // Arrange
        var riderId = Guid.NewGuid();
        var driverId = Guid.NewGuid();
        var request = new RequestRideCommand(riderId);
        var res1 = await _client.PostAsJsonAsync("/api/rides", request);
        var rideId = new Guid(res1.Headers.GetValues("X-ride-id").FirstOrDefault()!);
        var assignDriverRequest = new AssignDriverCommand(rideId, driverId);
        await _client.PutAsJsonAsync("/api/rides/assign", assignDriverRequest);
        var initRideRequest = new InitRideCommand(rideId);

        // Act
        var response = await _client.PutAsJsonAsync("/api/rides/init", initRideRequest);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task PUT_InitRide_ShouldReturnNoContent_WhenRideIdDoesNotExist()
    {
        // Arrange
        var rideId = Guid.NewGuid();
        var assignDriverRequest = new InitRideCommand(rideId);

        // Act
        var response = await _client.PutAsJsonAsync("/api/rides/init", assignDriverRequest);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task PUT_CompleteRide_ShouldReturnCreated()
    {
        // Arrange
        var riderId = Guid.NewGuid();
        var driverId = Guid.NewGuid();
        var request = new RequestRideCommand(riderId);
        var res1 = await _client.PostAsJsonAsync("/api/rides", request);
        var rideId = new Guid(res1.Headers.GetValues("X-ride-id").FirstOrDefault()!);
        var assignDriverRequest = new AssignDriverCommand(rideId, driverId);
        await _client.PutAsJsonAsync("/api/rides/assign", assignDriverRequest);
        var initRideRequest = new InitRideCommand(rideId);
        await _client.PutAsJsonAsync("/api/rides/init", initRideRequest);
        var completeRideRequest = new CompleteRideCommand(rideId);

        // Act
        var response = await _client.PutAsJsonAsync("/api/rides/complete", completeRideRequest);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task PUT_CompleteRide_ShouldReturnNoContent_WhenRideIdDoesNotExist()
    {
        // Arrange
        var rideId = Guid.NewGuid();
        var assignDriverRequest = new CompleteRideCommand(rideId);

        // Act
        var response = await _client.PutAsJsonAsync("/api/rides/complete", assignDriverRequest);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task PUT_CancelRide_ShouldReturnCreated()
    {
        // Arrange
        var riderId = Guid.NewGuid();
        var driverId = Guid.NewGuid();
        var request = new RequestRideCommand(riderId);
        var res1 = await _client.PostAsJsonAsync("/api/rides", request);
        var rideId = new Guid(res1.Headers.GetValues("X-ride-id").FirstOrDefault()!);
        var assignDriverRequest = new AssignDriverCommand(rideId, driverId);
        await _client.PutAsJsonAsync("/api/rides/assign", assignDriverRequest);
        var initRideRequest = new InitRideCommand(rideId);
        await _client.PutAsJsonAsync("/api/rides/init", initRideRequest);
        var completeRideRequest = new CancelRideCommand(rideId);

        // Act
        var response = await _client.PutAsJsonAsync("/api/rides/cancel", completeRideRequest);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task PUT_CancelRide_ShouldReturnNoContent_WhenRideIdDoesNotExist()
    {
        // Arrange
        var rideId = Guid.NewGuid();
        var assignDriverRequest = new CancelRideCommand(rideId);

        // Act
        var response = await _client.PutAsJsonAsync("/api/rides/cancel", assignDriverRequest);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

}