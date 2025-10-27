namespace myRideApp.Identity.Dtos;

public record TokenRequest
{
    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; }
}