var builder = WebApplication.CreateBuilder(args);

builder.AddApplicationServices();

builder.AddServiceDefaults();

var app = builder.Build();

app.UseCustomException();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors("AllowWebAngular");

app.UseAuthentication();

app.UseAuthorization();

app.MapRiderApiV1();

app.MapHub<MainHub>("/hub/ride");

app.Run();

