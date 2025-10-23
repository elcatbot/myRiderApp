namespace myRideApp.Gateway.Api.Extensions;

public static class ApplicationExtensions
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
        AddReverseProxy(builder);
        AddRequestRateLimit(builder);
        AddAuth(builder);
    }

    private static void AddAuth(IHostApplicationBuilder builder)
    {
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
        {
            options.TokenValidationParameters = new()
            {
                ValidIssuer = builder.Configuration["JwtToken:Issuer"],
                ValidAudience = builder.Configuration["JwtToken:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtToken:Key"]!))
            };
            options.RequireHttpsMetadata = false;
            options.IncludeErrorDetails = true;
            options.SaveToken = true;
        });

        builder.Services.AddAuthorization(options =>
        {
            // Example policy: only drivers
            options.AddPolicy("Authenticated", policy =>
            {
                policy.RequireAuthenticatedUser();
            });

            options.AddPolicy("RiderOnly", policy =>
            {
                policy.RequireRole("rider");
            });
        });
    }

    private static void AddRequestRateLimit(IHostApplicationBuilder builder)
    {

        // 1️⃣ Add rate limiter policies
        builder.Services.AddRateLimiter(options =>
        {
            // Rider: 10 requests per minute
            options.AddPolicy("RidesRateLimit", ctx =>
                RateLimitPartition.GetFixedWindowLimiter(
                    partitionKey: ctx.Connection.RemoteIpAddress?.ToString() ?? "global",
                    partition => new FixedWindowRateLimiterOptions
                    {
                        PermitLimit = 10,
                        Window = TimeSpan.FromMinutes(1),
                        QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                        QueueLimit = 0
                    }));

            // Driver: 20 requests per minute
            options.AddPolicy("DriverRateLimit", ctx =>
                RateLimitPartition.GetFixedWindowLimiter(
                    partitionKey: ctx.Connection.RemoteIpAddress?.ToString() ?? "global",
                    partition => new FixedWindowRateLimiterOptions
                    {
                        PermitLimit = 20,
                        Window = TimeSpan.FromMinutes(1),
                        QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                        QueueLimit = 0
                    }));

            options.OnRejected = async (context, token) =>
            {
                context.HttpContext.Response.StatusCode = 429;
                context.HttpContext.Response.Headers["X-RateLimit-RetryAfter"] = "10";
                await context.HttpContext.Response.WriteAsync("Rate limit exceeded. Try again later.");
            };
            options.RejectionStatusCode = 429; // Too Many Requests
        });

        // Global rate limit for all routes
        // options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(context =>
        // {
        //     var clientId = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";

        //     return RateLimitPartition.GetTokenBucketLimiter(
        //         partitionKey: clientId,
        //         factory: _ => new TokenBucketRateLimiterOptions
        //         {
        //             TokenLimit = 20,            // Max tokens (burst)
        //             TokensPerPeriod = 10,       // Tokens refilled each period
        //             ReplenishmentPeriod = TimeSpan.FromSeconds(10),
        //             AutoReplenishment = true,
        //             QueueLimit = 0
        //         });
        // });
    }

    private static void AddReverseProxy(IHostApplicationBuilder builder)
    {
        builder.Services.AddReverseProxy()
                    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));
    }
}