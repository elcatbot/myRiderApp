
namespace myRideApp.Drivers.Domain.Models;

public class PhoneNumber(string Value) : ValueObject
{
    public string Value { get; } = Value;

    public static PhoneNumber Create(string value)
    {
        if (!Regex.IsMatch(value, @"^\+\d{10,15}$"))
            throw new ArgumentException("Invalid phone number");
        return new PhoneNumber(value);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value;
}
