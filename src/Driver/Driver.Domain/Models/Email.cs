
namespace myRideApp.Drivers.Domain.Models;

public class Email(string Value) : ValueObject
{
    public static Email Create(string value)
    {
        if (!Regex.IsMatch(value, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            throw new ArgumentException("Invalid email format");
        return new Email(value);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
    public override string ToString() => Value;
}
