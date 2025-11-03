namespace myRideApp.Extensions.Hosting;

public static class HostExtensions
{
    public static void AddHostServices(this IHostApplicationBuilder builder)
    {
        builder.Services.AddSerilog((context, configureLogger) =>
        {
            configureLogger
                .Enrich.FromLogContext()
                .WriteTo.Console();

            var isElasticsearch = builder.Configuration["elasticsearchConnection:host"];
            if (!string.IsNullOrEmpty(isElasticsearch))
            {
                configureLogger.WriteTo.Elasticsearch(new[] { new Uri(builder.Configuration["elasticsearchConnection:host"]!) }, opts =>
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
                            builder.Configuration["elasticsearchConnection:user"]!,
                            builder.Configuration["elasticsearchConnection:password"]!
                        )
                    )
                    .ServerCertificateValidationCallback((o, cert, chain, errors) => true);
                });
            }
        });
    }
}