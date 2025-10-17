using System.Net.Http.Json;
using myRideApp.Rides.Application.Commands;
using NBomber.Contracts.Stats;
using NBomber.CSharp;
using NBomber.Http;
using NBomber.Plugins.Network.Ping;

/// dotnet run -c Release
var httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:5053") };

var rideRequestScenario = Scenario.Create("ride_request", async context =>
{
    var riderId = Guid.NewGuid();
    var request = new RequestRideCommand(riderId);
    var response = await httpClient.PostAsJsonAsync("/api/rides", request);

    return response.IsSuccessStatusCode
        ? Response.Ok()
        : Response.Fail();
})
.WithWarmUpDuration(TimeSpan.FromSeconds(10))
.WithLoadSimulations(Simulation.Inject(rate: 10, interval: TimeSpan.FromSeconds(1), during: TimeSpan.FromMinutes(1)));

var getRideScenario = Scenario.Create("get_ride_request", async context =>
{
    var rideId = Guid.NewGuid();
    var response = await httpClient.GetAsync($"/api/rides/{rideId}");

    return response.IsSuccessStatusCode || response.StatusCode == System.Net.HttpStatusCode.NotFound
        ? Response.Ok()
        : Response.Fail();
})
.WithWarmUpDuration(TimeSpan.FromSeconds(10))
.WithLoadSimulations(Simulation.Inject(rate: 10, interval: TimeSpan.FromSeconds(1), during: TimeSpan.FromMinutes(1)));

var result = NBomberRunner
    .RegisterScenarios(rideRequestScenario, getRideScenario)
    .Run();
