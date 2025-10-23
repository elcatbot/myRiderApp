var builder = DistributedApplication.CreateBuilder(args);

// var rabbitmq = builder.AddRabbitMQ("eventbus");

// var elasticSearch = builder.AddElasticsearch("elasticsearch");

// var kibana = builder.AddContainer("kibana", "docker.elastic.co/kibana/kibana:8.13.0")
//     .WithEnvironment("ELASTICSEARCH_HOSTS", elastic.GetEndpoint("http"))
//     .WithEndpoint(containerPort: 5601, isProxied: true);

var rideService = builder.AddProject<Projects.Ride_Api>("ride-api")
    .WithEnvironment("RUNNING_IN_ASPIRE", "true")
    // .WaitFor(rabbitmq)
    // .WithReference(rabbitmq);
    ;

var apiGateway = builder.AddProject<Projects.Gateway_Api>("gateway-api");


builder.Build().Run();
