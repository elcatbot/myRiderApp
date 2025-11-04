using myRideApp.Notification.Application.IntegrationEvents;

namespace myRideApp.Drivers.Application.Commands.Handlers;

public class RegisterDriverCommandHandler(
    IDriverRepository Repository
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
        Repository.Add(driver);
        await Repository.SaveChangesAsync();
        return driver.Id;
    }
}
