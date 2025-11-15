namespace myRideApp.Rides.Application.DTOs;

public record RidePageIndexDto
{
    public int PageIndex { get; init; }
    public int PageSize { get; init; }
    public long Count { get; init; }
    public IEnumerable<RideDto>? Data { get; init; }
}