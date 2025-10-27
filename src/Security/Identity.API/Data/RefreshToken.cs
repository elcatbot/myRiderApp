namespace myRideApp.Identity.Data;

public record RefreshToken
{
    public int Id { get; set; }
    public string? Token { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ExpiresAt { get; set; }
    public bool IsRevoked { get; set; }
    public string? UserId { get; set; }
    public AppUser? AppUser { get; set; }
    public string? ReplacedByToken { get; set; }
}
