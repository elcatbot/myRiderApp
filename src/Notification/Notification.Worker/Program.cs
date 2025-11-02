var builder = Host.CreateApplicationBuilder(args);

builder.AddApplicationServices();

builder.Services.AddHostedService<NotificationWorker>();

var host = builder.Build();
host.Run();
