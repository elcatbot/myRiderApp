namespace myRideApp.Identity.Services;

public class TokenService(IConfiguration Config) : ITokenService
{
    public string GenerateAccessToken(AppUser user, IList<string> roles)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(JwtRegisteredClaimNames.Email, user.Email!),
            new Claim("role", roles.FirstOrDefault() ?? "user")
        };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Config["JwtToken:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: Config["JwtToken:Issuer"],
            audience: Config["JwtToken:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GenerateRefreshToken()
    {
        var randomBytes = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomBytes);
        return Convert.ToBase64String(randomBytes);
    }
}
