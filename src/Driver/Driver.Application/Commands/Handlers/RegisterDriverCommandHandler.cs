using myRideApp.Notification.Application.IntegrationEvents;

namespace myRideApp.Drivers.Application.Commands.Handlers;

public class RegisterDriverCommandHandler(
    IDriverRepository Repository,
    IPublishSubscribeEvents publishEvents
)
    : IRequestHandler<RegisterDriverCommand, Guid>
{
    public async Task<Guid> Handle(RegisterDriverCommand request, CancellationToken cancellationToken)
    {
        var driver = new Driver(
            request.Name!,
            Email.Create(request.Email!),
            PhoneNumber.Create(request.PhoneNumber!),
            new Vehicle(request.Make!, request.Model!, request.PlateNumber!)
        );
        
        await Repository.AddAsync(driver);
        await Repository.SaveChangesAsync();

        await publishEvents.PublishAsync(
            new DriverProfileUpdatedIntegrationEvent(
                driver.Id,
                driver.Name!,
                driver.Email!.Value,
                string.Empty,
                driver.RegisteredAt
            )
        , nameof(Driver));

        return driver.Id;
    }
}
