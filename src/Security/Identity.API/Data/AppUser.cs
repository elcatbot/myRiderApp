namespace myRideApp.Identity.Data;

public class AppUser : IdentityUser
{
    public UserState State { get; set; }
    public ICollection<RefreshToken>? RefreshTokens { get; set; }
}

public enum UserState
{
    Enabled,
    Disabled,
    Locked
}