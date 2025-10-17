var builder = DistributedApplication.CreateBuilder(args);

// var rabbitmq = builder.AddRabbitMQ("eventbus");

var rideService = builder.AddProject<Projects.Ride_Api>("ride-api")
    .WithEnvironment("RUNNING_IN_ASPIRE", "true")
    // .WaitFor(rabbitmq)
    // .WithReference(rabbitmq);
    ;


builder.Build().Run();
