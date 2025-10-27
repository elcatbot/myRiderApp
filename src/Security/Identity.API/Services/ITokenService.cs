namespace myRideApp.Identity.Services;

public interface ITokenService
{
    string GenerateAccessToken(AppUser user, IList<string> roles);
    string GenerateRefreshToken();
}