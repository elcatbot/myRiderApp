namespace myRideApp.Drivers.Domain.Models;

public class AvailabilityWindow(DayOfWeek Day, TimeSpan Start, TimeSpan End) : ValueObject
{
    public DayOfWeek Day { get; } = Day;
    public TimeSpan Start { get; } = Start;
    public TimeSpan End { get; } = End;
    
    public bool IsAvailableAt(DateTime time)
    {
        return time.DayOfWeek == Day && time.TimeOfDay >= Start && time.TimeOfDay <= End;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Day;
        yield return Start;
        yield return End;
    }

    public override string ToString() => $"{Day}: {Start:hh\\:mm}–{End:hh\\:mm}";
}
