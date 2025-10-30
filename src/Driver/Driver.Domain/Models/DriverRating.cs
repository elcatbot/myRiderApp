namespace myRideApp.Drivers.Domain.Models;

public class DriverRating(int TotalRatings, double SumOfScores) : ValueObject
{
    public int TotalRatings { get; } = TotalRatings;
    public double SumOfScores { get; } = SumOfScores;

    private readonly List<int> _ratings = new();

    public double Average => _ratings.Count == 0 ? 0 : _ratings.Average();

    public IReadOnlyList<int> Ratings => _ratings.AsReadOnly();

    public DriverRating AddRating(int score)
    {
        if (score < 1 || score > 5)
        {
            throw new DriverDomainException("Rating must be between 1 and 5");
        }

        return new DriverRating(TotalRatings + 1, SumOfScores + score);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return TotalRatings;
        yield return SumOfScores;
    }

    public override string ToString() => $"{Average:F1} ({TotalRatings} ratings)";
}
