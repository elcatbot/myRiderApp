var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.AddApplicationServices();
builder.Host.AddHostServices();
builder.AddServiceDefaults();

builder.Configuration.AddJsonFile("appsettings.Yarp.json", optional: false, reloadOnChange: true);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.Use(async (context, next) =>
{
    Console.WriteLine(context.Connection.RemoteIpAddress);

    await next();
});

// 3️⃣ Use rate limiter globally
app.UseRateLimiter();

app.UseAuthentication();
app.UseAuthorization();
app.MapReverseProxy(pipeline =>
{
    pipeline.UseIPFilterPolicies();
    pipeline.UseLoadBalancing();

});

app.Run();

