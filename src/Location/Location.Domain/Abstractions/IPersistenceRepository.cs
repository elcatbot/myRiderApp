namespace myRideApp.Location.Domain.Abstractions;

public interface IPersistenceRepository
{
    Task<DriverLocation> GetByIdAsync(Guid id);
    IQueryable<DriverLocation> GetAllAsync();
    void Add(DriverLocation ride);
    void Update(DriverLocation ride);
    Task SaveChangesAsync();
}
