namespace myRideApp.Notification.Api.Extensions;

public static class ApplicationExtensions
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
        // builder.AddElasticsearchClient("elasticsearch");

        builder.AddHostServices();

        builder.AddRabbitMqEventBus(builder.Configuration["EventBusConnection"]!);

        builder.Services.AddSingleton<IPublishSubscribeEvents, PublishSubscribeEvents>();

        builder.AddCacheService("CacheConnection");

        builder.Services.AddTransient(typeof(IContactRepository<>), typeof(ContactRepository<>));

        builder.Services.AddMediatR(conf => conf.RegisterServicesFromAssemblyContaining(typeof(DriverNotifiedIntegrationEventHandler)));

        builder.Services.AddSingleton<IEmailSender, MailKitEmailSender>();

        builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("Email:Smtp"));

        builder.Services.AddSingleton<IRetryPolicyService, RetryPolicyService>();

    }
}