namespace myRideApp.Identity.Dtos;

public record UserRegister
{
    public string? Email { get; set; }
    public string? Name { get; set; }
    public string? Password { get; set; }
    public string? Role { get; set; } // e.g., "rider", "driver", "admin"
}