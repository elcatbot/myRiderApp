var builder = WebApplication.CreateBuilder(args);

builder.AddHostServices();
builder.AddGlobalServices();
builder.AddWebCors();
// builder.AddServiceDefaults();

builder.AddRabbitMqEventBus(builder.Configuration["EventBusConnection"]!);
builder.Services.AddSingleton<IPublishEvents, PublishEvents>();


builder.Services.AddOpenApi();

builder.Services.AddDbContext<IdentityContext>(options =>
    options.UseInMemoryDatabase("Identity"));

builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<IdentityContext>()
    .AddDefaultTokenProviders();

builder.Services.AddTransient<ITokenService, TokenService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors("AllowWebAngular");

app.UseApplicationPipeline();

app.MapIdentityApiV1();

app.Run();

