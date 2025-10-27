
var builder = WebApplication.CreateBuilder(args);

builder.Host.AddHostServices();
builder.AddGlobalServices();
builder.AddServiceDefaults();

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

app.UseApplicationPipeline();

app.MapIdentityApiV1();

app.Run();

