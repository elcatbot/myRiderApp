
var builder = WebApplication.CreateBuilder(args);

builder.AddRabbitMqEventBus("eventbus");

builder.Services.AddOpenApi();

builder.Services.AddDbContext<RideContext>(o => o.UseInMemoryDatabase("Rides"));

builder.Services.AddTransient<IRideRepository, RideRepository>();

builder.Services.AddMediatR(conf => conf.RegisterServicesFromAssemblyContaining(typeof(RequestRideCommandHandler)));

var app = builder.Build();

app.UseCustomException();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapRiderApiV1();
app.Run();

