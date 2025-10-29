namespace myRideApp.Drivers.Domain.Models;

public class Vehicle(string Make, string Model, string PlateNumber) : ValueObject
{
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Make;
        yield return Model;
        yield return PlateNumber;
    }
    public override string ToString() => $"{Make} {Model} ({PlateNumber})";
}
