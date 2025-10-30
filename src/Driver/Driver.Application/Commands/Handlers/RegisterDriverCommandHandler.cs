namespace myRideApp.Drivers.Application.Commands.Handlers;

public class RegisterDriverCommandHandler : IRequestHandler<RegisterDriverCommand, Guid>
{
    private readonly IDriverRepository _repository;

    public RegisterDriverCommandHandler(IDriverRepository repository, DriverContext context)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(RegisterDriverCommand request, CancellationToken cancellationToken)
    {
        var driver = new Driver(
            request.Name!,
            Email.Create(request.Email!),
            PhoneNumber.Create(request.PhoneNumber!),
            new Vehicle(request.Make!, request.Model!, request.PlateNumber!)
        );

        await _repository.AddAsync(driver);
        await _repository.SaveChangesAsync();
        return driver.Id;
    }
}
