namespace myRideApp.Rides.Domain;

public class Fare : ValueObject
{
    public decimal Amount { get; private set; }
    public string Currency { get; private set; }

    public Fare(decimal amount, string currency)
    {
        if (amount < 0)
        {
            throw new ArgumentException("Fare cannot be negative.");
        }
        Amount = amount;
        Currency = currency;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        throw new NotImplementedException();
    }
}
