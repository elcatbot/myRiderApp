var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.AddApplicationServices();
builder.Host.AddHostServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// 3️⃣ Use rate limiter globally
app.UseRateLimiter();

app.UseAuthentication();
app.UseAuthorization();
app.MapReverseProxy();

app.Run();

