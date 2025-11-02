namespace myRideApp.Utilities.Cache.Extensions;

public static class RedisCacheExtensions
{
    public static IHostApplicationBuilder AddCacheService(
        this IHostApplicationBuilder builder,
        string connectionstring
    )
    {
        var isAspire = Environment.GetEnvironmentVariable("RUNNING_IN_ASPIRE") == "true";
        connectionstring = isAspire ? "cache" : connectionstring;
        builder.AddRedisClient(connectionstring);
        builder.Services.AddScoped<ICacheService, RedisCacheService>();
        return builder;
    }
}