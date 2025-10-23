var builder = WebApplication.CreateBuilder(args);

builder.AddApplicationServices();
builder.Host.AddHostServices();

builder.AddServiceDefaults();

var app = builder.Build();

app.UseCustomException();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapRiderApiV1();
app.Run();

