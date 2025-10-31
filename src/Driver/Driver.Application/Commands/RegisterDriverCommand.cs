namespace myRideApp.Drivers.Application.Commands;

public record RegisterDriverCommand(
    string Name,
    string Email,
    string PhoneNumber,
    string Make,
    string Model,
    string PlateNumber
) : IRequest<Guid>;