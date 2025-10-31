namespace myRideApp.Drivers.Application.Commands;

public record AddAvailabilityWindowCommand(
    Guid DriverId,
    DayOfWeek Day,
    TimeSpan Start,
    TimeSpan End
) : IRequest<bool>;