var builder = DistributedApplication.CreateBuilder(args);

// var rabbitmq = builder.AddRabbitMQ("eventbus");

var rideService = builder.AddProject<Projects.Ride_Api>("ride-api")
    // .WaitFor(rabbitmq)
    // .WithReference(rabbitmq);
    ;


builder.Build().Run();
