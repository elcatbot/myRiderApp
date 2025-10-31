namespace myRideApp.Drivers.Application.Commands;

public record RateDriverCommand(Guid DriverId, int Score) : IRequest<bool>;
