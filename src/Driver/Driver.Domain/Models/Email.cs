namespace myRideApp.Drivers.Domain.Models;

public class Email(string Value) : ValueObject
{
    public string Value { get; } = Value;

    public static Email Create(string value)
    {
        if (!Regex.IsMatch(value, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            throw new DriverDomainException("Invalid email format");
        return new Email(value);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
    public override string ToString() => Value;
}
