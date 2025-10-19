namespace myRideApp.Rides.Api.Extensions;

public static class HostExtensions
{
    public static void AddHostServices(this IHostBuilder builder)
    {
        builder.UseSerilog((context, configureLogger) =>
        {
            configureLogger
                .Enrich.FromLogContext()
                .WriteTo.Console();

            var isElasticsearch = context.Configuration["elasticsearchConnection:host"];
            if (isElasticsearch != null)
            {
                configureLogger.WriteTo.Elasticsearch(new[] { new Uri(context.Configuration["elasticsearchConnection:host"]!) }, opts =>
                {
                    opts.DataStream = new DataStreamName("logs", "ride-api", "demo");
                    opts.BootstrapMethod = BootstrapMethod.Failure;
                    opts.ConfigureChannel = channelOpts =>
                    {
                        channelOpts.BufferOptions = new BufferOptions
                        {
                            ExportMaxConcurrency = 10
                        };
                    };
                }, transport =>
                {
                    transport.Authentication(
                        new BasicAuthentication(
                            context.Configuration["elasticsearchConnection:user"]!,
                            context.Configuration["elasticsearchConnection:password"]!
                        )
                    )
                    .ServerCertificateValidationCallback((o, cert, chain, errors) => true);
                });
            }
        });
    }
}